using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
public class CameraController : MonoBehaviour
{
    public PlayerInput pi;
    public float horizontalSpeed = 100.0f;
    public float verticalSpeed = 80.0f;
    public Image lockDot;
    public bool lockState;
    public bool isAI = false;

    private float tempEulerX;
    private GameObject playerHandle;
    private GameObject cameraHandle;
    private GameObject model;
    private new Camera camera;

    private LockTarget lockTarget;

    void Awake()
    {
        cameraHandle = transform.parent.gameObject;
        playerHandle = cameraHandle.transform.parent.gameObject;
        model = playerHandle.GetComponent<ActorController>().model;

        if (!isAI)
        {
            camera = Camera.main;
            lockDot.enabled = false;
            //Cursor.lockState = CursorLockMode.Locked;//ȡ�������ʾ���������ĵ�
        }
        lockState = false;
    }

    void Update()
    {
        if (lockTarget != null)
        {
            if(!isAI)
            {
            lockDot.rectTransform.position = Camera.main.WorldToScreenPoint(lockTarget.obj.transform.position + new Vector3(0, lockTarget.halfHeight, 0));//������ŵ�������

            }
            if (Vector3.Distance(model.transform.position, lockTarget.obj.transform.position) > 10.0f)//������˹�Զ���������
            {
                LockProcessA(null, false, false, isAI);
            }
            if (lockTarget!=null&&lockTarget.am!=null&& lockTarget.am.sm.isDie)
            {
                LockProcessA(null, false, false, isAI);
            }
        }
    }

    private void LockProcessA(LockTarget _locktarget,bool _lockDotEnable,bool _lockState,bool _isAI)//����lockTarget��ͼ�꣬ͼ����ʾ״̬
    {
        lockTarget = _locktarget;
        if(!_isAI)
        {
            lockDot.enabled = _lockDotEnable;
        }
        lockState = _lockState;
    }

    void FixedUpdate()
    {

        if (lockTarget == null)
        {
            Vector3 tempModelEuler = model.transform.eulerAngles;

            playerHandle.transform.Rotate(Vector3.up, pi.Jright * horizontalSpeed * Time.fixedDeltaTime);
            tempEulerX -= pi.Jup * verticalSpeed * Time.deltaTime;//ֱ�Ӽ��ϽǶ�
            tempEulerX = Mathf.Clamp(tempEulerX, -40, 30);
            cameraHandle.transform.localEulerAngles = new Vector3(tempEulerX, 0, 0);

            model.transform.eulerAngles = tempModelEuler;
        }
        else
        {
            Vector3 tempForward = lockTarget.obj.transform.position - model.transform.position;//����ͷ����
            tempForward.y = 0;
            playerHandle.transform.forward = tempForward;//���ﳯ��
            cameraHandle.transform.LookAt(lockTarget.obj.transform);//���������
        }
        if (!isAI)
        {
            camera.transform.eulerAngles = transform.eulerAngles;
            camera.transform.position = Vector3.Lerp(camera.transform.position, transform.position, 0.1f);
        }
    }

    public void LockUnlock()
    {
        //��ͷ�������˽ŵ�
        Vector3 modelOrigin1 = model.transform.position;//��ҵ����꣬�ڽŵ�
        Vector3 modelOrigin2 = modelOrigin1 + new Vector3(0, 1, 0);//�������1m

        //ai����ײ��Ҫ��ܶ�
        Vector3 boxCenter = isAI? (modelOrigin2): (modelOrigin2 + model.transform.forward * 5.0f);

        //���������˵���Ѱ�����ײ��
        Collider[] cols = Physics.OverlapSphere(boxCenter, 10.0f, LayerMask.GetMask("Player"));
        if(!isAI)//���
        {
            Array.Clear(cols, 0, cols.Length);
            cols = Physics.OverlapBox(boxCenter, new Vector3(0.5f, 0.5f, 5f), model.transform.rotation, LayerMask.GetMask("Enemy"));
        }

        if (cols.Length == 0)
        {
            LockProcessA(null, false, false, isAI);
        }
        else
        {
            foreach (var col in cols)
            {
                if (lockTarget != null && lockTarget.obj == col.gameObject)//˳���ܷ�
                {
                    LockProcessA(null, false, false, isAI);
                    break;
                }
                //lockTarget = new LockTarget(col.gameObject, col.bounds.extents.y);
                LockProcessA(new LockTarget(col.gameObject, col.bounds.extents.y), true, true, isAI);
                break;
            }
        }
    }

    private class LockTarget //װ�ص�����Ϣ����
    {
        public GameObject obj;
        public float halfHeight;
        public ActorManager am;

        public LockTarget(GameObject _obj, float _halfHeight)
        {
            obj = _obj;
            halfHeight = _halfHeight;
            am = _obj.GetComponent<ActorManager>();
        }
    }

}

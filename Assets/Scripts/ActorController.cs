using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActorController : MonoBehaviour
{
    public GameObject model;
    public PlayerInput pi;
    public CameraController camcon;
    public float walkSpeed = 2.0f;
    public float runMultiplier = 2.0f;
    public float jumpVelocity = 5.0f;
    public float rollVelocity = 1.0f;
    public float JumpAttackDevc = 5.0f;
    //public float jabMultiplier = 3.0f;

    [Header("=========Friction Setting==========")]
    public PhysicMaterial frictionOne;
    public PhysicMaterial frictionZero;

    public Animator anim;
    private Rigidbody rigid;
    private Vector3 planarVec;//�ƶ�����
    public Vector3 thrustVec;
    private Vector3 targetForward;//��Ծ�Ĵ�ֱ����
    private bool canAttack;
    private bool lockPlanar = false;
    private bool trackDirection = false;
    private CapsuleCollider col;
    //private float lerpTarget;
    private Vector3 deltaPos;

    public bool leftIsShield = false;
    public bool IsAI = false;

    public delegate void OnActionDelegate();
    public event OnActionDelegate OnAction;
    private void Awake()
    {
        pi = GetComponent<PlayerInput>();
        anim = model.GetComponent<Animator>();
        rigid = GetComponent<Rigidbody>();
        col = GetComponent<CapsuleCollider>();
    }
    void Start()
    {

    }

    void Update()
    {
        //anim.SetBool("defense",pi.defense);
        if (gameObject.layer == LayerMask.NameToLayer("Player") || gameObject.layer == LayerMask.NameToLayer("Enemy"))
        {
            if (pi.lockon)
            {
                camcon.LockUnlock();
            }

            if (camcon.lockState == false)
            {
                anim.SetFloat("forward", pi.Dmag * Mathf.Lerp(anim.GetFloat("forward"), (pi.run ? 2.0f : 1.0f), 0.5f));//��Ϊ����ֵ
                anim.SetFloat("right", 0);
            }
            else
            {
                Vector3 localDvec = transform.InverseTransformVector(pi.Dvec);//����ת����
                anim.SetFloat("forward", Mathf.Lerp(anim.GetFloat("forward"), localDvec.z * ((pi.run) ? 2.0f : 1.0f), 0.5f));
                anim.SetFloat("right", localDvec.x * ((pi.run) ? 2.0f : 1.0f));
            }

            if (pi.roll || (rigid.velocity.magnitude > 7.0f && tag == "Player"))
            {
                anim.SetTrigger("roll");
            }

            if (pi.jumpattack)
            {
                anim.SetTrigger("jumpAttack");
            }

            if (pi.jump)
            {
                anim.SetTrigger("jump");
            }
            else if ((pi.rb || pi.lb) && (CheckState("ground") || CheckStateTag("attackL") || CheckStateTag("attackR")) && canAttack)
            {
                if (pi.rb)
                {
                    anim.SetBool("R0L1", false);
                    anim.SetTrigger("attack");
                }
                else if (pi.lb && !leftIsShield)
                {
                    anim.SetBool("R0L1", true);
                    anim.SetTrigger("attack");
                }

            }


            if ((pi.rt || pi.lt) && (CheckState("ground") || CheckStateTag("attackL") || CheckStateTag("attackR")) && canAttack)
            {
                if (pi.rt)
                {
                    anim.SetTrigger("heavyAttack");
                }
                else
                {
                    if (!leftIsShield)
                    {
                        //�����ػ�
                    }
                    else
                    {
                        //�ܷ�
                        anim.SetTrigger("counterBack");
                    }
                }
            }

            if (leftIsShield)//�ֶ�ʱ
            {
                if (CheckState("ground") || CheckState("blocked"))//�ڵ�����ܻ�״̬ʱ�������Գֶ�
                {
                    anim.SetLayerWeight(anim.GetLayerIndex("defense"), 1);
                    anim.SetBool("defense", pi.defense);
                }
                else
                {
                    anim.SetLayerWeight(anim.GetLayerIndex("defense"), 0);//�ر�defense Layer��Ȩ��
                    anim.SetBool("defense", false);
                }
            }
            else
            {
                anim.SetLayerWeight(anim.GetLayerIndex("defense"), 0);
            }

            if (camcon.lockState == false)//δ����Ŀ��
            {
                if (pi.inputEnabled == true)
                {
                    if (pi.Dmag > 0.1f)//��ֹ���ٿؽ�ɫʱ���﷽���λ
                    {
                        targetForward = Vector3.Slerp(model.transform.forward, pi.Dvec, 0.3f);//ת�����
                        model.transform.forward = targetForward;
                    }

                }
                if (lockPlanar == false)
                {
                    planarVec = pi.Dmag * model.transform.forward * walkSpeed * ((pi.run) ? runMultiplier : 1.0f);
                }
            }
            else
            {
                if (trackDirection == false)
                {
                    model.transform.forward = transform.forward;
                }
                else
                {
                    model.transform.forward = planarVec.normalized;
                }

                if (lockPlanar == false)
                {
                    planarVec = pi.Dvec * walkSpeed * ((pi.run) ? runMultiplier : 1.0f);
                }
            }

            if (pi.action)
            {
                dunFan();
            }
        }
    }

    void FixedUpdate()
    {
        rigid.position += deltaPos;
        rigid.velocity = new Vector3(planarVec.x, rigid.velocity.y, planarVec.z) + thrustVec;
        thrustVec = Vector3.zero;
        deltaPos = Vector3.zero;
    }
    public void dunFan()
    {
        OnAction.Invoke();
    }

    public bool CheckState(string stateName, string layerName = "Base Layer")
    {
        int layerIndex = anim.GetLayerIndex(layerName);
        bool state = anim.GetCurrentAnimatorStateInfo(layerIndex).IsName(stateName);
        return state;
    }

    public bool CheckStateTag(string tagName, string layerName = "Base Layer")
    {
        int layerIndex = anim.GetLayerIndex(layerName);
        bool state = anim.GetCurrentAnimatorStateInfo(layerIndex).IsTag(tagName);
        return state;
    }
    /// <summary>
    /// Message processing block
    /// </summary>
    public void OnJumpEnter()//��ס���򣬱��ֺ�������������������
    {
        pi.inputEnabled = false;
        lockPlanar = true;
        thrustVec = new Vector3(0, jumpVelocity, 0);
        trackDirection = true;
    }

    public void IsGround()
    {
        anim.SetBool("isGround", true);
        canAttack = true;
    }

    public void IsNoGround()
    {
        anim.SetBool("isGround", false);
        canAttack = false;
    }

    public void OnGroundEnter()
    {
        pi.inputEnabled = true;
        lockPlanar = false;
        col.material = frictionOne;
        trackDirection = false;
       
    }

    public void OnGroundExit()
    {
        col.material = frictionZero;
    }
    public void OnFallEnter()
    {
        pi.inputEnabled = false;
        lockPlanar = true;
    }

    public void OnRollEnter()
    {
        pi.inputEnabled = false;
        lockPlanar = true;
        thrustVec = new Vector3(0, rollVelocity, 0);
        trackDirection = true;
    }

    public void OnJabEnter()
    {
        pi.inputEnabled = false;
        lockPlanar = true;

    }

    public void OnJabUpdate()
    {
        thrustVec = model.transform.forward * anim.GetFloat("jabVelocity");
    }

    public void OnAttack1hAEnter()
    {
        pi.inputEnabled = false;
        //lerpTarget = 1.0f;
        //print("OnAttack1hAEnter"+camcon.lockState);
        if (IsAI&&camcon.lockState==true)
        {
            print("on attack1h enter");
            camcon.LockUnlock();
        }
    }

    public void OnheavyAttackEnter()
    {
        pi.inputEnabled = false;
    }

    public void OnJumpAttackEnter()
    {
        pi.inputEnabled = false;
        trackDirection = true;
        planarVec = planarVec/10;
        lockPlanar = true;
    }

    public void OnAttack1hAUpdate()
    {
        thrustVec = model.transform.forward * anim.GetFloat("attack1hAVelocity");//������ǰ����
        //�л�����״̬�Ĺ���   BaseLayer���ɵ�attack��
        //float currentWeight = anim.GetLayerWeight(anim.GetLayerIndex("attack"));
        //currentWeight = Mathf.Lerp(currentWeight, lerpTarget,0.1f);
        //anim.SetLayerWeight(anim.GetLayerIndex("attack"), currentWeight);
    }

    public void OnheavyAttackUpdate()
    {
        thrustVec = model.transform.forward * anim.GetFloat("attack1hAVelocity");//������ǰ����
    }

    public void OnJumpAttackUpdate()
    {
        thrustVec += model.transform.forward * anim.GetFloat("attack1hAVelocity");
    }

    public void OnAttackExit()
    {
        model.SendMessage("WeaponDisable");

        //print("OnAttackExit"+camcon.lockState);
        if(camcon.lockState==false&&IsAI)
        {
        StartCoroutine(UnlockPlayer());
        }
        trackDirection = false;
        pi.inputEnabled = true;
        lockPlanar = false;
    }

    IEnumerator UnlockPlayer()
    {
        //print("��ʼЯ��");
        yield return new WaitForSeconds(0.5f);
        //ai��������Ŀ��
        if (IsAI && pi.rb == false)
        {
            //print("��Ŀ�ʼ");
            camcon.LockUnlock();
        }
    }

    public void OnHitEnter()
    {
        pi.inputEnabled = false;
        planarVec = Vector3.zero;
        model.SendMessage("WeaponDisable");
    }

    public void OnBlockedEnter()
    {
        pi.inputEnabled = false;
        model.SendMessage("WeaponDisable");
    }

    public void OnDieEnter()
    {
        pi.inputEnabled = false;
        planarVec = Vector3.zero;
        model.SendMessage("WeaponDisable");
    }

    public void OnStunnedEnter()
    {
        pi.inputEnabled = false;
        planarVec = Vector3.zero;
    }

    public void OncounterBackEnter()
    {
        pi.inputEnabled = false;
        planarVec = Vector3.zero;
    }
    public void OnUpdateRM(object _deltaPos)
    {
        if (CheckState("attack1hC"))
        {
            deltaPos += (Vector3)_deltaPos;//����
        }
    }

    public void IssueTrigger(string triggerName)
    {
        anim.SetTrigger(triggerName);
    }

    public void SetBool(string boolName, bool value)
    {
        anim.SetBool(boolName, value);
    }

    public void OnLockEnter()
    {
        pi.inputEnabled = false;
        planarVec = Vector3.zero;
        model.SendMessage("WeaponDisable");
    }
}

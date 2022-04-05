using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponManager : IActorManagerInterface
{
    //public ActorManager am;
    private Collider weaponColL;
    private Collider weaponColR;

    public GameObject whL;
    public GameObject whR;

    public WeaponController wcL;
    public WeaponController wcR;

    private void Start()
    {
        try
        {
            whL = transform.DeepFind("weaponHandleL").gameObject;
            wcL = BindWeaponController(whL);
            weaponColL = whL.GetComponentInChildren<Collider>();
            weaponColL.enabled = false;
        }
        catch (System.Exception ex) 
        {
            //û�С�weaponHandleL������
        }

        try
        {
            whR = transform.DeepFind("weaponHandleR").gameObject;
            wcR = BindWeaponController(whR);
            weaponColR = whR.GetComponentInChildren<Collider>();
            weaponColR.enabled = false;
        }
        catch (System.Exception ex) 
        {
            //û�С�weaponHandleR������
        }
    }

    public void ChangeDualHands(bool dualOn)//�л�˫��
    {
        am.ChangeDualHands(dualOn);
    }

    public void UpdateCollider(string side,Collider col)
    {
        if(side=="L")
        {
            weaponColL = col;
        }
        else if(side=="R")
        {
            weaponColR = col;
        }
    }

    public void UnloadWeapon(string side)
    {
        if(side=="L")
        {
            foreach (Transform trans in whL.transform)
            {
                weaponColL = null;
                wcL.wData = null;
                Destroy(trans.gameObject);
            }
        }
        else if(side == "R")
        {
            foreach (Transform trans in whR.transform)
            {
                weaponColR = null;
                wcR.wData = null;
                Destroy(trans.gameObject);
            }
        }
    }


    public WeaponController BindWeaponController(GameObject targetObj)//��wc�ű���������һ��Ŀ�����
    {
        WeaponController temWc;
        temWc = targetObj.GetComponent<WeaponController>();
        if (temWc == null)
        {
            temWc = targetObj.AddComponent<WeaponController>();
        }
        temWc.wm = this;
        return temWc;
    }
    public void WeaponEnable()
    {
        if (am.ac.CheckStateTag("attackL"))
        {
            weaponColL.enabled = true;
        }
        else
        {
            weaponColR.enabled = true;
        }
    }

    public void WeaponDisable()
    {
        weaponColR.enabled = false;
        weaponColL.enabled = false;
    }

    public void CounterBackEnter()
    {
        am.SetIsCounterBack(true);

    }

    public void CounterBackDisable()
    {
        am.SetIsCounterBack(false);
    }

    //��ն�����¼�
    public void AddJumpAttackDevc()
    {
        am.ac.thrustVec += new Vector3(0, 4.5f, 0);
    }
}

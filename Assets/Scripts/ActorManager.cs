using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActorManager : MonoBehaviour
{
    public ActorController ac;
    public BattleManager bm;
    public WeaponManager wm;
    public StateManager sm;
    public DirectorManager dm;
    public InteractionManager im;

    [Header("=== Override Animator ===")]
    public AnimatorOverrideController oneHandAnim;
    public AnimatorOverrideController twoHandAnim;
    private void Awake()
    {
        ac = GetComponent<ActorController>();
        GameObject model = ac.model;
        GameObject sensor = null;
        try
        {
        sensor = transform.Find("sensor").gameObject;
        }catch(System.Exception ex)
        {  
            //û�С�sensor������
        }
        bm = Bind<BattleManager>(sensor);
        wm = Bind<WeaponManager>(model);
        sm = Bind<StateManager>(gameObject);
        dm = Bind<DirectorManager>(gameObject);
        im = Bind<InteractionManager>(sensor);
        ac.OnAction += DoAction;
    }

    //���Ų�ͬ��timeline
    public void DoAction()
    {
        if (im.overlapEvastms.Count != 0)
        {
            if (im.overlapEvastms[0].active == true&&!dm.IsPlaying())
            {

                if (im.overlapEvastms[0].eventName == "frontStab")//&& im.overlapEvastms[0].am.sm.isStunned)
                {
                    dm.PlayFrontStab("frontStab", this, im.overlapEvastms[0].am);
                }
                else if (im.overlapEvastms[0].eventName == "openBox")
                {
                    if (BattleManager.CheckAnglePlayer(ac.model, im.overlapEvastms[0].am.gameObject, 90))//���нǶ��ж�
                    {
                        transform.position = im.overlapEvastms[0].am.transform.position + im.overlapEvastms[0].am.transform.TransformVector(im.overlapEvastms[0].offset);//���������ڱ�����ǰ��
                        ac.model.transform.LookAt(im.overlapEvastms[0].am.transform, Vector3.up); //���ﳯ��
                        dm.PlayFrontStab("openBox", this, im.overlapEvastms[0].am);
                        im.overlapEvastms[0].active = false;
                    }
                }
            }
        }

        if(ac.IsAI)
        {
            print("׼��ִ�� dm.playt");
            dm.PlayFrontStab("frontStab", this,this.GetComponent<DummyIUerInput>().playerAM);
        }
    }

    //����
    private T Bind<T>(GameObject go) where T : IActorManagerInterface
    {
        T tempInst;
        if(go==null)
        {
            return null;
        }
        tempInst = go.GetComponent<T>();
        if (tempInst == null)
        {
            tempInst = go.AddComponent<T>();
        }
        tempInst.am = this;
        return tempInst;
    }

    //��ֵ�Ƿ��ڿɶܷ���״̬
    public void SetIsCounterBack(bool value)
    {
        sm.isCounterBackEnable = value;//��ֵ�Ƿ��ڿɶܷ���״̬
    }
    public void TryDoDamge(WeaponController targetWc, bool attackValid, bool counterValid)
    {
        if(sm.isImmortal)
        {
            return;
        }
        if (sm.isCounterBackSucess)
        {
            if (counterValid)
            {
                targetWc.wm.am.Stunned();//�������������amִ�б��ܷ�����
            }
        }
        else if (sm.isCounterBackFailure)//�ܷ�
        {
            if (attackValid)
            {
                HitOrDie(targetWc,false);
            }
        }
        else if (sm.isDefense)//�ֶ�
        {
            Blocked();
            targetWc.wm.am.tanDao();
        }
        else//����
        {
            if (attackValid)
            {
                HitOrDie(targetWc,true);
            }
        }
    }

    public void HitOrDie(WeaponController targetWc,bool doHitAnimation)
    {
        if (sm.HP <= 0)
        {

        }
        else
        {
            sm.AddHp(-1*targetWc.GetATK());
            if(sm.isHeavyAttack||sm.isJumpAttack)
            {
                sm.AddHp(-0.5f * targetWc.GetATK());
            }
            if (sm.HP > 0)
            {
                if (doHitAnimation)
                {
                    Hit();
                }
                //ðѪЧ��
            }
            else
            {
                Die();
            }
        }
    }

    public void Stunned()
    {
        ac.IssueTrigger("stunned");
    }
    public void Blocked()
    {
        ac.IssueTrigger("blocked");
    }
    public void Hit()
    {
        ac.IssueTrigger("hit");
    }
    public void Die()
    {
        ac.IssueTrigger("die");
        ac.pi.inputEnabled = false;
        if (ac.camcon.lockState == true)
        {
            ac.camcon.LockUnlock();
        }
        ac.camcon.enabled = false;//�ص�cc����
    }
    
    public void tanDao()
    {
        ac.IssueTrigger("tanDao");
    }

    public void LockActorController(bool value)
    {
        ac.SetBool("lock", value);
    }

    public void ChangeDualHands(bool dualOn)//�л�˫��
    {
        //print("change" + dualOn);
        if(dualOn)
        {
            ac.anim.runtimeAnimatorController = twoHandAnim;
        }
        else
        {
            ac.anim.runtimeAnimatorController = oneHandAnim;
        }
        
    
    }
}

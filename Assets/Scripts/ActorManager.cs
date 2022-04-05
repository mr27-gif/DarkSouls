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
            //没有“sensor”物体
        }
        bm = Bind<BattleManager>(sensor);
        wm = Bind<WeaponManager>(model);
        sm = Bind<StateManager>(gameObject);
        dm = Bind<DirectorManager>(gameObject);
        im = Bind<InteractionManager>(sensor);
        ac.OnAction += DoAction;
    }

    //播放不同的timeline
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
                    if (BattleManager.CheckAnglePlayer(ac.model, im.overlapEvastms[0].am.gameObject, 90))//开盒角度判断
                    {
                        transform.position = im.overlapEvastms[0].am.transform.position + im.overlapEvastms[0].am.transform.TransformVector(im.overlapEvastms[0].offset);//设置人物在宝箱正前方
                        ac.model.transform.LookAt(im.overlapEvastms[0].am.transform, Vector3.up); //人物朝向
                        dm.PlayFrontStab("openBox", this, im.overlapEvastms[0].am);
                        im.overlapEvastms[0].active = false;
                    }
                }
            }
        }

        if(ac.IsAI)
        {
            print("准备执行 dm.playt");
            dm.PlayFrontStab("frontStab", this,this.GetComponent<DummyIUerInput>().playerAM);
        }
    }

    //泛型
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

    //赋值是否在可盾反的状态
    public void SetIsCounterBack(bool value)
    {
        sm.isCounterBackEnable = value;//赋值是否在可盾反的状态
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
                targetWc.wm.am.Stunned();//让侵入的武器的am执行被盾反动画
            }
        }
        else if (sm.isCounterBackFailure)//盾反
        {
            if (attackValid)
            {
                HitOrDie(targetWc,false);
            }
        }
        else if (sm.isDefense)//持盾
        {
            Blocked();
            targetWc.wm.am.tanDao();
        }
        else//受伤
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
                //冒血效果
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
        ac.camcon.enabled = false;//关掉cc代码
    }
    
    public void tanDao()
    {
        ac.IssueTrigger("tanDao");
    }

    public void LockActorController(bool value)
    {
        ac.SetBool("lock", value);
    }

    public void ChangeDualHands(bool dualOn)//切换双手
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

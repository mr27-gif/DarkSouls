using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StateManager : IActorManagerInterface
{
    //public ActorManager am;

    public float HPMax = 15.0f;
    public float HP = 15.0f;
    public float ATK = 10.0f;
    public Image HPImageBG;
    public Image HPImage;
    public bool IsDisplayHp=false;
    public Vector3 HPImageHigh = new Vector3(0, 2.5f, 0);
    public Vector3 HPImageBGTrans = Vector3.zero;

    public bool isGround;
    public bool isJump;
    public bool isFall;
    public bool isRoll;
    public bool isJab;
    public bool isAttack;
    public bool isHit;
    public bool isDie;
    public bool isBlocked;
    public bool isDefense;
    public bool isCounterBack;//盾反动画状态
    public bool isCounterBackEnable;//盾反动画的关键帧状态
    public bool isStunned;
    public bool isHeavyAttack;
    public bool isJumpAttack;

    public bool isAllowDefense;
    public bool isImmortal;//无敌状态
    public bool isCounterBackSucess;
    public bool isCounterBackFailure;

    private void Start()
    {
        HP = HPMax;
    }

    private void Update()
    {
        if(HPImageBG!=null)
        {
            HPImageBGTrans = HPImageBG.gameObject.transform.position;
        }
        isGround = am.ac.CheckState("ground");
        isJump = am.ac.CheckState("jump");
        isFall = am.ac.CheckState("fall");
        isRoll = am.ac.CheckState("roll");
        isJab = am.ac.CheckState("jab");
        isAttack = am.ac.CheckStateTag("attackR") || am.ac.CheckStateTag("attackL");
        isHit = am.ac.CheckState("hit");
        isDie = am.ac.CheckState("die");
        isBlocked = am.ac.CheckState("blocked");
        isCounterBack = am.ac.CheckState("counterBack");
        isStunned = am.ac.CheckState("stunned");
        isHeavyAttack = am.ac.CheckState("heavyAttack");
        isJumpAttack = am.ac.CheckState("jumpAttack");
        isCounterBackSucess = isCounterBackEnable;
        isCounterBackFailure = isCounterBack && !isCounterBackEnable;

        isAllowDefense = isGround || isBlocked;
        isDefense = isAllowDefense && am.ac.CheckState("defense1h", "defense");
        isImmortal = isRoll || isJab;

        if (HPImage != null&&!am.ac.IsAI)
        {
            HPImage.fillAmount = HP / HPMax;
        }
        if (am.ac.IsAI && IsDisplayHp)
        {
            HPImageBG.transform.position = am.ac.camcon.camera.WorldToScreenPoint(transform.position + HPImageHigh);
            HPImage.fillAmount = HP / HPMax;
        }
    }

    public void AddHp(float value)
    {
        HP += value;
        HP = Mathf.Clamp(HP, 0, HPMax);//限制最小值
    }
}

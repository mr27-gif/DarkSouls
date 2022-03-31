using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CapsuleCollider))]//加这个脚本时自动加上collider
public class BattleManager : IActorManagerInterface
{
    private CapsuleCollider defCol;

    //public ActorManager am;
    private void Start()
    {
        defCol = GetComponent<CapsuleCollider>();
        defCol.center = new Vector3(0,1.0f,0);
        defCol.height = 2.0f;
        defCol.radius = 0.5f;//调大受击框
        defCol.isTrigger = true;
    }

    private void OnTriggerEnter(Collider col)//
    {
        WeaponController targetWc = col.GetComponentInParent<WeaponController>();
        if(targetWc==null)
        {
            return;
        }
        GameObject attacker = targetWc.wm.am.gameObject;//敌人handle
        GameObject recevier = am.ac.model;//自己handle

        if(col.tag=="Weapon")
        {
            am.TryDoDamge(targetWc, CheckAngleTarget(recevier,attacker,70),CheckAnglePlayer(recevier,attacker,30));
        }
    }

    public static bool CheckAnglePlayer(GameObject player,GameObject target,float playerAngleLimit)
    {
        Vector3 counterDir = target.transform.position - player.transform.position;

        float counterAngel1 = Vector3.Angle(player.transform.forward, counterDir);//攻击者在受击者朝向的角度
        float counterAngel2 = Vector3.Angle(target.transform.forward, player.transform.forward);//两者朝向的角度（偏移量）

        bool counterValid = (counterAngel1 < playerAngleLimit && Mathf.Abs(counterAngel2 - 180) < 30);//是否在盾反角度范围

        return counterValid;
    }

    public static bool CheckAngleTarget(GameObject player,GameObject target,float targetAngleLimit)
    {
        Vector3 attackingDir = player.transform.position - target.transform.position;

        float attackingAngel = Vector3.Angle(target.transform.forward, attackingDir);//算出受击者在攻击者朝向的角度

        bool attackValid = (attackingAngel < targetAngleLimit);//是否在攻击角度范围
        return attackValid;
    }
}

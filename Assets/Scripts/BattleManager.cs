using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CapsuleCollider))]//������ű�ʱ�Զ�����collider
public class BattleManager : IActorManagerInterface
{
    private CapsuleCollider defCol;

    //public ActorManager am;
    private void Start()
    {
        defCol = GetComponent<CapsuleCollider>();
        defCol.center = new Vector3(0,1.0f,0);
        defCol.height = 2.0f;
        defCol.radius = 0.5f;//�����ܻ���
        defCol.isTrigger = true;
    }

    private void OnTriggerEnter(Collider col)//
    {
        WeaponController targetWc = col.GetComponentInParent<WeaponController>();
        if(targetWc==null)
        {
            return;
        }
        GameObject attacker = targetWc.wm.am.gameObject;//����handle
        GameObject recevier = am.ac.model;//�Լ�handle

        if(col.tag=="Weapon")
        {
            am.TryDoDamge(targetWc, CheckAngleTarget(recevier,attacker,70),CheckAnglePlayer(recevier,attacker,30));
        }
    }

    public static bool CheckAnglePlayer(GameObject player,GameObject target,float playerAngleLimit)
    {
        Vector3 counterDir = target.transform.position - player.transform.position;

        float counterAngel1 = Vector3.Angle(player.transform.forward, counterDir);//���������ܻ��߳���ĽǶ�
        float counterAngel2 = Vector3.Angle(target.transform.forward, player.transform.forward);//���߳���ĽǶȣ�ƫ������

        bool counterValid = (counterAngel1 < playerAngleLimit && Mathf.Abs(counterAngel2 - 180) < 30);//�Ƿ��ڶܷ��Ƕȷ�Χ

        return counterValid;
    }

    public static bool CheckAngleTarget(GameObject player,GameObject target,float targetAngleLimit)
    {
        Vector3 attackingDir = player.transform.position - target.transform.position;

        float attackingAngel = Vector3.Angle(target.transform.forward, attackingDir);//����ܻ����ڹ����߳���ĽǶ�

        bool attackValid = (attackingAngel < targetAngleLimit);//�Ƿ��ڹ����Ƕȷ�Χ
        return attackValid;
    }
}

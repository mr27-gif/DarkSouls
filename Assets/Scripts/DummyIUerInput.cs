using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class DummyIUerInput : PlayerInput
{
    public ActorManager playerAM;
    private ActorManager am;
    public float distance;
    private float mytimer=-1;
    private float attackTimer;

    private void Awake()
    {
        playerAM = GameObject.FindGameObjectWithTag("Player").GetComponent<ActorManager>();
        am = GetComponent<ActorManager>();

    }

    private void Start()
    {
        mytimer = 2;
    }

    void Update()
    {
        lt = false;
        action = false;
        jump = false;
        roll = false;
        rb = false;
        distance = Vector3.Distance(transform.position, playerAM.transform.position);
        mytimer -= Time.deltaTime;
        attackTimer -= Time.deltaTime;

        if (playerAM.sm.HP == 0 || am.sm.HP == 0)
        {
            targetDup = 0;
            return;
        }

        //距离
        if (distance >= 8 && distance <= 12)
        {
            if (am.ac.camcon.lockState == false)
            {

                am.ac.camcon.LockUnlock();
            }
            run = true;
            targetDup = 1.0f;
        }
        else if (distance >= 1.8 && distance < 8)
        {
            run = false;
            targetDup = 1.0f;
        }
        else if (distance < 1.8)
        {
            targetDup = 0;
            if (mytimer < 0)
            {

                defense = false;
                int randnumber = (int)(Time.time * 10.0f % 9);
                switch (randnumber)
                {
                    case 1:
                        lt = true;
                        mytimer = 2.0f;
                        break;
                    case 2:
                        defense = true;
                        mytimer = Random.Range(0.5f, 1.25f);
                        break;
                    case 3:
                        roll = true;
                        mytimer = 2.0f;
                        break;
                }
                if (randnumber > 3)
                {
                    rb = true;
                    mytimer = 2.0f;
                }
            }
        }

        //相隔1s判断一次
        if (attackTimer < 0)
        {
            //ai被打
            if (playerAM.sm.isAttack == true)
            {
                switch ((Time.time) * 10 % 3)
                {
                    case 1:
                        roll = true;
                        break;
                    case 2:
                        jump = true;
                        break;

                }
            }

            //ai持盾且玩家攻击
            if (defense && playerAM.sm.isAttack == true)
            {
                lt = true;
            }

            //成功盾反玩家
            if (playerAM.sm.isStunned == true)
            {
                switch (Time.time * 10 % 3)
                {
                    case 1:
                        action = true;
                        break;
                    case 2:
                        rb = true;
                        break;
                    case 3:
                        break;
                }
            }

            attackTimer = 1.0f;
        }

        //AI移动
        if (inputEnabled == false)
        {
            targetDup = 0;
            targetDright = 0;
        }

        Dup = Mathf.SmoothDamp(Dup, targetDup, ref velocityDup, 0.1f);
        Dright = Mathf.SmoothDamp(Dright, targetDright, ref velocityDright, 0.1f);

        //方形映射到圆形
        Vector2 CircleVec2 = SquareToCircle(new Vector2(Dright, Dup));
        float Dright2 = CircleVec2.x;
        float Dup2 = CircleVec2.y;
        Dmag = Mathf.Sqrt((Dup2 * Dup2) + (Dright2 * Dright2));
        Dvec = Dright * transform.right + Dup * transform.forward;
    }

}

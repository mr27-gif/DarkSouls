using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class DummyXiaoguaiInput : PlayerInput
{
    public ActorManager playerAM;
    private ActorManager am;
    public float distance;
    private float mytimer = -1;
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
                rb = true;
                mytimer = 2.0f;
            }
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

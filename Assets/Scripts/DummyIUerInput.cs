using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class DummyIUerInput : PlayerInput
{
    public ActorManager playerAM;
    private NavMeshAgent nav;
    private ActorManager am;

    private void Start()
    {
        nav = GetComponent<NavMeshAgent>();
        playerAM = GameObject.FindGameObjectWithTag("Player").GetComponent<ActorManager>();
        am = GetComponent<ActorManager>();
    }

    void Update()
    {
        Dmag = Mathf.Sqrt((Dup * Dup) + (Dright * Dright));
        Dvec = Dright * transform.right + Dup * transform.forward;
        
        if (playerAM.sm.HP == 0||am.sm.HP==0)
        {
            return;
        }

        if (Vector3.Distance(transform.position, playerAM.transform.position) > 1.5)
        {
            rb = false;
            nav.SetDestination(playerAM.transform.position);
            Dmag = 1.0f;
        }
        else if(Vector3.Distance(transform.position, playerAM.transform.position) < 1.5)
        {
            nav.ResetPath();
            rb = true;
        }
    }
}

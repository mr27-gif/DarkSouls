using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractionManager : IActorManagerInterface
{
    private CapsuleCollider interCol;

    public List<EventCasterManager> overlapEvastms =new List<EventCasterManager>();//װ�����е���ײ���ϵ�EventCasterManager

    void Start()
    {
        interCol = GetComponent<CapsuleCollider>();
        
    }

    void Update()
    {
        
    }

    void OnTriggerEnter(Collider col)
    {
        EventCasterManager[] ecastms = col.GetComponents<EventCasterManager>();
        foreach (var ecastm in ecastms)
        {
            if(!overlapEvastms.Contains(ecastm))
            {
                overlapEvastms.Add(ecastm);
            }
        }
    }
    private void OnTriggerExit(Collider col)
    {
        EventCasterManager[] ecastms = col.GetComponents<EventCasterManager>();
        foreach (var ecastm in ecastms)
        {
            if (overlapEvastms.Contains(ecastm))
            {
                overlapEvastms.Remove(ecastm);
            }
        }
    }
}

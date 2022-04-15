using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventCasterManager : IActorManagerInterface
{
    public string eventName;
    public bool active;
    public Vector3 offset = new Vector3(0, 0, 0.5f);

    public Item item;//他身上当前可获得的物品

    void Start()
    {
        if(am==null)
        {
            am = GetComponentInParent<ActorManager>();
        }
    }
}

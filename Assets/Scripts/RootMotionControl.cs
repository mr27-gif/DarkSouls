using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RootMotionControl : MonoBehaviour
{
    private Animator anim;
    private void Awake()
    {
        anim = GetComponent<Animator>();
    }
    private void OnAnimatorMove()
    {   
        //此函数与fixedUpdate不同步
        SendMessageUpwards("OnUpdateRM",anim.deltaPosition );//装箱
    }
}

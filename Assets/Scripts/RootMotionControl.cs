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
        //�˺�����fixedUpdate��ͬ��
        SendMessageUpwards("OnUpdateRM",anim.deltaPosition );//װ��
    }
}

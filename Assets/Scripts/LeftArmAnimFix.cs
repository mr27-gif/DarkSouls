using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LeftArmAnimFix : MonoBehaviour
{
    private Animator anim;
    private ActorController ac;
    public Vector3 a;
    private void Awake()
    {
        anim = GetComponent<Animator>();
        ac = GetComponentInParent<ActorController>();
    }
    private void OnAnimatorIK(int layerIndex)
    {
        if (ac.leftIsShield)
        {
            if (anim.GetBool("defense") == false)//把绑定着盾牌的手臂放下来，视觉上是没有在防御状态
            {
                Transform leftLowerArm = anim.GetBoneTransform(HumanBodyBones.LeftLowerArm);
                leftLowerArm.localEulerAngles += a;
                anim.SetBoneLocalRotation(HumanBodyBones.LeftLowerArm, Quaternion.Euler(leftLowerArm.localEulerAngles));//欧拉转4元数
            }
        }
    }
}

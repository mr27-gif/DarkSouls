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
            if (anim.GetBool("defense") == false)//�Ѱ��Ŷ��Ƶ��ֱ۷��������Ӿ�����û���ڷ���״̬
            {
                Transform leftLowerArm = anim.GetBoneTransform(HumanBodyBones.LeftLowerArm);
                leftLowerArm.localEulerAngles += a;
                anim.SetBoneLocalRotation(HumanBodyBones.LeftLowerArm, Quaternion.Euler(leftLowerArm.localEulerAngles));//ŷ��ת4Ԫ��
            }
        }
    }
}

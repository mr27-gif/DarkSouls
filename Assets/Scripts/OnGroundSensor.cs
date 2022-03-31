using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnGroundSensor : MonoBehaviour
{
    public CapsuleCollider capcol;
    public float offset = 0.1f;//让胶囊体的两头圆心稍向下一点-------解决在斜坡跳跃也会有下落动画出现

    private Vector3 point1;//胶囊碰撞体的下方圆心坐标
    private Vector3 point2;//上方
    private float radius;
    

    // Start is called before the first frame update
    void Awake()
    {
        radius = capcol.radius-0.05f;

    }

    // Update is called once per frame
    void FixedUpdate()
    {
        point1 = transform.position + transform.up * (radius-offset);
        point2 = transform.position + transform.up * (capcol.height-offset) - transform.up * radius;



        Collider[] outputCols = Physics.OverlapCapsule(point1, point2, radius,LayerMask.GetMask("Ground"));
        if (outputCols.Length!=0)
        {
            SendMessageUpwards("IsGround");
        }
        else
        {
            SendMessageUpwards("IsNoGround");
        }
    }
}

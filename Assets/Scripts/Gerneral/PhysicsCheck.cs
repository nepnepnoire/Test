using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PhysicsCheck : MonoBehaviour
{
    [Header("检测参数")]
    public float checkRaduis;
    public LayerMask groundLayer;
    public bool isGround;
    public Vector2 bottomOffset;
    public bool isDashing = true;//是否可以冲刺,true是可以，false是不可以

    public void Update()
    {
        Check();
    }
    public void Check()
    {
        //检测地面
        isGround = Physics2D.OverlapCircle((Vector2)transform.position + bottomOffset, checkRaduis, groundLayer);
        //检测冲刺
        isDashing = !isGround;
        
    }

    //选中进行辅助线绘制（脚底范围）
    private void OnDrawGizmosSelected()
    {
        Gizmos.DrawSphere((Vector2)transform.position + bottomOffset, checkRaduis);
    }
}

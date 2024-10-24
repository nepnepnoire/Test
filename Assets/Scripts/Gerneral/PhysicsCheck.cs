using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PhysicsCheck : MonoBehaviour
{
    [Header("检测参数")]
    public float checkRaduis;
    public float checkHookRaduis;
    public LayerMask groundLayer;
    public LayerMask HookLayer; 
    public bool isGround;
    public bool isrightWall;
    public bool isleftWall;
    public bool isGrapplePoint; // 是否检测到抓钩点
    public Vector2 bottomOffset;
    public Vector2 rightOffset;
    public Vector2 leftOffset;
    public Vector2 HookOffset; // 抓钩偏移

    public void Update()
    {
        Check();
    }
    public void Check()
    {
        //检测地面
        isGround = Physics2D.OverlapCircle((Vector2)transform.position + bottomOffset, checkRaduis, groundLayer);
        //检测右墙面
        isrightWall = Physics2D.OverlapCircle((Vector2)transform.position + rightOffset, checkRaduis, groundLayer);
        //检测左墙面
        isleftWall = Physics2D.OverlapCircle((Vector2)transform.position + leftOffset, checkRaduis, groundLayer);
        //检测抓钩点
        // 检测抓钩点
        isGrapplePoint = Physics2D.OverlapCircle((Vector2)transform.position + HookOffset, checkHookRaduis, HookLayer);
    }



    //选中进行辅助线绘制（脚底范围）
    private void OnDrawGizmosSelected()
    {
        Gizmos.DrawSphere((Vector2)transform.position + bottomOffset, checkRaduis);
        Gizmos.DrawSphere((Vector2)transform.position + rightOffset, checkRaduis);
        Gizmos.DrawSphere((Vector2)transform.position + leftOffset, checkRaduis);
        // 绘制抓钩检测范围
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere((Vector2)transform.position + HookOffset, checkHookRaduis);
    }
}

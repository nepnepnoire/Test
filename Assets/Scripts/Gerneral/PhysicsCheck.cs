using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PhysicsCheck : MonoBehaviour
{
    [Header("������")]
    public float checkRaduis;
    public float checkHookRaduis;
    public LayerMask groundLayer;
    public LayerMask HookLayer; 
    public bool isGround;
    public bool isrightWall;
    public bool isleftWall;
    public bool isGrapplePoint; // �Ƿ��⵽ץ����
    public Vector2 bottomOffset;
    public Vector2 rightOffset;
    public Vector2 leftOffset;
    public Vector2 HookOffset; // ץ��ƫ��

    public void Update()
    {
        Check();
    }
    public void Check()
    {
        //������
        isGround = Physics2D.OverlapCircle((Vector2)transform.position + bottomOffset, checkRaduis, groundLayer);
        //�����ǽ��
        isrightWall = Physics2D.OverlapCircle((Vector2)transform.position + rightOffset, checkRaduis, groundLayer);
        //�����ǽ��
        isleftWall = Physics2D.OverlapCircle((Vector2)transform.position + leftOffset, checkRaduis, groundLayer);
        //���ץ����
        // ���ץ����
        isGrapplePoint = Physics2D.OverlapCircle((Vector2)transform.position + HookOffset, checkHookRaduis, HookLayer);
    }



    //ѡ�н��и����߻��ƣ��ŵ׷�Χ��
    private void OnDrawGizmosSelected()
    {
        Gizmos.DrawSphere((Vector2)transform.position + bottomOffset, checkRaduis);
        Gizmos.DrawSphere((Vector2)transform.position + rightOffset, checkRaduis);
        Gizmos.DrawSphere((Vector2)transform.position + leftOffset, checkRaduis);
        // ����ץ����ⷶΧ
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere((Vector2)transform.position + HookOffset, checkHookRaduis);
    }
}

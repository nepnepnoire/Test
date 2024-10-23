using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PhysicsCheck : MonoBehaviour
{
    [Header("������")]
    public float checkRaduis;
    public LayerMask groundLayer;
    public LayerMask wallLayer;
    public bool isGround;
    public bool isrightWall;
    public bool isleftWall;
    public Vector2 bottomOffset;
    public Vector2 rightOffset;
    public Vector2 leftOffset;

    public void Update()
    {
        Check();
    }
    public void Check()
    {
        //������
        isGround = Physics2D.OverlapCircle((Vector2)transform.position + bottomOffset, checkRaduis, groundLayer);
        //�����ǽ��
        isrightWall = Physics2D.OverlapCircle((Vector2)transform.position + rightOffset, checkRaduis, wallLayer);
        //�����ǽ��
        isleftWall = Physics2D.OverlapCircle((Vector2)transform.position + leftOffset, checkRaduis, wallLayer);

    }

    //ѡ�н��и����߻��ƣ��ŵ׷�Χ��
    private void OnDrawGizmosSelected()
    {
        Gizmos.DrawSphere((Vector2)transform.position + bottomOffset, checkRaduis);
        Gizmos.DrawSphere((Vector2)transform.position + rightOffset, checkRaduis);
        Gizmos.DrawSphere((Vector2)transform.position + leftOffset, checkRaduis);
    }
}

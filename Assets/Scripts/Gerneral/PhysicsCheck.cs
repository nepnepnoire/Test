using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PhysicsCheck : MonoBehaviour
{
    [Header("������")]
    public float checkRaduis;
    public LayerMask groundLayer;
    public bool isGround;
    public Vector2 bottomOffset;

    public void Update()
    {
        Check();
    }
    public void Check()
    {
        //������
        isGround = Physics2D.OverlapCircle((Vector2)transform.position + bottomOffset, checkRaduis, groundLayer);
       
        

    }

    //ѡ�н��и����߻��ƣ��ŵ׷�Χ��
    private void OnDrawGizmosSelected()
    {
        Gizmos.DrawSphere((Vector2)transform.position + bottomOffset, checkRaduis);
    }
}

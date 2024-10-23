using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Laser : Enemy
{
    public GameObject laserPrefab; // ���������Ԥ��
    public float speed = 10f; // �����ٶ�
    private float laserLength = 3f; // ���ⳤ��
    
    public override void pursuit()
    {
        GameObject laser = Instantiate(laserPrefab, transform.position, Quaternion.identity);
        LineRenderer lineRenderer = laser.GetComponent<LineRenderer>();

        // ���ü���������յ�
        lineRenderer.SetPosition(0, transform.position); // ���
        lineRenderer.SetPosition(1, transform.position + transform.right * laserLength); // �յ�

        // ���ҷ��伤��
        Rigidbody rb = laser.GetComponent<Rigidbody>();
        if (rb != null)
        {
            rb.velocity = transform.right * speed; // ���ҷ���
        }
        // ����ѡ����һ��ʱ������ټ���
        Destroy(laser, 2f); // 2�������
    }
}

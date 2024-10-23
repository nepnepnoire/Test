using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Laser : Enemy
{
    public GameObject laserPrefab; // 激光物体的预设
    public float speed = 30f; // 激光速度
    private float laserLength = 30f; // 激光长度
    
    public override void pursuit()
    {
        GameObject laser = Instantiate(laserPrefab, transform.position, Quaternion.identity);
        LineRenderer lineRenderer = laser.GetComponent<LineRenderer>();

        // 设置激光的起点和终点
        lineRenderer.SetPosition(0, transform.position); // 起点
        lineRenderer.SetPosition(1, transform.position + transform.right * laserLength); // 终点

        // 向右发射激光
        Rigidbody rb = laser.GetComponent<Rigidbody>();
        if (rb != null)
        {
            rb.velocity = transform.right * speed; // 向右发射
        }
        // 可以选择在一段时间后销毁激光
        Destroy(laser, 1f); // 2秒后销毁
    }
}

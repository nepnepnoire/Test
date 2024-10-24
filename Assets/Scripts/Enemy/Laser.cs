using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Laser : Enemy
{
    public GameObject laserPrefab; // 激光物体的预设
    //public float speed = 30f; // 激光速度
    //private float laserLength = 30f; // 激光长度

    public float Cooldown = 3f; // 冷却时间
    private float lastTime = 0f; // 上次攻击时间
    public float fireInterval = 0.1f;//激光间隔

    public override void Update()
    {
        if (Time.time >= lastTime + Cooldown)
        {
            StartCoroutine(FireLasers());
            lastTime = Time.time; // 更新上次攻击时间
        }
    }
    private IEnumerator FireLasers()
    {
        float totaltime = 0;
        while (totaltime <= 1)
        {
            fireLaser();
            yield return new WaitForSeconds(fireInterval);
            totaltime += fireInterval;
        }
    }
    void fireLaser()
    {
        Vector3 offset = new Vector3(10, 0, 0);
        GameObject laser = Instantiate(laserPrefab, transform.position + offset, Quaternion.identity);
        Destroy(laser, 1f); // 1秒后销毁
    }
}

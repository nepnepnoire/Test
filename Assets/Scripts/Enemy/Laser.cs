using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Laser : Enemy
{
    public GameObject laserPrefab; // ���������Ԥ��
    //public float speed = 30f; // �����ٶ�
    //private float laserLength = 30f; // ���ⳤ��

    public float Cooldown = 3f; // ��ȴʱ��
    private float lastTime = 0f; // �ϴι���ʱ��
    public float fireInterval = 0.1f;//������

    public override void Update()
    {
        if (Time.time >= lastTime + Cooldown)
        {
            StartCoroutine(FireLasers());
            lastTime = Time.time; // �����ϴι���ʱ��
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
        Destroy(laser, 1f); // 1�������
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Laserlaunch : MonoBehaviour
{
    public GameObject laserPrefab;
    public float laserSpeed;
    public int health;
    public int attack;

    public float moveSpeed = 2f; // �ƶ��ٶ�
    public float patrolDistance = 30f; // �ǻ��ľ���

    private Vector3 startPoint; // ��ʼ��
    private Vector3 endPoint; // ������
    private bool movingToEnd = true; // ��ǰ�ƶ�����

    public Transform player; // ��Ҷ���
    public float detectionRange = 1f; // ��ⷶΧ
    public LayerMask playerLayer; // ��Ҳ�

    // Start is called before the first frame update
    void Start()
    {
        //����tag��ʼ��
        switch (gameObject.tag)
        {
            case "Leaky wire":
                attack = 10000;
                break;
            case "Laser transmitter":
                fire_laser();
                attack = 1;
                break;
            case "dead line":
                attack = 2;
                break;
            default:
                attack = 0;
                break;
        }
    }

    public void move()
    {
        if (movingToEnd)
        {
            // ��������ƶ�
            transform.position = Vector3.MoveTowards(transform.position, endPoint, moveSpeed * Time.deltaTime);
            if (Vector3.Distance(transform.position, endPoint) < 0.1f) // ����������ı䷽��
            {
                movingToEnd = false;
            }
        }
        else
        {
            // ����ʼ���ƶ�
            transform.position = Vector3.MoveTowards(transform.position, startPoint, moveSpeed * Time.deltaTime);
            if (Vector3.Distance(transform.position, startPoint) < 0.1f) // ������ʼ���ı䷽��
            {
                movingToEnd = true;
            }
        }
    }
    // Update is called once per frame
    void Update()
    {
        switch (gameObject.tag)
        {
            case "leaky wire":
                attack = 10000;
                break;
            case "Laser transmitter":
                break;
            case "dead line":
                target_acquisition();
                break;
            default:
                attack = 0;
                break;
        }
    }
    public void fire_laser()
    {
        GameObject laser = Instantiate(laserPrefab, transform.position, transform.rotation);
        Rigidbody rb = laser.GetComponent<Rigidbody>();
        rb.velocity = new Vector2(-laserSpeed, 0); // ���ü�����ٶ�
    }
    public void target_acquisition()
    {
        float distanceToPlayer = Vector2.Distance(transform.position, player.position);

        if (distanceToPlayer <= detectionRange)
        {
            // ���������ΪĿ��
            Debug.Log("Target acquired: " + player.name);
        }
        else
        {
            move();
        }
    }
}

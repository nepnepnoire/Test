using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Laserlaunch : MonoBehaviour
{
    public GameObject laserPrefab;
    public float laserSpeed;
    public int health;
    public int attack;

    public float moveSpeed = 2f; // 移动速度
    public float patrolDistance = 30f; // 徘徊的距离

    private Vector3 startPoint; // 起始点
    private Vector3 endPoint; // 结束点
    private bool movingToEnd = true; // 当前移动方向

    public Transform player; // 玩家对象
    public float detectionRange = 1f; // 侦测范围
    public LayerMask playerLayer; // 玩家层

    // Start is called before the first frame update
    void Start()
    {
        //根据tag初始化
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
            // 向结束点移动
            transform.position = Vector3.MoveTowards(transform.position, endPoint, moveSpeed * Time.deltaTime);
            if (Vector3.Distance(transform.position, endPoint) < 0.1f) // 到达结束点后改变方向
            {
                movingToEnd = false;
            }
        }
        else
        {
            // 向起始点移动
            transform.position = Vector3.MoveTowards(transform.position, startPoint, moveSpeed * Time.deltaTime);
            if (Vector3.Distance(transform.position, startPoint) < 0.1f) // 到达起始点后改变方向
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
        rb.velocity = new Vector2(-laserSpeed, 0); // 设置激光的速度
    }
    public void target_acquisition()
    {
        float distanceToPlayer = Vector2.Distance(transform.position, player.position);

        if (distanceToPlayer <= detectionRange)
        {
            // 锁定玩家作为目标
            Debug.Log("Target acquired: " + player.name);
        }
        else
        {
            move();
        }
    }
}

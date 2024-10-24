using System.Collections;
using System.Collections.Generic;
using System.Net;
using System.Threading;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public enum EnemyState
    {
        idle,
        pursuit,
        attacked,
        generalAttack,
        fire
    }
    public EnemyManager manager;

    public int health;
    public int attack;
    public float moveSpeed = 2f; // 移动速度
    public float patrolDistance = 3f; // 徘徊的距离

    private Vector3 startPoint; // 起始点
    private Vector3 endPoint; // 结束点
    private bool movingToEnd = true; // 当前移动方向

    public Transform player; // 玩家对象
    public float detectionRange = 10f; // 侦测范围
    public LayerMask playerLayer; // 玩家层

    public EnemyState enemystate;
    // Start is called before the first frame update
    [Header("受伤无敌")]
    public float invulnerableDuration = 10f;
    public float invulnerableCounter;
    public bool invulnerable;
    public virtual void Start()
    {
        startPoint = transform.position;
        manager = FindObjectOfType<EnemyManager>();
        endPoint = startPoint + new Vector3(patrolDistance, 0, 0); // 在 X 轴上计算结束点
        enemystate = EnemyState.idle;//初始化待机状态
        GameObject target = GameObject.Find("Player"); // 假设目标对象名为"Player"
        if (target != null)
        {
            player = target.transform;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    // Update is called once per frame
    public virtual void Update()
    {
        EnemyController();
        if (health <= 0)
        {
            Debug.Log("Destroy");
            manager.DestroyEnemy(this, 0);
        }
    }
    public virtual void attacked()
    {
        return;
    }
    public virtual void pursuit()
    {
        return;
    }
    public void EnemyController()
    {
        CheckState();
        switch (enemystate)
        {
            case EnemyState.idle:
                move();
                break;
            case EnemyState.pursuit:
                pursuit();
                break;
            case EnemyState.attacked:
                attacked();
                break;
        }
    }
    public virtual void TriggerInvulnerable()
    {
        if (!invulnerable)
        {
            invulnerable = true;
            invulnerableCounter = invulnerableDuration;
            enemystate= EnemyState.attacked;
        }
    }
    public void move()
    {
        if (moveSpeed == 0) return ;
        if (movingToEnd)
        {
            // 向结束点移动
            transform.position = Vector3.MoveTowards(transform.position, endPoint, moveSpeed * Time.deltaTime);
            if (Vector3.Distance(transform.position, endPoint) < 0.1f) // 到达结束点后改变方向
            {
                movingToEnd = false;
                Vector3 currentScale = transform.localScale;
                // 反转 x 轴的缩放
                currentScale.x *= -1;
                // 应用新的缩放值
                transform.localScale = currentScale;
            }
        }
        else
        {
            // 向起始点移动
            transform.position = Vector3.MoveTowards(transform.position, startPoint, moveSpeed * Time.deltaTime);
            if (Vector3.Distance(transform.position, startPoint) < 0.1f) // 到达起始点后改变方向
            {
                movingToEnd = true;
                Vector3 currentScale = transform.localScale;
                // 反转 x 轴的缩放
                currentScale.x *= -1;
                // 应用新的缩放值
                transform.localScale = currentScale;
            }
        }
    }
    public void CheckState()
    {
        if(enemystate == EnemyState.attacked) 
        {
            invulnerableCounter -= Time.deltaTime;
            if (invulnerableCounter <= 0)
            {
                invulnerable = false;
                enemystate = EnemyState.pursuit;
            }
        }else
        {
            float distanceToPlayer = Vector2.Distance(transform.position, player.position);
            if (distanceToPlayer <= detectionRange && player.gameObject.activeSelf)
            {
                // 锁定玩家作为目标
                enemystate = EnemyState.pursuit;//进入追击状态
                Debug.Log("Target acquired: " + player.name);
            }
            else
            {
                enemystate = EnemyState.idle;//进入待机状态
            }
        }
    }
}

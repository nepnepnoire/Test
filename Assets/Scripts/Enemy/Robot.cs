using System.Collections;
using System.Collections.Generic;
using System.Net;
using UnityEngine;

public class Robot : Enemy
{
    [Header("攻击参数")]
    public GameObject attackTriggerPrefab; // 用于设置攻击触发器的预制体
    public float attackRange = 10f; // 攻击范围
    Animator animator;

    public float Cooldown = 2f; // 冷却时间
    private float lastTime = 0f; // 上次攻击时间

    public override void Start()
    {
        base.Start(); // 调用父类的 Start 方法
        animator = GetComponent<Animator>();
    }
    public override void Update()
    {
        base.Update(); // 调用父类的 Update 方法
        // 示例：根据按键设置布尔参数
        if (enemystate == EnemyState.pursuit)
        {
            animator.SetBool("isRushing", true);
        }
        else
        {
            animator.SetBool("isRushing", false);
        }
    }
    public override void pursuit()
    {
        if((player.position.x - transform.position.x)* transform.localScale.x < 0)
        {
            Vector3 nowScale = transform.localScale;
            nowScale.x *= -1;
            transform.localScale = nowScale;
            //Debug.Log("Opp");
        }
        transform.position = Vector3.MoveTowards(transform.position, player.position, 2 * moveSpeed * Time.deltaTime);
        float distanceToPlayer = Vector2.Distance(transform.position, player.position);

        if (distanceToPlayer <= attackRange)
        {
            if (Time.time >= lastTime + Cooldown)
            {
                generalAttack();
                lastTime = Time.time; // 更新上次攻击时间
            }
        }
    }
    void generalAttack()
    {
        Vector3 Scalex = new Vector3(0, 0, 0);
        // 计算攻击触发器的位置
        Vector3 spawnPosition = transform.position + Scalex * attackRange; // 根据角色方向计算生成位置

        // 生成攻击触发器
        GameObject attackTrigger = Instantiate(attackTriggerPrefab, spawnPosition, Quaternion.identity);

        // 设置攻击触发器的标签
        attackTrigger.tag = "Eattack";
        Destroy(attackTrigger, 0.2f); // 1秒后销毁
    }
}

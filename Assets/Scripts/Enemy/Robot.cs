using System.Collections;
using System.Collections.Generic;
using System.Net;
using UnityEngine;

public class Robot : Enemy
{
    Animator animator;

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
        if(transform.position == player.position)
        {
            Debug.Log("I Catch U ,HAHAHAHAHAHAHA~~~~~~~~!!!!!!!!!");
        }
    }
}

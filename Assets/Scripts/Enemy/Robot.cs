using System.Collections;
using System.Collections.Generic;
using System.Net;
using UnityEngine;

public class Robot : Enemy
{
    [Header("��������")]
    public GameObject attackTriggerPrefab; // �������ù�����������Ԥ����
    public float attackRange = 10f; // ������Χ
    Animator animator;

    public float Cooldown = 2f; // ��ȴʱ��
    private float lastTime = 0f; // �ϴι���ʱ��

    public override void Start()
    {
        base.Start(); // ���ø���� Start ����
        animator = GetComponent<Animator>();
    }
    public override void Update()
    {
        base.Update(); // ���ø���� Update ����
        // ʾ�������ݰ������ò�������
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
                lastTime = Time.time; // �����ϴι���ʱ��
            }
        }
    }
    void generalAttack()
    {
        Vector3 Scalex = new Vector3(0, 0, 0);
        // ���㹥����������λ��
        Vector3 spawnPosition = transform.position + Scalex * attackRange; // ���ݽ�ɫ�����������λ��

        // ���ɹ���������
        GameObject attackTrigger = Instantiate(attackTriggerPrefab, spawnPosition, Quaternion.identity);

        // ���ù����������ı�ǩ
        attackTrigger.tag = "Eattack";
        Destroy(attackTrigger, 0.2f); // 1�������
    }
}

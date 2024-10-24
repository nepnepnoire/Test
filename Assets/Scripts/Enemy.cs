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
    public float moveSpeed = 2f; // �ƶ��ٶ�
    public float patrolDistance = 3f; // �ǻ��ľ���

    private Vector3 startPoint; // ��ʼ��
    private Vector3 endPoint; // ������
    private bool movingToEnd = true; // ��ǰ�ƶ�����

    public Transform player; // ��Ҷ���
    public float detectionRange = 10f; // ��ⷶΧ
    public LayerMask playerLayer; // ��Ҳ�

    public EnemyState enemystate;
    // Start is called before the first frame update
    [Header("�����޵�")]
    public float invulnerableDuration = 10f;
    public float invulnerableCounter;
    public bool invulnerable;
    public virtual void Start()
    {
        startPoint = transform.position;
        manager = FindObjectOfType<EnemyManager>();
        endPoint = startPoint + new Vector3(patrolDistance, 0, 0); // �� X ���ϼ��������
        enemystate = EnemyState.idle;//��ʼ������״̬
        GameObject target = GameObject.Find("Player"); // ����Ŀ�������Ϊ"Player"
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
            // ��������ƶ�
            transform.position = Vector3.MoveTowards(transform.position, endPoint, moveSpeed * Time.deltaTime);
            if (Vector3.Distance(transform.position, endPoint) < 0.1f) // ����������ı䷽��
            {
                movingToEnd = false;
                Vector3 currentScale = transform.localScale;
                // ��ת x �������
                currentScale.x *= -1;
                // Ӧ���µ�����ֵ
                transform.localScale = currentScale;
            }
        }
        else
        {
            // ����ʼ���ƶ�
            transform.position = Vector3.MoveTowards(transform.position, startPoint, moveSpeed * Time.deltaTime);
            if (Vector3.Distance(transform.position, startPoint) < 0.1f) // ������ʼ���ı䷽��
            {
                movingToEnd = true;
                Vector3 currentScale = transform.localScale;
                // ��ת x �������
                currentScale.x *= -1;
                // Ӧ���µ�����ֵ
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
                // ���������ΪĿ��
                enemystate = EnemyState.pursuit;//����׷��״̬
                Debug.Log("Target acquired: " + player.name);
            }
            else
            {
                enemystate = EnemyState.idle;//�������״̬
            }
        }
    }
}

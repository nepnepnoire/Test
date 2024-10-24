using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class Boss : Enemy
{
    // Start is called before the first frame update
    public bool halfHP;
    public bool change;
    public float Attackrange = 5f;
    public float Firerange = 20f;
    public int maxhealth;
    Animator animator;
    public GameObject misslyPrefab;
    public GameObject leftwavePrefab;
    public GameObject rightwavePrefab;
    public GameObject GeneralattackPrefab;
    public bool isWalking;
    public bool isFiring;
    public bool isFlying;
    public bool isAttacking;
    public bool isUnderattack;
    public bool isDowning;
    public bool Die;
    public bool Display = false;
    public float ground;

    public float Cooldown = 4f; // 冷却时间
    private float lastTime = 0f; // 上次攻击时间

    void setisWalking()
    {
        isWalking=true;
        isFlying=false;
        isFiring=false;
        isAttacking=false;
        isUnderattack=false;
        isDowning=false;
    }
    void setisisFlying()
    {
        isWalking = false;
        isFlying = true;
        isFiring = false;
        isAttacking = false;
        isUnderattack = false;
        isDowning = false;
    }
    void setisisFiring()
    {
        isWalking = false;
        isFlying = false;
        isFiring = true;
        isAttacking = false;
        isUnderattack = false;
        isDowning = false;
    }
    void setisAttacking()
    {
        isAttacking = true;
    }
    void setisUnderattack()
    {
        isWalking = false;
        isFlying = false;
        isFiring = false;
        isAttacking = false;
        isUnderattack = true;
        isDowning = false;
    }
    void setisDowning()
    {
        isWalking = false;
        isFlying = false;
        isFiring = false;
        isAttacking = false;
        isUnderattack = false;
        isDowning = true;
    }
    Vector3 offset = new Vector3(0, 10f, 0);
    public override void Start()
    {
        base.Start();
        halfHP = false;
        change = false;
        animator = GetComponent<Animator>();
        maxhealth = health;
        ground = transform.position.y;
        //变身动画
    }
    // Update is called once per frame
    public override void Update()
    {
        base.EnemyController();
        if (health <= 0)
        {
            Die = true;
            manager.DestroyEnemy(this, 2f);
        }
        else if (health * 2 <= maxhealth)
        {
            halfHP = true;
        }
    }
    public override void attacked()
    {
        isUnderattack = false;
    }
    public override void TriggerInvulnerable()
    {
        if (!invulnerable)
        {
            invulnerable = true;
            invulnerableCounter = invulnerableDuration;
            enemystate = EnemyState.attacked;
            setisUnderattack();
        }
    }
    public override void pursuit()
    {
        float distanceToPlayer = Vector2.Distance(transform.position + offset, player.position);

        if (distanceToPlayer < Attackrange)
        {
            if (Time.time >= lastTime + Cooldown)
            {
                generalAttack();
                lastTime = Time.time; // 更新上次攻击时间
            }
            else if(!halfHP)
            {
                MoveTowardsPlayer();
            }        
            else
            {
                FlyTowardsPlayer();
            }
        }
        else if (distanceToPlayer < Firerange)
        {
            if (Time.time >= lastTime + Cooldown)
            {
                Fire();
                lastTime = Time.time; // 更新上次攻击时间
            }
        }
        else
        {
            if (Time.time >= lastTime + Cooldown)
            {
                Down();
                lastTime = Time.time; // 更新上次攻击时间
            }
        }
    }
    private IEnumerator DelayedWalking()
    {
        yield return new WaitForSeconds(1.2f); // 延迟1秒
        setisWalking(); // 然后调用setisWalking
    }
    private IEnumerator DelayedAttacking()
    {
        yield return new WaitForSeconds(0.7f); // 延迟1秒
        Vector3 Scalex = new Vector3(0, 0, 0);
        Vector3 generalAttackOffset = new Vector3(10, 15, 0);
        // 计算攻击触发器的位置
        Vector3 spawnPosition = transform.position + generalAttackOffset + Scalex * Attackrange; // 根据角色方向计算生成位置
        GameObject attackTrigger = Instantiate(GeneralattackPrefab, spawnPosition, Quaternion.identity);
        attackTrigger.tag = "Eattack";
        Destroy(attackTrigger, 0.2f); // 1秒后销毁
    }
    private IEnumerator DelayedFiring()
    {
        yield return new WaitForSeconds(0.5f); // 延迟1秒
        GameObject missly = Instantiate(misslyPrefab, transform.position + 2 * offset, Quaternion.identity);
    }
    private IEnumerator DelayedDowning()
    {
        float speed = 100f; // 移动持续时间

        while (transform.position.y > ground)
        {
            // 每帧移动一定的距离
            float moveDistance = speed * Time.deltaTime; // 根据需要调整移动的速度
            transform.position -= new Vector3(0, moveDistance, 0); // 向下移动
            yield return null; // 等待下一帧
        }
        GameObject missly1 = Instantiate(leftwavePrefab, transform.position, Quaternion.identity);
        GameObject missly2 = Instantiate(rightwavePrefab, transform.position, Quaternion.identity);
        Destroy(missly1, 3f);
        Destroy(missly2, 3f);
    }
    void generalAttack()
    {
        setisAttacking();
        StartCoroutine(DelayedAttacking());
        StartCoroutine(DelayedWalking());
    }
    void Fire()
    {
        setisisFiring();
        StartCoroutine(DelayedFiring());
    }
    void Down()
    {
        setisisFlying();
        FlyUp();
        setisDowning();
        StartCoroutine(DelayedDowning());
    }
    void FlyUp()
    {
        Vector3 target = new Vector3(transform.position.x,transform.position.y + 100, transform.position.z);
        transform.position = target;
    }
    void MoveTowardsPlayer()
    {
        if ((player.position.x - transform.position.x) * transform.localScale.x < 0)
        {
            Vector3 nowScale = transform.localScale;
            nowScale.x *= -1;
            transform.localScale = nowScale;
            //Debug.Log("Opp");
        }
        Vector3 targetPosition = new Vector3(player.position.x - offset.x, transform.position.y, transform.position.z);
        transform.position = Vector3.MoveTowards(transform.position, targetPosition, moveSpeed * Time.deltaTime);
    }
    void FlyTowardsPlayer()
    {
        setisisFlying();
        if ((player.position.x - transform.position.x) * transform.localScale.x < 0)
        {
            Vector3 nowScale = transform.localScale;
            nowScale.x *= -1;
            transform.localScale = nowScale;
        }
        transform.position = Vector3.MoveTowards(transform.position, player.position - offset, moveSpeed * Time.deltaTime);
    }

}

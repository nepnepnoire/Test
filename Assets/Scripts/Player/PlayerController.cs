using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEditor.PlayerSettings;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    private bool isControlEnabled ; // 控制是否启用
    public Rigidbody2D rb;
    private PhysicsCheck physicsCheck;
    public Vector2 inputDirection;
    
    [Header("基本参数")]
    public float speed;
    public float jumpSpeed;
    public float dashSpeed;//冲刺力
    public float dashDis;
    public float dashDuration;//冲刺持续时间
    public float currentSpeed;//当前速度
    public int maxHealth = 10;
    public int currentHealth = 10;
    
    
    [Header("人物状态")]
    public bool isAttacking = false;//是否正在攻击

    public bool dashingCondition = false;//是否可以冲刺
    public bool isDashing = false;//
    public float DashStartTimer;//冲刺计时器
    public float DashCDStartTimer;//冲刺冷却
    public float DashCD;
    [Header("吸附和蹬墙跳参数")]
    public bool unlockAttach = false;
    public bool isAttaching;//是否吸附
    public bool attachCondition;//吸附条件判断
    public float slideDownSpeed;//下滑速度
    public float attachedJumpSpeed;//蹬墙跳速度
    private float wallJumpCooldown = 0.5f; // 蹬墙跳后的冷却时间
    private float lastWallJumpTime = -1.0f; // 上次蹬墙跳的时间
    private bool canInput = true; // 控制是否可以输入

    [Header("滑翔参数")]
    public bool unlockGlide = false;
    public float glidingSpeed;
    public bool glideCondition;
    public bool isGliding;

    [Header("钩锁参数")]
    public bool unlockHook = false;
    public float grappleDistance = 10f; // 抓钩的最大距离
    public LayerMask hookLayer; // 钩锁点的层
    public bool isGrappling = false; // 抓钩状态
    public float pullForce = 10f; // 拉动的力量
    public Vector3 grapplePoint; // 抓钩位置

    private Vector2 checkpointPosition; // 存储检查点位置
    private bool isDead = false; // 玩家是否死亡
    [Header("攻击参数")]
    public GameObject attackTriggerPrefab; // 用于设置攻击触发器的预制体
    public float attackRange = 5f; // 攻击范围
    private float lastAttackTime;
    [Header("受伤无敌")]
    public float invulnerableDuration;
    private float invulnerableCounter;
    public bool invulnerable;
    public int FixedScale;
    public float knock_backSpeed;//击退水平速度
    public float knock_upSpeed;//击退竖直速度



    public void Awake()
    {
        physicsCheck = GetComponent<PhysicsCheck>();
        
    }

    public void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        checkpointPosition = transform.position; // 初始化检查点为当前玩家位置
        dashingCondition = !physicsCheck.isGround;
        glideCondition = !physicsCheck.isGround;
        isControlEnabled = true;
    }

    public void DisableControls()
    {
        isControlEnabled = false; // 禁用控制
    }

    public void EnableControls()
    {
        isControlEnabled = true; // 启用控制
    }

    void Update()

    {
        if (isControlEnabled && !isDead) // 只有在控制启用且未死亡时才处理输入
        {
            

            // 示例：检测玩家死亡
            if (Input.GetKeyDown(KeyCode.R)) // 假设 R 键用于死亡
            {
                Die();
            }
        }
        //硬直状态无法操作
        if (invulnerable)
        {
            InvulnerableCount();
            knock_back();
        }
        else {
            if (isControlEnabled)
            {
                if (unlockHook)
                {
                    HandleHook();
                }
                if (unlockAttach)
                {
                    HandleAttach();
                }
                DashContinue();
                HandleJump();
                HandleDash();
                HandleAttack();
                if (unlockGlide)
                {

                    HandleGlide();
                }

                if (currentHealth <= 0)
                {
                    Debug.Log("Game Over!");
                    gameObject.SetActive(false);
                }
            }
        }

        
        
        
    }

    public void TriggerInvulnerable()
    {
        if (!invulnerable)
        {
            invulnerable = true;
            FixedScale = -(int)transform.localScale.x;
            invulnerableCounter = invulnerableDuration;
        }
    }
    public void knock_back()
    {

        rb.velocity = new Vector2(FixedScale * knock_backSpeed, 0);
    }
    private void InvulnerableCount()
    {
        if (invulnerable)
        {
            invulnerableCounter -= Time.deltaTime;
            if (invulnerableCounter <= 0)
            {
                invulnerable = false;
            }
        }
    }


    private void FixedUpdate()
    {
        if (isControlEnabled&&!invulnerable) 
        {
            HandleMovement(); 
        }
        
    }
    private void HandleMovement()
    {
        float moveInput = 0;
        if (Input.GetKey("a")) moveInput = -1; // 向左
        if (Input.GetKey("d") && !physicsCheck.isrightWall) moveInput = 1;  // 向右
        if (isDashing && transform.localScale.x * moveInput > 0)
        {
            return;
        };
        if (moveInput != 0)
        {
            //和dash冲突处理
            if (!Input.GetKey(KeyCode.LeftShift))
            {
                currentSpeed = Mathf.Lerp(currentSpeed, speed * moveInput, Time.deltaTime * 10);
                rb.velocity = new Vector2(currentSpeed * Time.deltaTime, rb.velocity.y);
            }
        }

        else
        {
            currentSpeed = Mathf.Lerp(currentSpeed, 0, Time.deltaTime * 10);
        }


        //人物反转
        int faceDir = (int)transform.localScale.x;

        if (moveInput > 0)
        {
            faceDir = 1;
        }
        else if (moveInput < 0)
        {
            faceDir = -1;
        }
        transform.localScale = new Vector3(faceDir, 1, 1);

        //冲突处理
        //&dash

    }

    public void HandleJump()
    {
        // 检查是否在蹬墙跳冷却时间内
        if (!canInput && Time.time < lastWallJumpTime + wallJumpCooldown)
        {
            // 禁用相同方向的输入
            if ((Input.GetKey(KeyCode.A) && rb.velocity.x < 0) || (Input.GetKey(KeyCode.D) && rb.velocity.x > 0))
            {
                return; // 禁止输入
            }
        }
        if (Input.GetKey(KeyCode.Space))
        {
            if (physicsCheck.isGround)//跳起来之后
            {
                rb.velocity = new Vector2(rb.velocity.x, jumpSpeed);
                dashingCondition = true;
            }
            else if (isAttaching)//附着的时候
            {
                Debug.Log("wallJumping");
                rb.velocity = new Vector2(attachedJumpSpeed * -rb.transform.localScale.x, jumpSpeed);
                transform.localScale = new Vector3(-rb.transform.localScale.x, 1, 1);
                isAttaching = false;
                lastWallJumpTime = Time.time; // 更新上次蹬墙跳的时间
                canInput = false; // 禁用相同方向的输入
            }
        }
        else
        {
            // 如果没有按下空格键，重置输入状态
            canInput = true;
        }
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        dashingCondition = false;
    }

    private void HandleDash()
    {
        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            if (dashingCondition)
            {
                rb.velocity = new Vector2((int)transform.localScale.x * dashSpeed, 0);
                dashingCondition = false;
                isDashing = true;
                DashStartTimer = dashDuration;
                DashCDStartTimer = DashCD;
            }
        }
    }
    public void DashContinue()
    {
        if (isDashing)
        {
            DashStartTimer -= Time.deltaTime;
            if (DashStartTimer <= 0)
            {
                isDashing = false;
                rb.velocity = new Vector2((int)transform.localScale.x, 0);
            }
            else
            {
                rb.velocity = new Vector2((int)transform.localScale.x * dashSpeed, 0);
            }
        }
    }
    private void HandleAttack()
    {
        if (Input.GetMouseButtonDown(0))
        {
            if (isAttacking == true)
            {
                //B动作
                if (Time.time - lastAttackTime < 0.35)
                {
                    Debug.Log("Attack B");

                }
                isAttacking = false;//重置攻击状态

            }
            else if (isAttacking == false)
            {
                //播放A动作
                Debug.Log("Attack A");
                isAttacking = true;
                attackA();
                lastAttackTime = Time.time;
            }
        }
    }
    public void attackA()
    {
        Vector3 Scalex = new Vector3((int)transform.localScale.x, 0, 0);
        // 计算攻击触发器的位置
        Vector3 spawnPosition = transform.position + Scalex * attackRange; // 根据角色方向计算生成位置

        // 生成攻击触发器
        GameObject attackTrigger = Instantiate(attackTriggerPrefab, spawnPosition, Quaternion.identity);

        // 设置攻击触发器的标签
        attackTrigger.tag = "Pattack";
        Destroy(attackTrigger, 0.2f); // 1秒后销毁

    }
    public void Interact(Interact interactor)
    {
        Debug.Log("Press E to interact");
        
    }

    private void HandleAttach()
    {
        /*// 检查是否在冷却时间内
        if (!isAttaching&& Time.time < lastAttachTime + attachCD)
        {
            isAttaching = false; // 在冷却期间禁止附着
            return;
        }*/

        attachCondition = (!physicsCheck.isGround) && (physicsCheck.isleftWall || physicsCheck.isrightWall);//离地且有墙


        if (attachCondition == true)
        {
            //附着
            if ((Input.GetKey(KeyCode.A) && physicsCheck.isleftWall) || (Input.GetKey(KeyCode.D) && physicsCheck.isrightWall))
            {
                //Debug.Log("Attach");
                isAttaching = true;
                rb.velocity = new Vector2(0, slideDownSpeed);
                //lastAttachTime = Time.time;

            }
            else
            {
                // 离开附着状态
                if (isAttaching)
                {
                    isAttaching = false; // 更新附着状态
                    //lastAttachTime = Time.time; // 更新离开附着时间
                }
            }

        }
        else
        {
            // 离开附着状态
            if (isAttaching)
            {
                isAttaching = false; // 更新附着状态
                //lastAttachTime = Time.time; // 更新离开附着时间
            }
        }
    }
    private void HandleGlide()
    {
        glideCondition = (!physicsCheck.isGround) && (rb.velocity.y < 0) && (!isAttaching);
        if (glideCondition == true)
        {
            if (Input.GetKey(KeyCode.Space))
            {
                //Debug.Log("Glide");
                isGliding = true;
                rb.velocity = new Vector2(rb.velocity.x, glidingSpeed);
            }
        }
        else isGliding = false;
    }

    private void HandleHook()
    {
        if(physicsCheck.isGrapplePoint) {
            //Debug.Log("Find hookpoint");
            if (Input.GetKey(KeyCode.F)) // 按“E”键抓钩
            {
                canInput = false;
                Grapple();
                dashingCondition = true;
            }

            /*if (isGrappling)
            {

                
                
                    ReleaseGrapple();
                
            }*/
        }
        
    }
    private void Grapple()
    {
        // 获取抓钩点的碰撞体
        Collider2D grappleCollider = Physics2D.OverlapCircle((Vector2)transform.position + physicsCheck.HookOffset, physicsCheck.checkHookRaduis, physicsCheck.HookLayer);
        grapplePoint = grappleCollider.transform.position;
        isGrappling = true;
        // 施加力将角色拉向抓钩点
        //Vector2 direction = (grapplePoint - transform.position).normalized; // 计算方向
        //rb.AddForce(direction * pullForce, ForceMode2D.Impulse); // 施加力
        transform.position = Vector2.MoveTowards(transform.position, grapplePoint,200*Time.deltaTime);
        rb.gravityScale = 0;
        if(transform.position == grapplePoint)
        {
            rb.gravityScale = 15;
            rb.velocity = new Vector2(speed*(int)transform.localScale.x, jumpSpeed* (int)transform.localScale.y);
             
            canInput = true;
        }
    }


        

    /*private void ReleaseGrapple()
    {
        isGrappling = false;
        rb.gravityScale = 15;
    }*/

    public void SetCheckpoint(Vector2 position)
    {
        checkpointPosition = position; // 更新检查点位置
    }

    public void Die()
    {
        isDead = true; // 设置死亡状态
        transform.position = checkpointPosition; // 复活到检查点位置
        isDead = false; // 恢复状态
    }

}



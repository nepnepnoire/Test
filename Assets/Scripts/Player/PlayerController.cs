using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEditor.PlayerSettings;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
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
    [Header("受伤无敌")]
    public float invulnerableDuration;
    private float invulnerableCounter;
    public bool invulnerable;
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


    private float lastAttackTime;

    public void Awake()
    {
        physicsCheck = GetComponent<PhysicsCheck>();
    }

    public void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        dashingCondition = !physicsCheck.isGround;
        glideCondition = !physicsCheck.isGround;

    }

    void Update()
    {
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



    private void FixedUpdate()
    {
        HandleMovement();
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
                lastAttackTime = Time.time;
            }
        }
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
}

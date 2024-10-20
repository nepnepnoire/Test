using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
public class PlayerController : MonoBehaviour
{
    public PlayerInputControl inputControl;
    public Rigidbody2D rb;
    private PhysicsCheck physicsCheck;
    public Vector2 inputDirection;
    [Header("基本参数")]
    public float speed;
    public float jumpForce;
    public float dashSpeed;//冲刺速度
    public float dashDuration;//冲刺持续时间
    public float currentSpeed;//当前速度
    public bool isAttacking = false;//是否正在攻击

    private float lastAttackTime;
    //交互、攻击、钩锁
    private void Awake()
    {
        inputControl = new PlayerInputControl();
        physicsCheck = GetComponent<PhysicsCheck>();
    }
    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }





    private void OnEnable()
    {
        inputControl.Enable();
    }

    private void OnDisable()
    {
        inputControl.Disable();
    }

    private void Update()
    {
        inputDirection = inputControl.Gameplay.Move.ReadValue<Vector2>();
        inputControl.Gameplay.Jump.started += Jump;
        //HandleDash();
        HandleAttack();

    }

    //冲刺
    

    private void HandleAttack()
    {
        if(Input.GetMouseButtonDown(0)) 
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





    //测试
    /*private void OnTriggerStay2D(Collider2D collision)
    {
        Debug.Log(collision.name);
    }*/
    private void FixedUpdate()
    {
        Move();
        
    }
    public void Move()
    {
        if(inputDirection.x!=0) {
            currentSpeed = Mathf.Lerp(currentSpeed, speed * inputDirection.x, Time.deltaTime * 10);
            rb.velocity = new Vector2(currentSpeed * Time.deltaTime, rb.velocity.y);
            int faceDir = (int)transform.localScale.x;
            if (inputDirection.x > 0)
            {
                faceDir = 1;
            }
            else if (inputDirection.x < 0)
            {
                faceDir = -1;
            }
            //人物反转
            transform.localScale = new Vector3(faceDir, 1, 1);
        }
        else
        {
            currentSpeed = Mathf.Lerp(currentSpeed, 0, Time.deltaTime * 10);
            rb.velocity = new Vector2(currentSpeed * Time.deltaTime, rb.velocity.y);
        }
        
        
    }
    //跳跃
    private void Jump(InputAction.CallbackContext context)
    {
        if (physicsCheck.isGround)
        {
            rb.AddForce(transform.up * jumpForce, ForceMode2D.Impulse);
            physicsCheck.isDashing = true;
        }
        
    }
    //空中冲刺
    private void Dash(InputAction.CallbackContext context)
    {
        
       if(false==physicsCheck.isGround&&true==physicsCheck.isDashing)
        {
            Debug.Log("Dash");
            currentSpeed = dashSpeed * Time.deltaTime * inputDirection.x;
            rb.velocity = new Vector2(currentSpeed, rb.velocity.y);
            physicsCheck.isDashing = false;
            //弱震屏效果
        }
    }

    //交互
    public void Interact(Interact interactor)
    {
        Debug.Log("Press E to interact");
    }

    //攻击
    public void PlayerAttack()
    {
        
        
    }
} 

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEditor.PlayerSettings;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour {

    

    public Rigidbody2D rb;
    private PhysicsCheck physicsCheck;
    public Vector2 inputDirection;
    public Vector3 pos;
    [Header("基本参数")]
    public float speed;
    public float jumpSpeed;
    public float dashSpeed;//冲刺力
    public float dashDis;
    public float dashDuration;//冲刺持续时间
    public float currentSpeed;//当前速度
    [Header("人物状态")]
    public bool isAttacking = false;//是否正在攻击
                                    
    public bool dashingCondition = false;//是否可以冲刺

    private float lastAttackTime;


    public void Awake()
    {
        physicsCheck = GetComponent<PhysicsCheck>();
    }

    public void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        dashingCondition = !physicsCheck.isGround;
        
    }


    void Update () {
        HandleDash();
        HandleJump();
        HandleAttack();
    }

    private void FixedUpdate()
    {
        HandleMovement();

    }
    private void HandleMovement()
    {

        float moveInput = 0;

        if (Input.GetKey("a")) moveInput = -1; // 向左
        if (Input.GetKey("d")) moveInput = 1;  // 向右

        if (moveInput != 0)
        {
            //和dash冲突处理
            if (!Input.GetKey(KeyCode.LeftShift))
            {
                currentSpeed = Mathf.Lerp(currentSpeed, speed * moveInput, Time.deltaTime * 10);
                rb.velocity = new Vector2(currentSpeed * Time.deltaTime, rb.velocity.y);
            }
            else HandleDash();
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
        if (Input.GetKey(KeyCode.Space))
        {
            if (physicsCheck.isGround)//跳起来之后
            {
                rb.velocity = new Vector2(rb.velocity.x, jumpSpeed);
                dashingCondition = true;
            }
        }
    }
    private void HandleDash()
    {
        pos = transform.localPosition;
        if (physicsCheck.isGround) dashingCondition = false;
        if(Input.GetKey(KeyCode.LeftShift)) 
        {
            if (dashingCondition)
            {
                Debug.Log("Dashing!");
               pos.x += (int)transform.localScale.x * dashDis;
                transform.localPosition = pos;
               //rb.velocity = new Vector2((int)transform.localScale.x * dashSpeed, rb.velocity.y);
               dashingCondition = false;

            }
            else;
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



    /*void CheckPlayerInput()
    {

        walk_left = Input.GetKey(KeyCode.A);

        walk_right = Input.GetKey(KeyCode.D);

        walk = walk_right || walk_left;

        jump = Input.GetKeyDown(KeyCode.W);
    }*/
    /*void UpdatePlayerPosition() {

		Vector3 pos = transform.localPosition;
		Vector3 scale = transform.localScale;

		if (walk) {
			if (walk_left) {
				pos.x -= velocity.x * Time.deltaTime;
				scale.x = -1;
            }

			if (walk_right) {
				pos.x += velocity.x * Time.deltaTime;
				scale.x = 1;
            }
            //pos = CheckWallRays (pos, scale.x);
        }

		if (jump && (playerState != PlayerState.jumping)) {
			playerState = PlayerState.jumping;
			velocity = new Vector2 (velocity.x, jumpvelocity);
		}

		if (playerState == PlayerState.jumping) {
			pos.y += velocity.y * Time.deltaTime;
			velocity.y -= gravity * Time.deltaTime;
		}

		if (velocity.y <= 0)  
			pos = CheckFloorRays (pos);

		transform.localPosition = pos;
		transform.localScale = scale;
		walk = false;
		walk_left = false;
		walk_right = false;
		jump = false;

    }*/





}

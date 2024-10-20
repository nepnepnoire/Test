using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEditor.PlayerSettings;

public class Player : MonoBehaviour {


    public Rigidbody2D rb;
    private PhysicsCheck physicsCheck;
    public Vector2 inputDirection;
    [Header("基本参数")]
    public float speed;
    public float jumpForce;
    public float dashForce;//冲刺力
    public float dashDuration;//冲刺持续时间
    public float currentSpeed;//当前速度
    [Header("人物状态")]
    public bool isAttacking = false;//是否正在攻击
    //public bool isDashing = true;//是否可以冲刺,true是可以，false是不可以

    private float lastAttackTime;


    public void Awake()
    {
        physicsCheck = GetComponent<PhysicsCheck>();
    }

    public void Start()
    {
        rb = GetComponent<Rigidbody2D>();   
    }


    void Update () {
        HandleMovement();
        HandleJump();
        HandleDash();
        HandleAttack();
    }


    private void HandleMovement()
    {
        float moveInput = 0;

        if (Input.GetKey("a")) moveInput = -1; // 向左
        if (Input.GetKey("d")) moveInput = 1;  // 向右

        if (moveInput != 0)
        {
            currentSpeed = Mathf.Lerp(currentSpeed, speed * moveInput, Time.deltaTime * 10);
            transform.Translate(Vector2.right * currentSpeed * Time.deltaTime);
        }
        else
        {
            currentSpeed = Mathf.Lerp(currentSpeed, 0, Time.deltaTime * 3);
        }

        int faceDir = (int)transform.localScale.x;

        if (moveInput > 0)
        {
            faceDir = 1;
        }
        else if (moveInput < 0)
        {
            faceDir = -1;
        }
        //人物反转
        transform.localScale = new Vector3(faceDir, 1, 1);

    }

    public void HandleJump()
    {
        if (Input.GetKey(KeyCode.Space))
        {
            if (physicsCheck.isGround)
            {
                
                rb.AddForce(transform.up * jumpForce, ForceMode2D.Impulse);
                physicsCheck.isDashing = true;
            }
        }
    }
    private void HandleDash()
    {
        if(Input.GetKey(KeyCode.LeftShift)) 
        {
            if(physicsCheck.isDashing)
            {
                rb.AddForce(transform.right * dashForce, ForceMode2D.Impulse);
                physicsCheck.isDashing = false;
                
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

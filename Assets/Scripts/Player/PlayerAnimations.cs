using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimations : MonoBehaviour
{
    private Animator anim;
    private Rigidbody2D rb;
    public PlayerController playerController;
    public PhysicsCheck physicsCheck;

    private void Awake()
    {
        anim = GetComponent<Animator>();
        playerController = GetComponent<PlayerController>();
        physicsCheck = GetComponent<PhysicsCheck>();
        rb = GetComponent<Rigidbody2D>();
    }
    public void Update()
    {
        SetAnimation();
    }
    public void SetAnimation()
    {
        anim.SetFloat("currentSpeed", Mathf.Abs(playerController.currentSpeed));
        anim.SetBool("isGround", physicsCheck.isGround);
        anim.SetBool("isDashing", playerController.isDashing);
        anim.SetBool("isAttaching", playerController.isAttaching);
        anim.SetFloat("yËÙ¶È",rb.velocity.y);
        anim.SetBool("isGliding", playerController.isGliding);
    }
}

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
    }
}

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerState : MonoBehaviour
{
    [Header("ÌøÔ¾×´Ì¬")]
    public bool jumpingCondition = false;
    public bool isJumping = false;//

    [Header("³å´Ì×´Ì¬")]
    public bool dashingCondition = false;//ÊÇ·ñ¿ÉÒÔ³å´Ì
    public bool isDashing = false;//
    
    [Header("Îü¸½×´Ì¬")]
    public bool isAttaching;//ÊÇ·ñÎü¸½
    public bool attachCondition;//Îü¸½Ìõ¼þÅÐ¶Ï

    [Header("¹¥»÷×´Ì¬")]
    public bool isAttacking = false;//ÊÇ·ñÕýÔÚ¹¥»÷
    //private float lastAttackTime;

    private PhysicsCheck physicsCheck;

    public void Awake()
    {
        physicsCheck = GetComponent<PhysicsCheck>();
    }

    public void Update()
    {
        CheckPlayerState();
    }

    private void CheckPlayerState()
    {
        jumpingCondition = physicsCheck.isGround;
        dashingCondition = !physicsCheck.isGround;
        attachCondition = physicsCheck.isGround&&(physicsCheck.isrightWall||physicsCheck.isrightWall);

        

    }
}

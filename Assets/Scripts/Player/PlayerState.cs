using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerState : MonoBehaviour
{
    [Header("��Ծ״̬")]
    public bool jumpingCondition = false;
    public bool isJumping = false;//

    [Header("���״̬")]
    public bool dashingCondition = false;//�Ƿ���Գ��
    public bool isDashing = false;//
    
    [Header("����״̬")]
    public bool isAttaching;//�Ƿ�����
    public bool attachCondition;//���������ж�

    [Header("����״̬")]
    public bool isAttacking = false;//�Ƿ����ڹ���
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

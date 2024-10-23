using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControllerUpdate : MonoBehaviour
{
    public enum PlayerState
    {
        idle,
        walk,
        dash,
        jump,
        attack,
        damaged,
        attached
    }

    private PlayerState currentState;

    private float damagedTime = 1f;
    private float damagedTimer;

    private void Start()
    {
        currentState = PlayerState.idle;
    }

    private void Update()
    {
        HandleState();
        HandleInput();
    }

    private void HandleState()
    {
        switch (currentState)
        {
            case PlayerState.idle:
                Debug.Log("idle");
                break;
            case PlayerState.walk:
                 Debug.Log("walk");
                break;
            case PlayerState.dash:
                Debug.Log("dash");
                break;
            case PlayerState.jump:
                Debug.Log("jump");
                break;
            case PlayerState.attack:
                Debug.Log("attack");
                break;
            case PlayerState.damaged:
                HandleDamagedState();
                break;
            case PlayerState.attached:
                Debug.Log("attached");
                break;
        }
    }
    private void HandleInput()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Jump();
        }
        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            Dash();
        }
        if (Input.GetKeyDown(KeyCode.E))
        {
            Attach();
        }
        if (Input.GetKeyDown(KeyCode.F))
        {
            Attack();
        }
        if (Input.GetKeyDown(KeyCode.G))
        {
            TakeDamage();
        }
        if (Input.GetKeyDown(KeyCode.W) && currentState == PlayerState.idle)
        {
            currentState = PlayerState.walk;
        }
    }
    private void HandleDamagedState()
    {
        damagedTimer += Time.deltaTime;
        if (damagedTimer >= damagedTime)
        {
            currentState = PlayerState.idle;
            damagedTimer = 0;
        }
    }

    public void Jump()
    {
        if (currentState == PlayerState.idle || currentState == PlayerState.walk)
        {
            currentState = PlayerState.jump;
        }
    }

    public void Dash()
    {
        if (currentState == PlayerState.jump)
        {
            currentState = PlayerState.dash;
        }
    }
    public void Attach()
    {
        if (currentState == PlayerState.jump)
        {
            currentState = PlayerState.attached;
        }
    }

    public void TakeDamage()
    {
        if (currentState != PlayerState.damaged)
        {
            currentState = PlayerState.damaged;
        }
    }

    public void Attack()
    {
        if (currentState == PlayerState.idle || currentState == PlayerState.walk)
        {
            currentState = PlayerState.attack;
        }
    }
}

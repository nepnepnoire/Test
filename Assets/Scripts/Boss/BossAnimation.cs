using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossAnimation : MonoBehaviour
{
    // Start is called before the first frame update
    Animator anim;
    Boss Boss;
    private void Awake()
    {
        anim = GetComponent<Animator>();
        Boss = GetComponent<Boss>();
    }
    public void Update()
    {
        SetAnimation();
        
    }
    public void SetAnimation()
    {
        anim.SetBool("Underattacked", Boss.isUnderattack);
        anim.SetBool("ishalfHP", Boss.halfHP);
        anim.SetBool("isFlying", Boss.isFlying);
        anim.SetBool("isAttacking", Boss.isAttacking);
        anim.SetBool("isFiring", Boss.isFiring);
        anim.SetBool("isWalking", Boss.isWalking);
        anim.SetBool("Die", Boss.Die);
        anim.SetBool("isDowning", Boss.isDowning);
    }
}

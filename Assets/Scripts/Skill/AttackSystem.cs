using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackSystem : MonoBehaviour
{
    // Start is called before the first frame update
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Pattack")
        {
            Enemy enemy = gameObject.GetComponent<Enemy>();
            if (enemy != null && enemy.enemystate!=Enemy.EnemyState.attacked)
            {
                // 调用 enemy 的方法或修改其属性
                enemy.health -= 1;
                enemy.TriggerInvulnerable();
            }
        }else if(collision.gameObject.tag == "Eattack")
        {
            PlayerController player = gameObject.GetComponent<PlayerController>();
            if (player != null && !player.invulnerable)
            {
                // 调用 player 的方法或修改其属性
                player.currentHealth -= 1;
                player.TriggerInvulnerable();
            }
        }
    }
}

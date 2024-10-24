using UnityEngine;

public class Checkpoint : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player")) // 确保是玩家
        {
            PlayerController playerController = collision.GetComponent<PlayerController>();
            if (playerController != null)
            {
                playerController.SetCheckpoint(transform.position); // 设置检查点
            }
        }
    }
}
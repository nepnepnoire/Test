using UnityEngine;

public class NPC : MonoBehaviour
{
    public string[] dialogue; // 对话内容
    private DialogueManager dialogueManager;
    private PlayerController playerController; // 玩家控制器引用
    private bool playerInRange = false; // 玩家是否在范围内

    private void Start()
    {
        dialogueManager = FindObjectOfType<DialogueManager>(); // 获取对话管理器
        playerController = FindObjectOfType<PlayerController>(); // 获取玩家控制器
    }

    private void Update()
    {
        if (playerInRange && Input.GetKeyDown(KeyCode.E)) // 检测 E 键
        {
            playerController.DisableControls(); // 禁用玩家控制
            dialogueManager.StartDialogue(dialogue); // 开始对话
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player")) // 确保是玩家
        {
            playerInRange = true; // 玩家进入范围
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            playerInRange = false; // 玩家离开范围
            dialogueManager.EndDialogue(); // 结束对话
            playerController.EnableControls(); // 确保控制在对话结束时启用
        }
    }
}
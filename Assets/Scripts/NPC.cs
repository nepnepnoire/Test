using UnityEngine;

public class NPC : MonoBehaviour
{
    public string[] dialogue; // �Ի�����
    private DialogueManager dialogueManager;
    private PlayerController playerController; // ��ҿ���������
    private bool playerInRange = false; // ����Ƿ��ڷ�Χ��

    private void Start()
    {
        dialogueManager = FindObjectOfType<DialogueManager>(); // ��ȡ�Ի�������
        playerController = FindObjectOfType<PlayerController>(); // ��ȡ��ҿ�����
    }

    private void Update()
    {
        if (playerInRange && Input.GetKeyDown(KeyCode.E)) // ��� E ��
        {
            playerController.DisableControls(); // ������ҿ���
            dialogueManager.StartDialogue(dialogue); // ��ʼ�Ի�
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player")) // ȷ�������
        {
            playerInRange = true; // ��ҽ��뷶Χ
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            playerInRange = false; // ����뿪��Χ
            dialogueManager.EndDialogue(); // �����Ի�
            playerController.EnableControls(); // ȷ�������ڶԻ�����ʱ����
        }
    }
}
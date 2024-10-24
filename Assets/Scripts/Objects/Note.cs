using UnityEngine;

public class Note : MonoBehaviour
{
    public string noteContent; // ֽ������
    public GameObject noteUI; // ��ʾֽ�����ݵ� UI
    private bool isPlayerInRange = false; // ����Ƿ��ڷ�Χ��
    private PlayerController playerController; // ��ҿ���������

    private void Start()
    {
        playerController = FindObjectOfType<PlayerController>(); // ��ȡ��ҿ�����
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player")) // ȷ�������
        {
            isPlayerInRange = true; // ��ҽ��뷶Χ
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player")) // ȷ�������
        {
            isPlayerInRange = false; // ����뿪��Χ
            //CloseNote(); // �ر� UI
        }
    }

    private void Update()
    {
        if (isPlayerInRange && Input.GetKeyDown(KeyCode.E)) // ����Ƿ��ڷ�Χ�ڲ����� E ��
        {
            ShowNote();
        }
    }

    private void ShowNote()
    {
        noteUI.SetActive(true); // �� UI
        noteUI.GetComponentInChildren<TMPro.TextMeshProUGUI>().text = noteContent; // �����ı�
        playerController.DisableControls(); // ������ҿ���
    }

    public void CloseNote()
    {
        noteUI.SetActive(false); // �ر� UI
        playerController.EnableControls(); // ������ҿ���
        Destroy(gameObject); // ����ֽ������
    }
}
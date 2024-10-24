using UnityEngine;

public class Note : MonoBehaviour
{
    public string noteContent; // 纸条内容
    public GameObject noteUI; // 显示纸条内容的 UI
    private bool isPlayerInRange = false; // 玩家是否在范围内
    private PlayerController playerController; // 玩家控制器引用

    private void Start()
    {
        playerController = FindObjectOfType<PlayerController>(); // 获取玩家控制器
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player")) // 确保是玩家
        {
            isPlayerInRange = true; // 玩家进入范围
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player")) // 确保是玩家
        {
            isPlayerInRange = false; // 玩家离开范围
            //CloseNote(); // 关闭 UI
        }
    }

    private void Update()
    {
        if (isPlayerInRange && Input.GetKeyDown(KeyCode.E)) // 检查是否在范围内并按下 E 键
        {
            ShowNote();
        }
    }

    private void ShowNote()
    {
        noteUI.SetActive(true); // 打开 UI
        noteUI.GetComponentInChildren<TMPro.TextMeshProUGUI>().text = noteContent; // 设置文本
        playerController.DisableControls(); // 禁用玩家控制
    }

    public void CloseNote()
    {
        noteUI.SetActive(false); // 关闭 UI
        playerController.EnableControls(); // 启用玩家控制
        Destroy(gameObject); // 销毁纸条对象
    }
}
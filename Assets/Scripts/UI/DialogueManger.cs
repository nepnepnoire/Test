using UnityEngine;
using UnityEngine.UI;  // 如果使用 Unity UI
using TMPro;         // 如果使用 TextMeshPro
using System.Collections.Generic; // 确保导入这个命名空间

public class DialogueManager : MonoBehaviour
{
    public GameObject dialoguePanel;     // 对话框 Panel
    public TextMeshProUGUI dialogueText; // 对话文本组件
    public Button continueButton;         // 继续按钮
    private PlayerController playerController; // 玩家控制器引用

    private Queue<string> sentences;      // 存储对话句子

    private void Start()
    {
        sentences = new Queue<string>();
        continueButton.onClick.AddListener(ContinueDialogue);
        dialoguePanel.SetActive(false); // 确保对话框在开始时隐藏
        playerController = FindObjectOfType<PlayerController>(); // 获取玩家控制器
    }

    public void StartDialogue(string[] dialogue)
    {
        sentences.Clear();
        foreach (string sentence in dialogue)
        {
            sentences.Enqueue(sentence); // 将句子加入队列
        }
        dialoguePanel.SetActive(true); // 显示对话框
        DisplayNextSentence(); // 显示第一句
    }

    public void ContinueDialogue()
    {
        if (sentences.Count > 0)
        {
            DisplayNextSentence();
        }
        else
        {
            EndDialogue();
        }
    }

    public void DisplayNextSentence()
    {
        string sentence = sentences.Dequeue(); // 从队列中取出下一句
        dialogueText.text = sentence; // 显示句子
    }

    public void EndDialogue()
    {
        dialoguePanel.SetActive(false); // 关闭对话框
        playerController.EnableControls(); // 结束对话时启用玩家控制
    }
}
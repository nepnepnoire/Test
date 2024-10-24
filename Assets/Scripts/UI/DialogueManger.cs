using UnityEngine;
using UnityEngine.UI;  // ���ʹ�� Unity UI
using TMPro;         // ���ʹ�� TextMeshPro
using System.Collections.Generic; // ȷ��������������ռ�

public class DialogueManager : MonoBehaviour
{
    public GameObject dialoguePanel;     // �Ի��� Panel
    public TextMeshProUGUI dialogueText; // �Ի��ı����
    public Button continueButton;         // ������ť
    private PlayerController playerController; // ��ҿ���������

    private Queue<string> sentences;      // �洢�Ի�����

    private void Start()
    {
        sentences = new Queue<string>();
        continueButton.onClick.AddListener(ContinueDialogue);
        dialoguePanel.SetActive(false); // ȷ���Ի����ڿ�ʼʱ����
        playerController = FindObjectOfType<PlayerController>(); // ��ȡ��ҿ�����
    }

    public void StartDialogue(string[] dialogue)
    {
        sentences.Clear();
        foreach (string sentence in dialogue)
        {
            sentences.Enqueue(sentence); // �����Ӽ������
        }
        dialoguePanel.SetActive(true); // ��ʾ�Ի���
        DisplayNextSentence(); // ��ʾ��һ��
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
        string sentence = sentences.Dequeue(); // �Ӷ�����ȡ����һ��
        dialogueText.text = sentence; // ��ʾ����
    }

    public void EndDialogue()
    {
        dialoguePanel.SetActive(false); // �رնԻ���
        playerController.EnableControls(); // �����Ի�ʱ������ҿ���
    }
}
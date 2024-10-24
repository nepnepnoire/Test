using UnityEngine;
using UnityEngine.UI;

public class PauseMenu : MonoBehaviour
{
    public GameObject pauseMenuUI; // ���������ͣ�˵� Panel
    private bool isPaused = false;

    void Update()
    {
        // ��� ESC ��
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (isPaused)
            {
                Resume();
            }
            else
            {
                Pause();
            }
        }
    }

    public void Resume()
    {
        pauseMenuUI.SetActive(false); // ������ͣ�˵�
        Time.timeScale = 1f; // �ָ���Ϸʱ��
        isPaused = false;
    }

    public void Pause()
    {
        pauseMenuUI.SetActive(true); // ��ʾ��ͣ�˵�
        Time.timeScale = 0f; // ��ͣ��Ϸʱ��
        isPaused = true;
    }

    public void QuitGame()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false; // �˳�����ģʽ
#endif
        Application.Quit(); // �˳���Ϸ
        Debug.Log("Quit game"); // �ڱ༭���е���
    }
}
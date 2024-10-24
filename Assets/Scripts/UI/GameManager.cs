using UnityEngine;
using UnityEngine.SceneManagement; // ���ó�������

public class GameManager : MonoBehaviour
{
    // ����������ڰ�ť���ʱ����
    public void StartGame()
    {
        SceneManager.LoadScene("Level1"); // �滻Ϊ�����Ϸ��������
    }

    public void QuitGame()
    {
        Application.Quit();
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false; // �˳�����ģʽ
#endif
    }
}
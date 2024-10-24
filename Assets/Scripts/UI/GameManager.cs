using UnityEngine;
using UnityEngine.SceneManagement; // 引用场景管理

public class GameManager : MonoBehaviour
{
    // 这个方法将在按钮点击时调用
    public void StartGame()
    {
        SceneManager.LoadScene("Level1"); // 替换为你的游戏场景名称
    }

    public void QuitGame()
    {
        Application.Quit();
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false; // 退出播放模式
#endif
    }
}
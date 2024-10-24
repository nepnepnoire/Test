using UnityEngine;

public class NoteUI : MonoBehaviour
{
    public Note note; // 引用纸条脚本

    public void CloseNote()
    {
        note.CloseNote(); // 关闭纸条内容
    }
}
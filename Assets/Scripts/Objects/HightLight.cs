using UnityEngine;

public class HightLight : MonoBehaviour
{
    private SpriteRenderer spriteRenderer; // SpriteRenderer 组件
    private Color originalColor; // 原始颜色
    public Color highlightColor = Color.yellow; // 高亮颜色

    private void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>(); // 获取 SpriteRenderer 组件
        originalColor = spriteRenderer.color; // 记录原始颜色
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player")) // 确保是玩家
        {
            Highlight(true); // 高亮边框
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player")) // 确保是玩家
        {
            Highlight(false); // 恢复原始边框
        }
    }

    private void Highlight(bool isHighlighted)
    {
        if (isHighlighted)
        {
            spriteRenderer.color = highlightColor; // 设置高亮颜色
        }
        else
        {
            spriteRenderer.color = originalColor; // 恢复原始颜色
        }
    }
}
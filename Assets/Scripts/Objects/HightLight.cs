using UnityEngine;

public class HightLight : MonoBehaviour
{
    private SpriteRenderer spriteRenderer; // SpriteRenderer ���
    private Color originalColor; // ԭʼ��ɫ
    public Color highlightColor = Color.yellow; // ������ɫ

    private void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>(); // ��ȡ SpriteRenderer ���
        originalColor = spriteRenderer.color; // ��¼ԭʼ��ɫ
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player")) // ȷ�������
        {
            Highlight(true); // �����߿�
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player")) // ȷ�������
        {
            Highlight(false); // �ָ�ԭʼ�߿�
        }
    }

    private void Highlight(bool isHighlighted)
    {
        if (isHighlighted)
        {
            spriteRenderer.color = highlightColor; // ���ø�����ɫ
        }
        else
        {
            spriteRenderer.color = originalColor; // �ָ�ԭʼ��ɫ
        }
    }
}
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ButtonHoverEffect : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    private Vector3 originalScale;
    public float scaleMultiplier = 1.2f; // �Ŵ���
    public Color highlightColor = Color.yellow; // ������ɫ

    private Color originalColor;
    private Image buttonImage;

    void Start()
    {
        originalScale = transform.localScale;
        buttonImage = GetComponent<Image>();
        originalColor = buttonImage.color; // ��¼ԭʼ��ɫ
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        // �����ͣʱ�Ŵ�ť
        transform.localScale = originalScale * scaleMultiplier;
        // �ı䰴ť��ɫ
        buttonImage.color = highlightColor;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        // ����뿪ʱ�ָ���ť��С
        transform.localScale = originalScale;
        // �ָ���ť��ɫ
        buttonImage.color = originalColor;
    }
}
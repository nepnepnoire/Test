using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ButtonHoverEffect : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    private Vector3 originalScale;
    public float scaleMultiplier = 1.2f; // 放大倍数
    public Color highlightColor = Color.yellow; // 高亮颜色

    private Color originalColor;
    private Image buttonImage;

    void Start()
    {
        originalScale = transform.localScale;
        buttonImage = GetComponent<Image>();
        originalColor = buttonImage.color; // 记录原始颜色
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        // 鼠标悬停时放大按钮
        transform.localScale = originalScale * scaleMultiplier;
        // 改变按钮颜色
        buttonImage.color = highlightColor;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        // 鼠标离开时恢复按钮大小
        transform.localScale = originalScale;
        // 恢复按钮颜色
        buttonImage.color = originalColor;
    }
}
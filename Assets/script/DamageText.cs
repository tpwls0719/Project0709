using System.Collections;
using TMPro;
using UnityEngine;

public class DamageText : MonoBehaviour
{
    public TextMeshProUGUI text;
    public float floatUpSpeed = 50f;
    public float fadeDuration = 0.5f;
    private RectTransform rect;
    private CanvasGroup canvasGroup;

    void Awake()
    {
        rect = GetComponent<RectTransform>();
        canvasGroup = gameObject.AddComponent<CanvasGroup>();
    }

    public void Show(int damage)
    {
        text.text = damage.ToString();
        StartCoroutine(Floatup());
    }

    //텍스트가 위로 떠오르며 서서히 사라지는 코루틴
    private IEnumerator Floatup()
    {
        float elapsed = 0f;
        while (elapsed < fadeDuration)
        {
            elapsed += Time.deltaTime;
            rect.anchoredPosition += Vector2.up * floatUpSpeed * Time.deltaTime;
            canvasGroup.alpha = 1 - (elapsed / fadeDuration);
            yield return null;
        }
        Destroy(gameObject);
    }
}

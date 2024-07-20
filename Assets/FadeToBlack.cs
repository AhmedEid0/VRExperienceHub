using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class FadeToBlack : MonoBehaviour
{
    public float fadeDuration = 1f;
    private Image fadeImage;

    void Awake()
    {
        fadeImage = GetComponent<Image>();
    }

    public IEnumerator FadeOut()
    {
        for (float t = 0; t < fadeDuration; t += Time.deltaTime)
        {
            fadeImage.color = new Color(0, 0, 0, Mathf.Clamp01(t / fadeDuration));
            yield return null;
        }
    }

    public IEnumerator FadeIn()
    {
        for (float t = fadeDuration; t > 0; t -= Time.deltaTime)
        {
            fadeImage.color = new Color(0, 0, 0, Mathf.Clamp01(t / fadeDuration));
            yield return null;
        }
    }
}

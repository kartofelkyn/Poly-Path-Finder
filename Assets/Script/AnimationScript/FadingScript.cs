using UnityEngine;
using System.Collections;
using System.Collections.Generic;

/// <summary>
/// Handles fading in and out of a CanvasGroup for smooth transitions, 
/// such as when loading new scenes or displaying UI elements. 
/// The script allows for customizable fade duration and can be triggered to fade in or out as needed.
/// </summary>

public class FadingScript : MonoBehaviour
{
    [SerializeField] private CanvasGroup canvasGroup;
    [SerializeField] private float fadeDuration = 3f;
    [SerializeField] private bool fadeIn = false;

    public IEnumerator FadeIn()
    {
        yield return StartCoroutine(FadeCanvasGroup(canvasGroup, canvasGroup.alpha, 0, fadeDuration));
    }

    public IEnumerator FadeOut()
    {
        yield return StartCoroutine(FadeCanvasGroup(canvasGroup, canvasGroup.alpha, 1, fadeDuration));
    }
    private IEnumerator FadeCanvasGroup(CanvasGroup cg, float start, float end, float duration)
    {
        float elapsed = 0f;
        while (elapsed < duration)
        {
            elapsed += Time.deltaTime;
            cg.alpha = Mathf.Lerp(start, end, elapsed / duration);
            yield return null;
        }
        cg.alpha = end;
    }
}

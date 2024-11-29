
using UnityEngine;
using System.Collections; // required for IEnumerator

public class WelcomeMessageHandler : MonoBehaviour
{
    public GameObject welcomeCanvas; // Reference to the canvas or parent object containing the welcome message
public CanvasGroup canvasGroup; // reference to the canvasgroup
    public void HideWelcomeMessage()
    {
        //Check if CanvasGroup is assinged
        if (canvasGroup != null)
        {
 StartCoroutine(FadeOut());
        }
       else if (welcomeCanvas != null)
    {
        //if no CanvasGroup, just deactivate the canvas
        welcomeCanvas.SetActive(false);
    }
    else
    {
        Debug.LogError("CanvasGroup and WelcomeCanvas are both null. Cannot hide the message");
    }
    }
    private IEnumerator FadeOut()
    {
        float duration = 1f; // Duration of fade out in seconds
        float startAlpha = canvasGroup.alpha;

        for (float t = 0; t < duration; t += Time.deltaTime)
        {
            canvasGroup.alpha = Mathf.Lerp(startAlpha, 0, t / duration);
            yield return null;
        }
    canvasGroup.alpha = 0;
    canvasGroup.gameObject.SetActive(false); // disable the canvas after fading out
    }
}

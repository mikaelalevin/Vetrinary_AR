using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class LineFillController : MonoBehaviour
{
    public Image lineImage; // Assign in Inspector
    public float fillDuration = 3f; // Duration to fill the line

    private Coroutine fillCoroutine; // Store coroutine reference

    private void Start()
    {
        lineImage.fillAmount = 0f; // Start empty
        lineImage.enabled = true; // Make visible
    }

    public void StartFilling()
    {
        if (fillCoroutine != null)
            StopCoroutine(fillCoroutine); // Stop any existing fill animation

        lineImage.fillAmount = 0f; // Reset fill to 0
        fillCoroutine = StartCoroutine(FillLine());
    }

    public void Hide()
    {
        StopCoroutine(fillCoroutine);
        lineImage.fillAmount = 0f;
    }

    private IEnumerator FillLine()
    {
        float timeElapsed = 0f; // Timer to track elapsed time
        while (timeElapsed < fillDuration)
        {
            // Increment the fillAmount based on elapsed time
            lineImage.fillAmount = Mathf.Lerp(0f, 1f, timeElapsed / fillDuration);
            timeElapsed += Time.deltaTime; // Increment elapsed time
            yield return null; // Wait for the next frame
        }

        lineImage.fillAmount = 0f; // Ensure it ends fully filled

    }
}
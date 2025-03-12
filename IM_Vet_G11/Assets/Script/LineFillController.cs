using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class LineFillController : MonoBehaviour
{
    public Image lineImage; // Assign in Inspector
    public float fillSpeed = 3f; // Speed of filling

    private Coroutine fillCoroutine; // Store coroutine reference

    private void Start()
    {
        lineImage.fillAmount = 0f; // Start empty
        lineImage.enabled = false; // Start hidden
    }

    public void StartFilling()
    {
        if (fillCoroutine != null)
            StopCoroutine(fillCoroutine); // Stop any existing fill animation

        lineImage.fillAmount = 0f; // Reset fill
        lineImage.enabled = true; // Make visible
        fillCoroutine = StartCoroutine(FillLine());
    }

    private IEnumerator FillLine()
    {
        while (lineImage.fillAmount < 3f)
        {
            lineImage.fillAmount += Time.deltaTime * fillSpeed;
            yield return null;
        }

        lineImage.enabled = false; // Hide after filling
    }
}

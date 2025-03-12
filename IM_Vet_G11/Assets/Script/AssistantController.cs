using UnityEngine;
using TMPro;
using System.Collections;

public class AssistantController : MonoBehaviour
{
    public GameObject speechBubble; // UI-pratbubblan (Image)
    public TextMeshProUGUI speechText; // Textfältet i pratbubblan
    public float assistDuration = 5f; // Hur länge assistenten visas
    public float typingSpeed = 0.05f; // Hastighet för textanimation

    private Coroutine typingCoroutine;

    void Start()
    {
        speechBubble.SetActive(true); // Visa pratbubblan
        StartCoroutine(ShowMessage());
    }

    private IEnumerator ShowMessage()
    {
        string message = "Hello and welcome to your veterinary clinic! \nLet's start by calling for your first patient Billy.";
        yield return StartCoroutine(TypeText(message)); // Skriv ut texten gradvis
        yield return new WaitForSeconds(assistDuration); // Vänta några sekunder
        speechBubble.SetActive(false); // Dölj pratbubblan
    }

    private IEnumerator TypeText(string message)
    {
        speechText.text = ""; // Rensa tidigare text

        foreach (char letter in message.ToCharArray())
        {
            speechText.text += letter;
            yield return new WaitForSeconds(typingSpeed); // Vänta innan nästa bokstav visas
        }
    }
}

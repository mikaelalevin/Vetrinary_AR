using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class AssistantController : MonoBehaviour
{
    public GameObject assistant; // Dra in assistenten i Unity

     public GameObject speechBubble; // Pratbubblan (UI-panel eller textbakgrund)
    public Text speechText; // Texten som visas i pratbubblan

    public float assistDuration = 5f; // Hur länge assistenten visas
    public float timeLimit = 10f; // Tidsgräns innan assistenten visas igen
    public float typingSpeed = 0.05f; // Hastighet på skrivningseffekten


    private float timeSinceLastAction;
    private bool isAssisting = false;
    private Coroutine typingCoroutine; // Referens till korutinen


    void Start()
    {
       assistant.SetActive(true);
        speechBubble.SetActive(true);
        ShowAssistant("Hello and welcome to your own AR veterinary clinic! \nToday, we will examine Billy, the horse in front of you. Look at different parts of his body to start the checkup.");
    
    }

   void Update()
    {
        timeSinceLastAction += Time.deltaTime;

        if (timeSinceLastAction >= timeLimit && !isAssisting)
        {
            ShowAssistant("Try looking at Billy to start the examination! ");
        }
    }

    public void ShowAssistant(string message)
    {
        isAssisting = true;
        assistant.SetActive(true);
        speechBubble.SetActive(true);

        // Stoppa pågående textanimation om den finns
        if (typingCoroutine != null)
        {
            StopCoroutine(typingCoroutine);
        }

        // Starta ny skrivanimation
        typingCoroutine = StartCoroutine(TypeText(message));

        StartCoroutine(HideAssistantAfterDelay(assistDuration));
    }

    private IEnumerator TypeText(string message)
    {
        speechText.text = ""; // Rensa tidigare text

        foreach (char letter in message.ToCharArray())
        {
            speechText.text += letter;
            yield return new WaitForSeconds(typingSpeed); // Vänta en kort stund innan nästa bokstav visas
        }
    }

    private IEnumerator HideAssistantAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        assistant.SetActive(false);
        speechBubble.SetActive(false);
        isAssisting = false;
        timeSinceLastAction = 0f;
    }

    public void PlayerActionTaken()
    {
        timeSinceLastAction = 0f; // Återställ timern när spelaren gör något
    }
}
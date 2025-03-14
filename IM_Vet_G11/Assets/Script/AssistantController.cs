using UnityEngine;
using TMPro;
using System.Collections;

public class AssistantController : MonoBehaviour
{
    public GameObject speachBubble; // UI-pratbubblan (Image)
    public TextMeshProUGUI speachText; // Textfältet i pratbubblan
    public float assistDuration = 5f; // Hur länge assistenten visas
    public float typingSpeed = 0.05f; // Hastighet för textanimation
    public float delayBeforeReappearing = 5f; // Väntetid innan pratbubblan kommer tillbaka

    private Coroutine typingCoroutine;

    void Start()
    {
        StartCoroutine(ShowMessages());
    }

    private IEnumerator ShowMessages()
    {
        // Första meddelandet
        speachBubble.SetActive(true);
        speachText.gameObject.SetActive(true);
        yield return StartCoroutine(TypeText("Hello and welcome to your veterinary clinic! \nLet's start by calling for your first patient Billy"));
        yield return new WaitForSeconds(assistDuration);

        // Dölj pratbubblan
        speachBubble.SetActive(false);
        speachText.gameObject.SetActive(false);
        yield return new WaitForSeconds(delayBeforeReappearing);

        // Andra meddelandet
        speachBubble.SetActive(true);
        speachText.gameObject.SetActive(true);
        yield return StartCoroutine(TypeText("Now, grab the thermometer to check if Billy has a fever"));
        yield return new WaitForSeconds(assistDuration);

        // Dölj pratbubblan igen
        speachBubble.SetActive(false);
        speachText.gameObject.SetActive(false);
         yield return new WaitForSeconds(delayBeforeReappearing);

        // Tredje meddelandet
        speachBubble.SetActive(true);
        speachText.gameObject.SetActive(true);
        yield return StartCoroutine(TypeText("39.4 It looks like Billy has a fever. Give him a pil to bring down his tempature"));
        yield return new WaitForSeconds(assistDuration);

         // Dölj pratbubblan igen
        speachBubble.SetActive(false);
        speachText.gameObject.SetActive(false);
        yield return new WaitForSeconds(delayBeforeReappearing);

        // Fjärde meddelandet
        speachBubble.SetActive(true);
        speachText.gameObject.SetActive(true);
        yield return StartCoroutine(TypeText("Good joob! Give Billy a carrot to get rid of the nasty medicine taste"));
        yield return new WaitForSeconds(assistDuration);
        
         // Dölj pratbubblan igen
        speachBubble.SetActive(false);
        speachText.gameObject.SetActive(false);
        yield return new WaitForSeconds(delayBeforeReappearing);

         // Femte meddelandet
        speachBubble.SetActive(true);
        speachText.gameObject.SetActive(true);
        yield return StartCoroutine(TypeText("Fever in horses can be caused by an infection from a wound. Walk around Billy to see if you can find any"));
        yield return new WaitForSeconds(assistDuration);

         // Dölj pratbubblan igen
        speachBubble.SetActive(false);
        speachText.gameObject.SetActive(false);
        yield return new WaitForSeconds(delayBeforeReappearing);

         // Sjätte meddelandet
        speachBubble.SetActive(true);
        speachText.gameObject.SetActive(true);
        yield return StartCoroutine(TypeText("You really have an eye for spotting wounds! Take a cotton swab to clean the wound before we bandage it"));
        yield return new WaitForSeconds(assistDuration);

         // Dölj pratbubblan igen
        speachBubble.SetActive(false);
        speachText.gameObject.SetActive(false);
        yield return new WaitForSeconds(delayBeforeReappearing);

         // Sjunde meddelandet
        speachBubble.SetActive(true);
        speachText.gameObject.SetActive(true);
        yield return StartCoroutine(TypeText("Well done! Now put a bandage on to keep the wound protected from bacteria"));
        yield return new WaitForSeconds(assistDuration);

         // Dölj pratbubblan igen
        speachBubble.SetActive(false);
        speachText.gameObject.SetActive(false);
        yield return new WaitForSeconds(delayBeforeReappearing);

         // Åttonde meddelandet
        speachBubble.SetActive(true);
        speachText.gameObject.SetActive(true);
        yield return StartCoroutine(TypeText("Lastly, Billy needs his yearly vaccination. Give him the shot"));
        yield return new WaitForSeconds(assistDuration);

          // Dölj pratbubblan igen
        speachBubble.SetActive(false);
        speachText.gameObject.SetActive(false);
        yield return new WaitForSeconds(delayBeforeReappearing);

         // Åttonde meddelandet
        speachBubble.SetActive(true);
        speachText.gameObject.SetActive(true);
        yield return StartCoroutine(TypeText("Now reward Billy for being such a good boy with a carrot"));
        yield return new WaitForSeconds(assistDuration);

          // Dölj pratbubblan igen
        speachBubble.SetActive(false);
        speachText.gameObject.SetActive(false);


       




    }

    private IEnumerator TypeText(string message)
    {
        speachText.text = ""; // Rensa tidigare text

        foreach (char letter in message.ToCharArray())
        {
            speachText.text += letter;
            yield return new WaitForSeconds(typingSpeed); // Vänta innan nästa bokstav visas
        }
    }
}

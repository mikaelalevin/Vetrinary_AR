using UnityEngine;
using TMPro;
using System.Collections;

public class AssistantController : MonoBehaviour
{
    public GameObject speachBubble; // UI-pratbubblan (Image)
    public TextMeshProUGUI speachText; // Textfältet i pratbubblan
    public float assistDuration = 5f; // Hur länge assistenten visas
    public float typingSpeed = 0.05f; // Hastighet för textanimation
    public float delayBeforeReappearing = 1f; // Väntetid innan pratbubblan kommer tillbaka

    private int onboardingStage = 0;

    private Coroutine typingCoroutine;
    private Coroutine activeCoroutine;

    void Start()
    {
        HideSpeech();
        activeCoroutine = StartCoroutine(OnboardOne());
    }

    public void StartOnboardingStep(IEnumerator newCoroutine)
    {
        if (activeCoroutine != null)
        {
            StopCoroutine(activeCoroutine); // Stop the currently running coroutine
        }
        activeCoroutine = StartCoroutine(newCoroutine); // Start new coroutine
    }

    public void HideSpeech()
    {
        speachBubble.SetActive(false);
        speachText.gameObject.SetActive(false);
    }

    public IEnumerator OnboardOne()
    {
        onboardingStage = 1;
        yield return new WaitForSeconds(delayBeforeReappearing);
        speachBubble.SetActive(true);
        speachText.gameObject.SetActive(true);
        yield return StartCoroutine(TypeText("Hello and welcome to your veterinary clinic! \nLet's start by calling for your first patient Billy"));
    }

    public IEnumerator OnboardTwo()
    {
        if (onboardingStage <= 1) {
            HideSpeech();
            onboardingStage = 2;
            yield return new WaitForSeconds(delayBeforeReappearing);
            speachBubble.SetActive(true);
            speachText.gameObject.SetActive(true);
            yield return StartCoroutine(TypeText("Now, grab the thermometer and check if Billy has a fever, pick up the instruments by aiming the reticle"));
        }
    }
    public IEnumerator OnboardThree()
    {
        if (onboardingStage == 2) {
            HideSpeech();
            onboardingStage = 3;
            yield return new WaitForSeconds(delayBeforeReappearing);
            speachBubble.SetActive(true);
            speachText.gameObject.SetActive(true);
            yield return StartCoroutine(TypeText("39.4 It looks like Billy has a fever. Put down the thermomterer, and give Billy a pill instead."));
        }
    }
    public IEnumerator OnboardFour()
    {
        if (onboardingStage == 3)
        {
            HideSpeech();
            onboardingStage = 4;
            yield return new WaitForSeconds(delayBeforeReappearing);
            speachBubble.SetActive(true);
            speachText.gameObject.SetActive(true);
            yield return StartCoroutine(TypeText("Good job! Give Billy a carrot to get rid of the nasty medicine taste"));
        }
    }
    public IEnumerator OnboardFive()
    {
        if (onboardingStage == 4)
        {
            HideSpeech();
            onboardingStage = 5;
            yield return new WaitForSeconds(delayBeforeReappearing);
            speachBubble.SetActive(true);
            speachText.gameObject.SetActive(true);
            yield return StartCoroutine(TypeText("Fever in horses can be caused by an ear infection. Take the cotton swab and clean the ears"));
        }
        if (onboardingStage == 7) //Staging complete
        {
            HideSpeech();
            yield break;
        }
    }
    public IEnumerator OnboardSix()
    {
        if (onboardingStage == 5)
        {
            HideSpeech();
            onboardingStage = 6;
            yield return new WaitForSeconds(delayBeforeReappearing);
            speachBubble.SetActive(true);
            speachText.gameObject.SetActive(true);
            yield return StartCoroutine(TypeText("Lastly, Billy needs his yearly vaccination. Give him the shot"));
        }
    }
    public IEnumerator OnboardSeven()
    {
        if (onboardingStage == 6)
        {
            HideSpeech();
            onboardingStage = 7;
            yield return new WaitForSeconds(delayBeforeReappearing);
            speachBubble.SetActive(true);
            speachText.gameObject.SetActive(true);
            yield return StartCoroutine(TypeText("Now reward Billy with a carrot for being such a good boy"));
        }
    }


   /* private IEnumerator ShowMessages()
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
        yield return StartCoroutine(TypeText("Fever in horses can be caused by an ear infection. Take the cotton swab and clean the ears"));
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
   */

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

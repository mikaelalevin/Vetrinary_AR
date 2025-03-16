using UnityEngine;

public class MouthTrigger : MonoBehaviour
{
    public AudioClip syringe;
    public AudioClip carrot;
    private AudioSource audioSource;
    public PlacementIndicator placementIndicator;
    public AssistantController assistantController;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }


    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Termometer"))
        {
            Debug.Log("Termometern aktiverade triggern på hästen!");
            assistantController.StartOnboardingStep(assistantController.OnboardThree());
        }
        if (other.gameObject.CompareTag("Morot"))
        {
            Debug.Log("Moroten aktiverade triggern på hästen!");
            audioSource.PlayOneShot(carrot);
            placementIndicator.HorseEat();
            assistantController.StartOnboardingStep(assistantController.OnboardFive());
        }
        if (other.gameObject.CompareTag("Piller"))
        {
            Debug.Log("Pillret aktiverade triggern på hästen!");
            placementIndicator.HorseEat();
            assistantController.StartOnboardingStep(assistantController.OnboardFour());
        }
    }
}
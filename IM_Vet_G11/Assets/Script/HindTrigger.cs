using UnityEngine;

public class HindTrigger : MonoBehaviour
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
        if (other.gameObject.CompareTag("Spruta"))
        {
            Debug.Log("Sprutan aktiverade triggern på hästen!");
            audioSource.PlayOneShot(syringe);
            assistantController.StartOnboardingStep(assistantController.OnboardSeven());
        }
    }
}
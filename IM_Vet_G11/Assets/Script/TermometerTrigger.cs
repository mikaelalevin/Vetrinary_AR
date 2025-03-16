using UnityEngine;

public class TermometerTrigger : MonoBehaviour
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
            assistantController.OnboardThree();
        }
    }
}
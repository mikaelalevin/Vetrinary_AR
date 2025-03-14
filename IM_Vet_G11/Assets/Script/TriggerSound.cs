using UnityEngine;

public class Trigger_Sound : MonoBehaviour
{
    public AudioClip syringe;
    public AudioClip carrot;
    private AudioSource audioSource;
    public PlacementIndicator placementIndicator;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }


    private void OnTriggerEnter(Collider other) // Om du vill använda en trigger istället
    {
        if (other.gameObject.CompareTag("Spruta"))
        {
            Debug.Log("Sprutan aktiverade triggern på hästen!");
            audioSource.PlayOneShot(syringe);
        }
        if (other.gameObject.CompareTag("Morot"))
        {
            Debug.Log("Sprutan aktiverade triggern på hästen!");
            audioSource.PlayOneShot(carrot);
            placementIndicator.HorseEat();
        }
    }
}
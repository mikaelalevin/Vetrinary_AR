using UnityEngine;
using UnityEngine.Android;

public class ScaleFromMicrophone : MonoBehaviour
{
    public PlacementIndicator placementIndicatorScript;
    public AudioSource source;
    private AudioClip microphoneClip;
    public Vector3 minScale;
    public Vector3 maxScale;
    public AudioLoudnessDetection detector;

    public float loudnessSensibility = 100;
    public float threshold = 0.1f;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        if (!Permission.HasUserAuthorizedPermission(Permission.Microphone))
        {
            Permission.RequestUserPermission(Permission.Microphone);
        }

    }

    // Update is called once per frame
    void Update()
    {
        float loudness = detector.getLoudnessFromMicrophone() * loudnessSensibility;

        if (loudness < threshold)
            loudness = 0;

        if (loudness > threshold)
            placementIndicatorScript.moveHorse();

        transform.localScale = Vector3.Lerp(minScale, maxScale, loudness);
    }

    public void MicrophoneToAudioClip()
    {
        //hämtar ljud
        string microphoneName = Microphone.devices[0];
        microphoneClip = Microphone.Start(microphoneName, true, 20, AudioSettings.outputSampleRate);
    }
}

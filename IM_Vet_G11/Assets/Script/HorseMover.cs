using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;
using System.Collections.Generic;

public class HorseMover : MonoBehaviour
{
    public GameObject placementIndicator; // Reference to PlacementIndicator object
    public GameObject horse; // The object to move (Horse)

    private Camera arCamera;

    // Start is called before the first frame update
    void Start()
    {
        arCamera = Camera.main; // Get the main camera
    }

    // Update is called once per frame
    void Update()
    {
        // Detect screen tap or click
        if (Input.touchCount > 0 || Input.GetMouseButtonDown(0))
        {
            // Only process the first touch or mouse click
            Vector2 touchPosition = (Input.touchCount > 0) ? Input.GetTouch(0).position : (Vector2)Input.mousePosition;

            // Check if the PlacementIndicator has a valid position
            if (placementIndicator != null && horse != null)
            {
                // Only move the horse when the placementIndicator has a valid position
                if (placementIndicator.transform.position != Vector3.zero)
                {
                    // Set the horse's position to the placement indicator's position
                    horse.transform.position = placementIndicator.transform.position;
                    horse.transform.rotation = placementIndicator.transform.rotation;
                }
            }
        }
    }
}
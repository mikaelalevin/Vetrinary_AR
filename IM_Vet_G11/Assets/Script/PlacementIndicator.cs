using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;
using System.Collections.Generic;
using TMPro;

public class PlacementIndicator : MonoBehaviour
{
    public Canvas ui;
    public GameObject sprutaTable;
    public GameObject sprutaHands;
    public ARRaycastManager rayManager;

    public GameObject horseScene;
    public GameObject text;
    public LineFillController lineController;
    private GameObject visual;
    List<ARRaycastHit> hits = new List<ARRaycastHit>();

    private float hitTimer = 0f;
    public float requiredHitTime = 3f;

    public TextMeshProUGUI syringeText;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        // Get the components
        visual = transform.GetChild(0).gameObject;

        // Hide the placement indicator visual initially
        visual.SetActive(false);
        text.SetActive(false);        
        horseScene.SetActive(false);

        if (syringeText) syringeText.gameObject.SetActive(false);
        sprutaHands.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        // Shoot a raycast from the center of the screen        
        rayManager.Raycast(new Vector2(Screen.width / 2, Screen.height / 2), hits, TrackableType.Planes);

        // If we hit an AR plane surface, update the position and rotation
        if (hits.Count > 0)
        {
            transform.position = hits[0].pose.position;
            transform.rotation = hits[0].pose.rotation;

            // Show the visual if it's not active
            if (!visual.activeInHierarchy)
                visual.SetActive(true);
        }
        else
        {
            visual.SetActive(false); // Hide the visual if no plane is detected
        }

        // Raycast to check if sprutaTable is hit
        Ray ray = new Ray(Camera.main.transform.position, Camera.main.transform.forward);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit))
        {
            if (hit.collider.gameObject == sprutaTable)
            {
                lineController.StartFilling();
                hitTimer += Time.deltaTime;

                if (syringeText)
                    syringeText.gameObject.SetActive(true);               

                if (hitTimer >= requiredHitTime)
                {
                    if (sprutaTable) sprutaTable.SetActive(false); // Disables the entire object
                    if (sprutaHands) sprutaHands.SetActive(true);
                }
            }
            else
            {
                hitTimer = 0f; // Reset timer if not hitting sprutaTable
                if (syringeText) syringeText.gameObject.SetActive(false);
            }
        }
        else
        {
            hitTimer = 0f; // Reset timer if nothing is hit
            if (syringeText) syringeText.gameObject.SetActive(false);
        }

        // Detect screen tap or click
        if (Input.GetMouseButtonDown(0))
        {            
            moveHorse();            
        }
    }

    public void moveHorse()
    {
        //text.SetActive(true); (debugg för att testa input)
        horseScene.SetActive(true);

        // Only process the first touch or mouse click
        Vector2 touchPosition = (Input.touchCount > 0) ? Input.GetTouch(0).position : (Vector2)Input.mousePosition;

        // Check if the PlacementIndicator has a valid position
        if (horseScene != null && hits.Count > 0)
        {
            // Move the horse to the PlacementIndicator's position


            horseScene.transform.position = hits[0].pose.position;
            horseScene.transform.rotation = hits[0].pose.rotation; // Optionally match the rotation

            // Log to verify the movement
            Debug.Log("Horse moved to: " + horseScene.transform.position);
        }
    }
}

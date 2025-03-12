using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;
using System.Collections.Generic;
using TMPro;
using System.Collections;

public class PlacementIndicator : MonoBehaviour
{
    public Canvas ui;

    public GameObject sprutaTable;
    public GameObject sprutaHands;
    public GameObject bomullTable;
    public GameObject bomullHands;
    public GameObject pillerTable;
    public GameObject pillerHands;
    public GameObject morotTable;
    public GameObject morotHands;
    public GameObject termometerTable;
    public GameObject termometerHands;
    public GameObject table;

    private GameObject objectHands;
    private GameObject objectTable;

    public ARRaycastManager rayManager;
    public GameObject horseScene;
    public GameObject text;

    private bool handsOccupied = false;
    public LineFillController lineController;
    private GameObject visual;
    List<ARRaycastHit> hits = new List<ARRaycastHit>();

    private float hitTimer = 0f;
    public float requiredHitTime = 3f;

    public TextMeshProUGUI syringeText;

    private bool isPickingUp = false;
    private bool isPuttingDown = false;
    private bool isRaycastingObject = false;

    void Start()
    {
        visual = transform.GetChild(0).gameObject;
        visual.SetActive(false);
        text.SetActive(false);
        horseScene.SetActive(false);
        if (syringeText) syringeText.gameObject.SetActive(false);
        sprutaHands.SetActive(false);
        bomullHands.SetActive(false);
        pillerHands.SetActive(false);
        morotHands.SetActive(false);
        termometerHands.SetActive(false);
    }

    void Update()
    {
        rayManager.Raycast(new Vector2(Screen.width / 2, Screen.height / 2), hits, TrackableType.Planes);

        if (hits.Count > 0)
        {
            transform.position = hits[0].pose.position;
            transform.rotation = hits[0].pose.rotation;
            if (!visual.activeInHierarchy) visual.SetActive(true);
        }
        else
        {
            visual.SetActive(false);
        }

        Ray ray = new Ray(Camera.main.transform.position, Camera.main.transform.forward);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit))
        {
            if (hit.collider.gameObject == sprutaTable) HandleObjectDetection(sprutaTable, sprutaHands);
            else if (hit.collider.gameObject == bomullTable) HandleObjectDetection(bomullTable, bomullHands);
            else if (hit.collider.gameObject == pillerTable) HandleObjectDetection(pillerTable, pillerHands);
            else if (hit.collider.gameObject == morotTable) HandleObjectDetection(morotTable, morotHands);
            else if (hit.collider.gameObject == termometerTable) HandleObjectDetection(termometerTable, termometerHands);
            else if (hit.collider.gameObject == table && handsOccupied) StartPuttingDown();
            else ResetProcess();
        }
        else
        {
            ResetProcess();
        }

        if (Input.GetMouseButtonDown(0)) moveHorse();
    }

    void HandleObjectDetection(GameObject tableObj, GameObject handsObj)
    {
        if (!isRaycastingObject)
        {
            isRaycastingObject = true;
            hitTimer = 0f;
            isPickingUp = false;
            isPuttingDown = false;
        }

        objectTable = tableObj;
        objectHands = handsObj;

        if (!isPickingUp) StartPickingUp();
    }

    void StartPickingUp()
    {
        isPickingUp = true;
        lineController.StartFilling();
        StartCoroutine(PickUpCoroutine());
    }

    IEnumerator PickUpCoroutine()
    {
        hitTimer = 0f; // Reset timer only at the start of the coroutine
        while (hitTimer < requiredHitTime && isRaycastingObject)
        {
            hitTimer += Time.deltaTime;
            yield return null; // Wait for the next frame
        }

        if (isRaycastingObject && !handsOccupied)
        {
            Debug.Log("Pickup complete! Moving object to hands.");

            // Disable object on the table
            if (objectTable != null)
            {
                objectTable.SetActive(false);
            }

            // Enable object in the player's hands
            if (objectHands != null)
            {
                objectHands.SetActive(true);
            }

            handsOccupied = true;

            // Notify AssistantController
            FindObjectOfType<AssistantController>()?.PlayerActionTaken();
        }
    }

    void StartPuttingDown()
    {
        if (!isPuttingDown)
        {
            isPuttingDown = true;
            lineController.StartFilling();
            StartCoroutine(PutDownCoroutine());
        }
    }

    IEnumerator PutDownCoroutine()
    {
        hitTimer = 0f; // Reset timer at start

        while (hitTimer < requiredHitTime && isRaycastingObject)
        {
            hitTimer += Time.deltaTime;
            yield return null;
        }

        // Enable object back on the table
        if (objectTable != null)
        {
            objectTable.SetActive(true);
        }

        // Disable the object in the player's hands
        if (objectHands != null)
        {
            objectHands.SetActive(false);
        }

        handsOccupied = false;

        // Notify AssistantController
        FindObjectOfType<AssistantController>()?.PlayerActionTaken();
    }

    void ResetProcess()
    {
        if (isRaycastingObject)
        {
            isRaycastingObject = false;
            hitTimer = 0f;
            isPickingUp = false;
            isPuttingDown = false;
        }
    }

    public void moveHorse()
    {
        horseScene.SetActive(true);
        Vector2 touchPosition = (Input.touchCount > 0) ? Input.GetTouch(0).position : (Vector2)Input.mousePosition;

        if (horseScene != null && hits.Count > 0)
        {
            horseScene.transform.position = hits[0].pose.position;
            horseScene.transform.rotation = hits[0].pose.rotation;
            Debug.Log("Horse moved to: " + horseScene.transform.position);
            FindObjectOfType<AssistantController>()?.PlayerActionTaken();
        }
    }
}
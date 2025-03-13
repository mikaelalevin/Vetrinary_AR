using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;
using System.Collections.Generic;
using TMPro;
using System.Collections;
using UnityEngine.InputSystem.HID;
using Unity.VisualScripting;

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
    public GameObject lineImage;

    private GameObject objectHands;
    private GameObject objectTable;

    public ARRaycastManager rayManager;
    public GameObject horseScene;
    public GameObject text;


    private bool handsOccupied = false;
    private bool handlingObject = false;
    public LineFillController lineController;
    private GameObject visual;
    List<ARRaycastHit> hits = new List<ARRaycastHit>();


    public float requiredHitTime = 3f;

    public TextMeshProUGUI syringeText;

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
            if (hit.collider.gameObject == sprutaTable && !handsOccupied) HandleObjectDetection(sprutaTable, sprutaHands);
            else if (hit.collider.gameObject == bomullTable && !handsOccupied) HandleObjectDetection(bomullTable, bomullHands);
            else if (hit.collider.gameObject == pillerTable && !handsOccupied) HandleObjectDetection(pillerTable, pillerHands);
            else if (hit.collider.gameObject == morotTable && !handsOccupied) HandleObjectDetection(morotTable, morotHands);
            else if (hit.collider.gameObject == termometerTable && !handsOccupied) HandleObjectDetection(termometerTable, termometerHands);
            else if (hit.collider.gameObject == table && handsOccupied) PutDown();
        }

        if (Input.GetMouseButtonDown(0)) moveHorse();
    }

    void HandleObjectDetection(GameObject tableObj, GameObject handsObj)
    {
        if (!handlingObject)
        {
            handlingObject = true;
            lineController.StartFilling();            
        }

        if (!handsOccupied)
        {
            StartCoroutine(PickUpCoroutine(tableObj, handsObj));
        }
    }

    IEnumerator PickUpCoroutine(GameObject tableObj, GameObject handsObj)
    {
        float pickUpTimer = 0f;


        while (pickUpTimer < requiredHitTime)
        {
            Ray ray = new Ray(Camera.main.transform.position, Camera.main.transform.forward);
            RaycastHit hit;

            if (!Physics.Raycast(ray, out hit) || hit.collider.gameObject != tableObj)
            {
                Debug.Log("No longer hitting the object. Exiting pickup.");
                handlingObject = false;
                lineController.Hide();
                yield break; // Exit coroutine                
            }

            pickUpTimer += Time.deltaTime;
            yield return null; // Wait until the next frame
        }

        // Pickup complete
        objectHands = handsObj;
        objectTable = tableObj;
        PickUp();
        handsOccupied = true;
        handlingObject = false;
        lineController.Hide();

    }

    void PickUp()
    {
        objectHands.SetActive(true);
        objectTable.SetActive(false);
    }

    void PutDown()
    {
        if (!handlingObject)
        {
            handlingObject = true;
            lineController.StartFilling();
        }

        if (handsOccupied)
        {
            StartCoroutine(PutDownCoroutine());
        }
    }

    IEnumerator PutDownCoroutine()
    {
        float putDownTimer = 0f;

        while (putDownTimer < requiredHitTime)
        {
            Ray ray = new Ray(Camera.main.transform.position, Camera.main.transform.forward);
            RaycastHit hit;

            if (!Physics.Raycast(ray, out hit) || hit.collider.gameObject != table)
            {
                Debug.Log("No longer hitting the table. Exiting put down.");
                handlingObject = false;
                lineController.Hide();
                yield break;
            }

            putDownTimer += Time.deltaTime;
            yield return null; // Wait until the next frame
        }

        // Put down complete
        objectHands.SetActive(false);
        objectTable.SetActive(true);
        handsOccupied = false;
        handlingObject = false;
        lineController.Hide();

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
            //FindObjectOfType<AssistantController>()?.PlayerActionTaken();
        }
    }
}
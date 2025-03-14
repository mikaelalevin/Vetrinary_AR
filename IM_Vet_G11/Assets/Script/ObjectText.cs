using TMPro;
using UnityEngine;

public class ObjectText : MonoBehaviour
{
    public GameObject sprutaTable;
    public GameObject bomullTable;
    public GameObject pillerTable;
    public GameObject morotTable;
    public GameObject termometerTable;
    public GameObject table;
    public GameObject horseMouth;

    public TextMeshProUGUI syringeText;
    public TextMeshProUGUI morotText;
    public TextMeshProUGUI bomullText;
    public TextMeshProUGUI pillerText;
    public TextMeshProUGUI termometerText;
    public TextMeshProUGUI tableText;
    public TextMeshProUGUI horseMouthText;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        syringeText.gameObject.SetActive(false);
        morotText.gameObject.SetActive(false);
        bomullText.gameObject.SetActive(false);
        pillerText.gameObject.SetActive(false);
        termometerText.gameObject.SetActive(false);
        tableText.gameObject.SetActive(false);
        horseMouthText.gameObject.SetActive(false);
    }

    public void ShowText(GameObject objectInView)
    {
        if (objectInView == sprutaTable)
            syringeText.gameObject.SetActive(true);
        if (objectInView == bomullTable)
            bomullText.gameObject.SetActive(true);
        if (objectInView == pillerTable)
            pillerText.gameObject.SetActive(true);
        if (objectInView == morotTable)
            morotText.gameObject.SetActive(true);
        if (objectInView == termometerTable)
            termometerText.gameObject.SetActive(true);
        if (objectInView == table)
            tableText.gameObject.SetActive(true);
        if (objectInView == horseMouth)
            horseMouthText.gameObject.SetActive(true);
    }

    public void HideText()
    {
        syringeText.gameObject.SetActive(false);
        morotText.gameObject.SetActive(false);
        bomullText.gameObject.SetActive(false);
        pillerText.gameObject.SetActive(false);
        termometerText.gameObject.SetActive(false);
        tableText.gameObject.SetActive(false);
        horseMouthText.gameObject.SetActive(false);
    }
}

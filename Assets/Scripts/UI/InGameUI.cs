using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class InGameUI : MonoBehaviour
{
    [SerializeField] private GameManager gameManager;
    [SerializeField] private UnitSelections unitSelections;
    [SerializeField] private TextMeshProUGUI Temps;
    private int minutes;
    private int dixMinutes;
    private float dixSecondes;
    private float secondes;
    [SerializeField] private TextMeshProUGUI Enemies;
    private List<GameObject> enemiesList;
    private int nbEnemiesLeft;
    [SerializeField] private GameObject[] slots;

    // Start is called before the first frame update
    void Start()
    {
        Enemies.SetText("Enemies Left: ");
        Temps.SetText(minutes + ":" + secondes);
        enemiesList = unitSelections.GetEnemiesList();
        StartCoroutine(Minuteur());
        
    }

    // Update is called once per frame
    void Update()
    {
        nbEnemiesLeft = enemiesList.Count;
        Enemies.SetText("Enemies Left: " + nbEnemiesLeft);
        Temps.SetText(dixMinutes + minutes + ":" + dixSecondes + secondes);
    }

    private IEnumerator Minuteur()
    {
        while (true)
        {
            yield return new WaitForSeconds(1);
            if (secondes == 9)
            {
                dixSecondes++;
                secondes = 0;
            }
            else if (dixSecondes == 5)
            {
                minutes++;
                dixSecondes = 0;
            }
            else if (minutes == 9)
            {
                dixMinutes++;
                minutes = 0;
            }
            else
            {
                secondes++;
            }
        }
       
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class InGameUI : MonoBehaviour
{
    [SerializeField] private GameManager gameManager;
    [SerializeField] private KingManager kingManager;
    [SerializeField] private UnitSelections unitSelections;
    [SerializeField] private TextMeshProUGUI Temps;
    private int minutes;
    private int dixMinutes;
    private float dixSecondes;
    private float secondes;
    [SerializeField] private TextMeshProUGUI Enemies;
    private List<GameObject> enemiesList;
    private List<GameObject> unitList;
    private int nbEnemiesLeft;

    private List<GameObject> unitSelectedList;
    [SerializeField] private GameObject[] slots;
    [SerializeField] private GameObject LoseScreen;
    [SerializeField] private GameObject WinScreen;
    private GameObject[] activeSlots;
    private int initEnemies;

    // Start is called before the first frame update
    void Start()
    {
        Enemies.SetText("Enemies Left: ");
        Temps.SetText(dixMinutes + minutes + ":" + dixSecondes + secondes);
        enemiesList = unitSelections.GetEnemiesList();
        initEnemies = enemiesList.Count;
        unitSelectedList = unitSelections.GetSelectedList();
        unitList = unitSelections.GetUnitList();
        StartCoroutine(Minuteur());
        activeSlots = new GameObject[slots.Length];
    }

    // Update is called once per frame
    void Update()
    {
        nbEnemiesLeft = enemiesList.Count;
        Enemies.SetText("Enemies Left: " + nbEnemiesLeft);
        Temps.SetText(dixMinutes + minutes + ":" + dixSecondes + secondes);
        int compteur = 0;
        foreach (GameObject active in activeSlots)
        {
            if (active != null)
            {
                active.SetActive(false);
            }
        }
        foreach (GameObject slots in slots)
        {
            if (unitSelectedList.Count > compteur)
            {
                
                activeSlots[compteur] = slots.transform.Find(unitSelectedList[compteur].gameObject.GetComponent<Pokemon>().name).gameObject;
                activeSlots[compteur].SetActive(true);
            }
            
            compteur++;
        }

        if (unitList.Count == 0)
        {
            Defeat();
        }
        if (enemiesList.Count == 0)
        {
            Victory();
        }


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

    public string GetTimeSpoiled()
    {
        return (dixMinutes + minutes + ":" + dixSecondes + secondes);
    }

    public string GetEnemiesLeft()
    {
        return enemiesList.Count + "/" + initEnemies;
    }

    public void Defeat()
    {
        Temps.gameObject.SetActive(false);
        Enemies.gameObject.SetActive(false);
        LoseScreen.SetActive(true);
        Time.timeScale = 0;
        gameManager.SetTrack();
    }
    public void Victory()
    {
        Temps.gameObject.SetActive(false);
        Enemies.gameObject.SetActive(false);
        WinScreen.SetActive(true);
        Time.timeScale = 0;
        gameManager.SetTrack();
    }
}

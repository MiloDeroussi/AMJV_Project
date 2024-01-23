using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class RulesManager : UIManager
{
    [SerializeField] private Scrollbar difficultyScrollBar;
    private TextMeshProUGUI difficultyTxt;
    private string[] difficultyTag;
    private float[] difficultyModifierTab;
    private float difficultyModifier;
    [SerializeField] private Scrollbar mapScrollBar;
    private TextMeshProUGUI mapTxt;
    private string[] mapTag;
    private string SceneName;
    [SerializeField] private GameObject buttons;
    private Buttons buttonsClass;

    // Start is called before the first frame update
    void Start()
    {
        difficultyTxt = difficultyScrollBar.GetComponentInChildren<TextMeshProUGUI>();
        mapTxt = mapScrollBar.GetComponentInChildren<TextMeshProUGUI>();
        difficultyTag = new string[] { "Easy", "Normal", "Hard" };
        difficultyModifierTab = new float[] { 0.75f, 1f, 1.25f };
        mapTag = new string[] { "Beach", "Forest" };

        buttonsClass = buttons.GetComponent<Buttons>();
    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log(difficultyScrollBar.value * (difficultyScrollBar.numberOfSteps - 1));
        difficultyTxt.SetText(difficultyTag[Mathf.FloorToInt(difficultyScrollBar.value * (difficultyScrollBar.numberOfSteps - 1))]);
        difficultyModifier = difficultyModifierTab[Mathf.FloorToInt(difficultyScrollBar.value * (difficultyScrollBar.numberOfSteps - 1))];
        mapTxt.SetText(mapTag[Mathf.FloorToInt(mapScrollBar.value * (mapScrollBar.numberOfSteps - 1))]);
        SceneName = mapTxt.text;
        buttonsClass.SetLevelScene(SceneName);
    }
    
}

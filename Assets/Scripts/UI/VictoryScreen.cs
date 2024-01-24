using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class VictoryScreen : MonoBehaviour
{
    [SerializeField] private GameObject Canvas;
    [SerializeField] private TextMeshProUGUI time;

    // Start is called before the first frame update
    void Start()
    {

    }

    private void OnEnable()
    {
        InGameUI UI = Canvas.GetComponent<InGameUI>();
        time.SetText("Time Spoiled: " + UI.GetTimeSpoiled());
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Return))
        {
            Time.timeScale = 1;
            SceneManager.LoadScene("Main Title");
        }
    }
}

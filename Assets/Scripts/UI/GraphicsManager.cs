using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class GraphicsManager : MonoBehaviour
{
    [SerializeField] private Scrollbar QualityScrollBar;
    [SerializeField] private TMPro.TMP_Dropdown windowOption;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log(Mathf.FloorToInt(QualityScrollBar.value * (QualityScrollBar.numberOfSteps-1)) );
        QualitySettings.SetQualityLevel(Mathf.FloorToInt(QualityScrollBar.value * (QualityScrollBar.numberOfSteps-1)));
        if (windowOption.value == 0)
        {
            Screen.fullScreen = false;
        }
        else if (windowOption.value == 1)
        {
            Screen.fullScreen = true;
        }
    }
}

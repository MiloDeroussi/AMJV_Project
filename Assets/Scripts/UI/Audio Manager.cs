using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.Audio;

public class AudioManager : MonoBehaviour
{
    [SerializeField] private Scrollbar GeneralScrollBar;
    [SerializeField] private Scrollbar MusicScrollBar;
    [SerializeField] private Scrollbar EffectScrollBar;
    [SerializeField] private AudioMixer mixer;
    private TextMeshProUGUI mapTxt;
    private string[] mapTag;
    private string SceneName;


    // Start is called before the first frame update
    void Start()
    {
       
    }

    // Update is called once per frame
    void Update()
    {
        SetVolume("MasterVolume", GeneralScrollBar.value);
        SetVolume("MusicVolume", MusicScrollBar.value);
        SetVolume("EffectsVolume", EffectScrollBar.value);
    }

    private void SetVolume(string mixerVolume,float sliderValue)
    {
        if (sliderValue != 0)
        {
            mixer.SetFloat(mixerVolume, Mathf.Log10(sliderValue) * 20);
        }
        else
        {
            mixer.SetFloat(mixerVolume, Mathf.Log10(0.001f) * 20);
        }
        
    }
}

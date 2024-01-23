using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Buttons : MonoBehaviour
{
    // Start is called before the first frame update


    private string LevelScene;

    
    private string AudioSettingsScene;
    private string GraphicsSettingsScene;
    private string RuleScene;
    private string TitleScene;

    void Start()
    {
        AudioSettingsScene = "Audio Settings";
        GraphicsSettingsScene = "Graphic Settings";
        RuleScene = "Rule Screen";
        TitleScene = "Main Title";
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Play()
    {
        SceneManager.LoadScene(RuleScene);
    }

    public void AudioSettings()
    {
        SceneManager.LoadScene(AudioSettingsScene);
    }

    public void GraphicSettings()
    {
        SceneManager.LoadScene(GraphicsSettingsScene);
    }

    public void Quit()
    {
        Application.Quit();
    }
    public void LaunchLevel()
    {
        
        SceneManager.LoadScene(LevelScene);
    }

    public void BackToTitle()
    {
        SceneManager.LoadScene(TitleScene);
    }

    public void SetLevelScene(string level)
    {
        LevelScene = level;
    }
}

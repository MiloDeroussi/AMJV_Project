using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Buttons : MonoBehaviour
{
    // Start is called before the first frame update

    [SerializeField] private string PlayScene;
    [SerializeField] private string SettingsScene;
    [SerializeField] private GameManager gameManager;
    private string LevelScene;

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Play()
    {
        SceneManager.LoadScene(PlayScene);
    }

    public void Settings()
    {
        SceneManager.LoadScene(SettingsScene);
    }

    public void Quit()
    {
        Application.Quit();
    }
    public void LaunchLevel()
    {
        LevelScene = gameManager.getLevelName();
        SceneManager.LoadScene(LevelScene);
    }
}

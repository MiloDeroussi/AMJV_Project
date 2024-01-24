using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    // Start is called before the first frame update

    private GameObject king;
    private RulesManager ruleManager;
    [SerializeField] private AudioSource music;
    [SerializeField] private AudioClip[] musics;
    private int currentTrack;
    
    private string levelName;
    private float difficultyModifier;

    private void Awake()
    {
        int numGameManager = FindObjectsOfType<GameManager>().Length;
        if (numGameManager != 1)
        {
            Destroy(this.gameObject);
        }
        else
        {
            DontDestroyOnLoad(this);
        }
    }

    public GameObject GetKing()
    {
        return king;
    }

    public void SetKing(GameObject king)
    {
        this.king = king;
    }

    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {

    }

    public float getDifficultyModifier()
    {
        return difficultyModifier;
    }

    public void SetTrack()
    {
        currentTrack = Mathf.Abs(currentTrack - 1) ;
        music.clip = musics[currentTrack];
        music.Play();
    }


}

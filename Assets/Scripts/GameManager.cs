using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    // Start is called before the first frame update

    private GameObject king;
    [SerializeField] private UIManager UIManager;
    private string levelName;
    private float difficultyModifier;

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
        DontDestroyOnLoad(this);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public float getDifficultyModifier()
    {
        return difficultyModifier;
    }

    public string getLevelName()
    {
        return levelName;
    }
}

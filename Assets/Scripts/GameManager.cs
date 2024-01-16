using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    // Start is called before the first frame update

    private GameObject king;
    [SerializeField] GameObject Flag;

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
}

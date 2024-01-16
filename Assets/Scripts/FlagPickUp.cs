using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlagPickUp : MonoBehaviour
{

    [SerializeField] private GameManager gameManager;
    [SerializeField] LayerMask mask;
    private bool FlagPicked;

    // Start is called before the first frame update
    void Start()
    {
        FlagPicked = false;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log(mask);
        Debug.Log(other.gameObject.layer);
        if (!FlagPicked && other.gameObject.layer == mask) 
        {
            Debug.Log("ok");
            gameManager.SetKing(other.gameObject);
            FlagPicked = true;
            GameObject couronne = gameObject.transform.Find("Couronne").gameObject;
            couronne.SetActive(true);
        }
    }
}

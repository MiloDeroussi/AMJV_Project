using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HomeManager : MonoBehaviour
{
    private bool brought;
    [SerializeField] private GameObject kingManager;
    [SerializeField] private GameObject inGameUI;
    // Start is called before the first frame update
    void Start()
    {
        brought = false;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!(brought) & (other.gameObject == kingManager.GetComponent<KingManager>().getKing()))
        {
            inGameUI.GetComponent<InGameUI>().Victory();
            Debug.Log("Victory !");
            brought = true;
        }
    }
}

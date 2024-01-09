using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlagManager : MonoBehaviour
{
    private bool captured;
    [SerializeField] GameObject kingManager;
    // Start is called before the first frame update
    void Start()
    {
        captured = false;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!(captured) & (other.gameObject.layer == 11))
        {
            kingManager.GetComponent<KingManager>().setKing(other.gameObject);
            captured = true;
        }
    }
}

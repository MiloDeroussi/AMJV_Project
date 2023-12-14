using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Selectionable : MonoBehaviour
{
    [SerializeField] GameObject selectionManager;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnMouseDown()
    {
        selectionManager.GetComponent<Deplacement>().selected = gameObject;
    }
}

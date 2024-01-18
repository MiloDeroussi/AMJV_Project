using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Ectoplasma : Pokemon
{
    NavMeshAgent myAgent;
    Camera myCam;
    public LayerMask ground;
    // Start is called before the first frame update
    void Start()
    {
        myAgent = GetComponent<NavMeshAgent>();
        myCam = Camera.main;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public override void Attack(GameObject target)
    {
        Debug.Log("bim");
        target.GetComponent<Health>().setPoison();
    }

    public override void Capacity()
    {
        RaycastHit hit;
        Ray ray = myCam.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out hit, Mathf.Infinity, ground))
        {
            myAgent.Warp(hit.point);
        }
    }
}

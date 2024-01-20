using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class UnitControl : MonoBehaviour
{
    private Camera myCam;
    private NavMeshAgent myAgent;
    private Pokemon pokemon;
    public LayerMask ground;
    public LayerMask ennemy;
    private EUnitStateMachine usm;
    // Start is called before the first frame update
    void Start()
    {
        myCam = Camera.main;
        myAgent = GetComponent<NavMeshAgent>();
        usm = GetComponent<EUnitStateMachine>();
        pokemon = GetComponent<Pokemon>();
    }

    // Update is called once per frame
    void Update()
    {
        /*if ((myAgent.destination - myAgent.nextPosition).magnitude < 2 && myAgent.velocity.magnitude < 1)
        {
            myAgent.SetDestination(myAgent.nextPosition);
        }*/
        if (Input.GetMouseButtonDown(1))
        {
            RaycastHit hit;
            Ray ray = myCam.ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(ray, out hit, Mathf.Infinity, ennemy))
            {
                Debug.Log("J'attaque " + hit.collider.gameObject.name+" !");
                myAgent.SetDestination(hit.point);
                usm.focusTarget = hit.collider.gameObject;
                usm.obeyActionIsGiven = true;
            }

            else if (Physics.Raycast(ray, out hit, Mathf.Infinity, ground))
            {
                myAgent.SetDestination(hit.point);
                usm.obeyActionIsGiven = true;
                usm.focusTarget = null;
            }
        }
    }
}

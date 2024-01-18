using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class UnitControl : MonoBehaviour
{
    Camera myCam;
    NavMeshAgent myAgent;
    Pokemon pokemon;
    public LayerMask ground;
    public LayerMask ennemy;
    // Start is called before the first frame update
    void Start()
    {
        myCam = Camera.main;
        myAgent = GetComponent<NavMeshAgent>();

        pokemon = GetComponent<Pokemon>();
    }

    // Update is called once per frame
    void Update()
    {
        if ((myAgent.destination - myAgent.nextPosition).magnitude < 2 && myAgent.velocity.magnitude < 1)
        {
            myAgent.SetDestination(myAgent.nextPosition);
        }
        if (Input.GetMouseButtonDown(1))
        {
            RaycastHit hit;
            Ray ray = myCam.ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(ray, out hit, Mathf.Infinity, ennemy))
            {
                Debug.Log("J'attaque " + hit.collider.gameObject.name+" !");
                pokemon.Attack(hit.collider.gameObject);
            }

            if (Physics.Raycast(ray, out hit, Mathf.Infinity, ground))
            {
                myAgent.SetDestination(hit.point);
            }
        }

        if (Input.GetKeyDown(KeyCode.C))
        {
            RaycastHit hit;
            Ray ray = myCam.ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(ray, out hit, Mathf.Infinity))
            {
                Debug.Log("J'utilise ma capacit� !");
                pokemon.Capacity(hit.collider.gameObject);
            }
        }
    }
}

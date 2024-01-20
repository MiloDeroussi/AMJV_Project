using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Ectoplasma : Pokemon
{
    NavMeshAgent myAgent;
    Camera myCam;
    public LayerMask ground;
    public LayerMask targets;
    private bool isOnCooldown;
    [SerializeField] float attackRange;
    [SerializeField] float detectRange;
    [SerializeField] float capacityCd;
    [SerializeField] float attackCd;
    [SerializeField] float capacityDuration;
    // Start is called before the first frame update
    void Start()
    {
        myAgent = GetComponent<NavMeshAgent>();
        myCam = Camera.main;
        isOnCooldown = false;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public override void Attack(GameObject target)
    {
        if (!isOnCooldown)
        {
            isOnCooldown = true;
            Debug.Log("bim");
            target.GetComponent<Health>().setPoison();
            StartCoroutine(Cooldown(attackCd));
        }
    }

    public override void Capacity(GameObject target)
    {
        RaycastHit hit;
        Ray ray = myCam.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out hit, Mathf.Infinity, ground))
        {
            myAgent.Warp(hit.point);
            Collider[] hits = Physics.OverlapSphere(myAgent.nextPosition, 10, targets);
            foreach (Collider c in hits)
            {
                c.GetComponent<Health>().damage(3);
            }
        }
    }

    private IEnumerator Cooldown(float cd) { 
        yield return new WaitForSeconds(cd);
        isOnCooldown = false;
    }
}

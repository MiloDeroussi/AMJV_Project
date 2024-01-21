using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Ectoplasma : Pokemon
{
    private EUnitStateMachine usm;

    NavMeshAgent myAgent;
    Camera myCam;
    public LayerMask ground;
    public LayerMask targets;
    public bool isOnAttackCooldown;
    public bool isOnCapacityCooldown;
    [SerializeField] float attackRange;
    [SerializeField] float detectRange;
    [SerializeField] float capacityCd;
    [SerializeField] private float attackCd;
    [SerializeField] float capacityDuration;
    [SerializeField] float capacityRange;
    [SerializeField] float capacityRadius;
    [SerializeField] float capacityDamage;
    // Start is called before the first frame update

    public override float getAttackCd()
    {
        return attackCd;
    }

    public override float getCooldown()
    {
        return capacityCd;
    }

    void Start()
    {
        usm = GetComponent<EUnitStateMachine>();
        myAgent = GetComponent<NavMeshAgent>();
        myCam = Camera.main;
        isOnCapacityCooldown = false;isOnAttackCooldown = false;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public override void Attack()
    {
        if (!isOnAttackCooldown)
        {
            isOnAttackCooldown = true;
            usm?.attackTarget.GetComponent<Health>().setPoison();
            StartCoroutine(AttackCooldown(attackCd));
        }
    }

    public override void Capacity()
    {
        if (!isOnCapacityCooldown)
        {
            RaycastHit hit;
            Ray ray = myCam.ScreenPointToRay(Input.mousePosition);

            if (usm.GetIsEnemy())
            {
                isOnCapacityCooldown = true;
                Collider[] scans = Physics.OverlapSphere(myAgent.nextPosition, capacityRange, targets);
                int random = Random.Range(0, scans.Length);
                myAgent.Warp(scans[random].transform.position);
                Collider[] hits = Physics.OverlapSphere(myAgent.nextPosition, capacityRadius, targets);
                foreach (Collider c in hits)
                {
                    c.GetComponent<Health>().damage(capacityDamage);
                }
            }

            else if (Physics.Raycast(ray, out hit, Mathf.Infinity, ground))
            {
               
                if (Mathf.Abs(Vector3.Distance(hit.point, this.transform.position)) < capacityRange)
                {
                    myAgent.Warp(hit.point);
                    isOnCapacityCooldown = true;
                    Collider[] hits = Physics.OverlapSphere(myAgent.nextPosition, capacityRadius, targets);
                    foreach (Collider c in hits)
                    {
                        c.GetComponent<Health>().damage(capacityDamage);
                    }
                }

            }
            if (isOnCapacityCooldown)
            {
                StartCoroutine(CapacityCooldown(capacityCd));
            }
        }
       
    }

    private IEnumerator AttackCooldown(float cd) { 
        yield return new WaitForSeconds(cd);
        isOnAttackCooldown = false;
    }

    private IEnumerator CapacityCooldown(float cd)
    {
        yield return new WaitForSeconds(cd);
        isOnCapacityCooldown = false;
    }
}

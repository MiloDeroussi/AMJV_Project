using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Leveinard : Pokemon
{
    private EUnitStateMachine usm;

    NavMeshAgent myAgent;
    Camera myCam;
    public LayerMask ground;
    public LayerMask targets;
    public bool isOnAttackCooldown; public bool isOnCapacityCooldown;
    private float damage;
    [SerializeField] float attackRange;
    [SerializeField] float detectRange;
    [SerializeField] float capacityCd;
    [SerializeField] float attackCd;
    [SerializeField] float capacityDuration;
    [SerializeField] float capacityDamage;  [SerializeField] float attackDamage;
    [SerializeField] float capacityRange;
    [SerializeField] GameObject[] eggBag;
    private GameObject egg;
    private int i;

    public override float getAttackCd()
    {
        return attackCd;
    }

    public override float getCooldown()
    {
        return capacityCd;
    }

    // Start is called before the first frame update
    void Start()
    {
        usm = GetComponent<EUnitStateMachine>();
        myAgent = GetComponent<NavMeshAgent>();
        myCam = Camera.main;
        isOnCapacityCooldown = false; isOnAttackCooldown = false;
        i = 0;
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
            egg = eggBag[i];
            i = (i + 1) % 3;
            egg.transform.position = transform.position + transform.forward * 2;
            egg.transform.LookAt(usm.attackTarget.transform.position + Vector3.up * (usm.attackTarget.transform.position - transform.forward).magnitude);
            egg.SetActive(true);
            StartCoroutine(AttackCooldown(attackCd));
        }
    }

    public override void Capacity()
    {
        if (!isOnAttackCooldown)
        {
            Collider[] hits = Physics.OverlapSphere(myAgent.nextPosition, capacityRange, targets);
            foreach (Collider c in hits)
            {
                c.GetComponent<Health>().heal(capacityDamage);
            }
            StartCoroutine(CapacityCooldown(capacityCd));
        }
        
    }

    private IEnumerator AttackCooldown(float cd)
    {
        yield return new WaitForSeconds(cd);
        isOnAttackCooldown = false;
    }

    private IEnumerator CapacityCooldown(float cd)
    {
        yield return new WaitForSeconds(cd);
        isOnCapacityCooldown = false;
    }
}
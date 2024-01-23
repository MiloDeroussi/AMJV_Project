using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.AI;

public class Minotaupe : Pokemon
{
    private EUnitStateMachine usm;

    NavMeshAgent myAgent;
    Camera myCam;
    public LayerMask ground;
    public LayerMask targets;
    public bool isOnAttackCooldown;
    public bool isOnCapacityCooldown;
    private float damage;
    private float maxHealth;
    [SerializeField] float attackRange;
    [SerializeField] float detectRange;
    [SerializeField] float capacityCd;
    [SerializeField] float attackCd;
    [SerializeField] float capacityDuration;
    [SerializeField] float capacityRange;
    [SerializeField] float capacityRadius;
    [SerializeField] float capacityDamage;
    [SerializeField] float attackDamage;
    [SerializeField] GameObject[] pieges;
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
        maxHealth = GetComponent<Health>().getHealth();
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
            damage = attackDamage;
            if (Random.value < 0.25)
            {
                damage *= 2;
            }
            usm.attackTarget.GetComponent<Health>().damage(damage);
            StartCoroutine(AttackCooldown(attackCd));
        }
    }

    public override void Capacity()
    {
        if (!isOnCapacityCooldown)
        {
            isOnCapacityCooldown = true;
            Collider[] hits = Physics.OverlapSphere(myAgent.nextPosition, capacityRadius);
            foreach (Collider c in hits)
            {
                foreach (GameObject piege in pieges)
                {
                    if (c.gameObject == piege)
                    {
                        c.gameObject.SetActive(false);
                    }
                }
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
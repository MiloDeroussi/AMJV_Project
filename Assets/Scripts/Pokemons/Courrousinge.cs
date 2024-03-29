using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Courrousinge : Pokemon
{
    private EUnitStateMachine usm;

    NavMeshAgent myAgent;
    Camera myCam;
    [SerializeField] LayerMask ground;
    [SerializeField] LayerMask targets;
    public bool isOnAttackCooldown; public bool isOnCapacityCooldown;
    private float damage;
    private float maxHealth;

    [SerializeField] float capacityCd;
    [SerializeField] float attackCd;
    [SerializeField] float capacityDuration;

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
            damage = 3 + 1 * (maxHealth - GetComponent<Health>().getHealth()) / 4;
            usm.attackTarget.GetComponent<Health>().damage(damage);
            StartCoroutine(AttackCooldown(attackCd));
        }
    }

    public override void Capacity()
    {
        if (!isOnCapacityCooldown)
        {
            isOnCapacityCooldown = true;
            Debug.Log(usm.attackTarget);
            float initialHp = usm.attackTarget.GetComponent<Health>().getHealth();
            damage = 2 + 1 * (maxHealth - GetComponent<Health>().getHealth()) / 2;
            usm.attackTarget.GetComponent<Health>().damage(damage);
            GetComponent<Health>().heal(initialHp - usm.attackTarget.GetComponent<Health>().getHealth());
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
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Goupelin : Pokemon
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
    [SerializeField] GameObject blizzard;
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
            usm.attackTarget.GetComponent<Health>().damage(attackDamage);
            StartCoroutine(Slow(2f, usm.attackTarget.GetComponent<NavMeshAgent>()));
            StartCoroutine(AttackCooldown(attackCd));
        }
    }

    public override void Capacity()
    {
        if (!isOnCapacityCooldown)
        {
            StartCoroutine(Blizzard());
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

    private IEnumerator Slow(float slowTime, NavMeshAgent target)
    {
        float originalSpeed = target.speed;
        target.speed = originalSpeed / 2;
        yield return new WaitForSeconds(slowTime);
        target.speed = originalSpeed;
        isOnCapacityCooldown = false;
    }

    private IEnumerator Blizzard()
    {
        isOnCapacityCooldown = true;
        float i = 10;
        GameObject at = usm.attackTarget;
        blizzard.transform.position = at.transform.position;
        Vector3 dir = at.transform.position - transform.position;
        blizzard.SetActive(true);
        while (i > 0)
        {
            Collider[] hits = Physics.OverlapSphere(at.transform.position, capacityRadius, targets);
            foreach (Collider c in hits)
            {
                blizzard.transform.position = at.transform.position;
                c.GetComponent<Health>().damage(capacityDamage);
                c.GetComponent<Rigidbody>().AddForce(dir * 3);
                StartCoroutine(Slow(2f, c.GetComponent<NavMeshAgent>()));
            }
            i--;
            yield return new WaitForSeconds(capacityDuration / 10);
        }
        blizzard.SetActive(false);
    }
}
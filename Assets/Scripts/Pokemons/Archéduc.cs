using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Archéduc : Pokemon
{
    private EUnitStateMachine usm;

    NavMeshAgent myAgent;
    Camera myCam;
    public LayerMask ground;
    public LayerMask targets;
    public bool isOnAttackCooldown; public bool isOnCapacityCooldown;
    private int i;
    private GameObject fleche;
    private bool special;
    private GameObject specialFleche;
    private int j;
    [SerializeField] float attackRange;
    [SerializeField] float detectRange;
    [SerializeField] float capacityCd;
    [SerializeField] float attackCd;
    [SerializeField] float capacityDuration;
    [SerializeField] float capacityDamage;[SerializeField] float attackDamage;
    [SerializeField] float capacityRange;
    [SerializeField] private GameObject[] carquois;
    [SerializeField] private GameObject[] specialCarquois;

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
        j = 0;
    }

    // Update is called once per frame
    void Update()
    {

    }

    public override void Attack()
    {
        if (!isOnAttackCooldown)
        {
            if (!special)
            {
                isOnAttackCooldown = true;
                fleche = carquois[i];
                i = (i + 1) % 6;
                fleche.transform.position = transform.position + transform.forward * 2;
                fleche.transform.LookAt(usm.attackTarget.transform.position);
                fleche.SetActive(true);
                StartCoroutine(AttackCooldown(attackCd));
            }
            
            else
            {
                isOnAttackCooldown = true;
                specialFleche = specialCarquois[j];
                j = (j + 1) % 6;
                specialFleche.transform.position = transform.position + transform.forward * 2;
                specialFleche.transform.LookAt(usm.attackTarget.transform.position);
                specialFleche.SetActive(true);
                StartCoroutine(AttackCooldown(attackCd));
            }
        }
    }

    public override void Capacity()
    {
        if (!isOnCapacityCooldown)
        {
            StartCoroutine(Tranchombre());
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

    private IEnumerator Tranchombre()
    {
        StartCoroutine(CapacityCooldown(capacityCd));
        special = true;
        yield return new WaitForSeconds(capacityDuration);
        special = false;
    }
}


using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Courrousinge : Pokemon
{
    NavMeshAgent myAgent;
    Camera myCam;
    public LayerMask ground;
    public LayerMask targets;
    private bool isOnCooldown;
    private float damage;
    private float maxHealth;
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
        maxHealth = GetComponent<Health>().getHealth();
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
            damage = 3 + 1 * (maxHealth - GetComponent<Health>().getHealth()) / 4;
            target.GetComponent<Health>().damage(damage);
            StartCoroutine(Cooldown(attackCd));
        }
    }

    public override void Capacity(GameObject target)
    {
        if (true)
        {
            float initialHp = target.GetComponent<Health>().getHealth();
            damage = 2 + 1 * (maxHealth - GetComponent<Health>().getHealth()) / 2;
            target.GetComponent<Health>().damage(damage);
            GetComponent<Health>().heal(initialHp - target.GetComponent<Health>().getHealth());

        }
    }

    private IEnumerator Cooldown(float cd)
    {
        yield return new WaitForSeconds(cd);
        isOnCooldown = false;
    }
}
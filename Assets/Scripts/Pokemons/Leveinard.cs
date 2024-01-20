using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Leveinard : Pokemon
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
            target.GetComponent<Health>().damage(5);
            StartCoroutine(Cooldown(attackCd));
        }
    }

    public override void Capacity(GameObject target)
    {
        Collider[] hits = Physics.OverlapSphere(myAgent.nextPosition, 10, targets);
        foreach (Collider c in hits)
        {
            c.GetComponent<Health>().heal(10);
        }
    }

    private IEnumerator Cooldown(float cd)
    {
        yield return new WaitForSeconds(cd);
        isOnCooldown = false;
    }
}
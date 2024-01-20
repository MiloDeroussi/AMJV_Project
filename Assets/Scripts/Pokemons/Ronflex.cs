using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Ronflex : Pokemon
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
            Collider[] hits = Physics.OverlapSphere(myAgent.nextPosition, 10, targets);
            foreach (Collider c in hits)
            {
                c.GetComponent<Health>().damage(5);
            }
            StartCoroutine(Cooldown(attackCd));
        }
    }

    public override void Capacity(GameObject target)
    {
    
        StartCoroutine(Repos());
    }

    private IEnumerator Cooldown(float cd)
    {
        yield return new WaitForSeconds(cd);
        isOnCooldown = false;
    }

    private IEnumerator Repos()
    {
        float i = 5;
        while (i > 0)
        {
            GetComponent<Health>().heal(5);
            i--;
            yield return new WaitForSeconds(1);
        }
    }
}

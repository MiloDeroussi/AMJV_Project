using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class UnitTransition : Transition
{
    // Start is called before the first frame update

    private EUnitStateMachine.UnitState state { get; set; }

    public bool isPatrolling;
    public bool canCapaciting;
    public bool isOnCooldown;

    private GameObject unit;
    private Transform uTransform;
    private Vector3 initialPosition;

    [HideInInspector] public NavMeshAgent agent;

    //Les variables suivantes seront à récupérer sur notre unité:
    private float attackRange;
    private float detectRange;
    public float patrolRange;



    private float cd;
    private float capacityDuration;


    //Les variables suivantes seront à récupérer sur les unités enemies:
    [SerializeField]
    private LayerMask uLayerMask;
    [SerializeField]
    private GameManager gameManager;
    [SerializeField]
    private bool isEnemy;
    [SerializeField]
    private GameObject UnitSelector;
    [SerializeField]
    private EUnitStateMachine stateMachine;



    void Start()
    {
        state = EUnitStateMachine.UnitState.PATROLLING;

        unit = this.gameObject;
        uTransform = unit.transform;
        initialPosition = uTransform.position;

        stateMachine.attackTarget = null;
        agent = this.GetComponent<NavMeshAgent>();

        //Les variables suivantes seront à récupérer sur notre unité
        attackRange = 5f;
        detectRange = 15f;
        cd = 25f;
        capacityDuration = 2f;

        canCapaciting = false;
        isOnCooldown = false;
        //Les variables suivantes seront à récupérer sur les unités enemies:

    }

    // Update is called once per frame
    void Update()
    {

    }

    

    void IdleToAttacking()
    {
        Collider[] results = new Collider[1];

        if (Physics.OverlapSphereNonAlloc(uTransform.position, attackRange, results, uLayerMask) > 0)
        {

            if (results[0] != null)
            {
                stateMachine.attackTarget = results[0].gameObject;
                stateMachine.focusTarget = null;
                state = stateMachine.Transition(EUnitStateMachine.UnitState.ATTACKING);

            }
        }
    }

    void IdleToMoving()
    {

        Collider[] results = new Collider[1];

        if (Physics.OverlapSphereNonAlloc(uTransform.position, detectRange, results, uLayerMask) > 0)
        {

            if (results[0] != null)
            {
                stateMachine.focusTarget = results[0].gameObject;
                stateMachine.attackTarget = null;

                state = stateMachine.Transition(EUnitStateMachine.UnitState.MOVING);
            }
        }

    }

    void AttackingToIdle()
    {
        Collider[] results = new Collider[1];

        if (Physics.OverlapSphereNonAlloc(uTransform.position, attackRange, results, uLayerMask) == 0)
        {

            stateMachine.attackTarget = null;
            stateMachine.focusTarget = null;
            state = stateMachine.Transition(EUnitStateMachine.UnitState.IDLE);
        }

        else
        {
            stateMachine.attackTarget = results[0].gameObject;
        }


    }

    void AttackToCapaciting()
    {
        if (canCapaciting == false && isOnCooldown == false)
        {
            canCapaciting = true;
            StartCoroutine(RandomTimeCapaciting(capacityDuration));
        }
        if (isOnCooldown == true)
        {
            StartCoroutine(Cooldown(cd));
        }

    }

    void MovingToIdle()
    {

        Collider[] results = new Collider[20];

        if (Physics.OverlapSphereNonAlloc(uTransform.position, detectRange, results, uLayerMask) == 0)
        {
            stateMachine.focusTarget = null;
            stateMachine.attackTarget = null;

            state = stateMachine.Transition(EUnitStateMachine.UnitState.IDLE);
        }
        else
        {
            if (!ContainsCollider(results, stateMachine.focusTarget))
            {
                stateMachine.focusTarget = results[0].gameObject;
            }
        }

    }

    bool ContainsCollider(Collider[] res, GameObject col)
    {
        foreach (Collider cols in res)
        {
            if (col == cols?.gameObject)
            {
                return true;
            }
        }
        return false;
    }

    void MovingToAttacking()
    {
        Collider[] results = new Collider[1];

        if (Physics.OverlapSphereNonAlloc(uTransform.position, attackRange, results, uLayerMask) > 0)
        {

            if (results[0] != null)
            {
                stateMachine.attackTarget = results[0].gameObject;
                stateMachine.focusTarget = null;

                state = stateMachine.Transition(EUnitStateMachine.UnitState.ATTACKING);

            }
        }
    }

    void PatrollingToMoving()
    {
        Collider[] results = new Collider[1];

        if (Physics.OverlapSphereNonAlloc(uTransform.position, detectRange, results, uLayerMask) > 0)
        {

            if (results[0] != null)
            {
                stateMachine.focusTarget = results[0].gameObject;
                stateMachine.attackTarget = null;
                state = stateMachine.Transition(EUnitStateMachine.UnitState.MOVING);
            }
        }
    }

    private IEnumerator RandomTimeCapaciting(float capacityDuration)
    {
        yield return new WaitForSeconds(Random.Range(1f, 5f));
        state = stateMachine.Transition(EUnitStateMachine.UnitState.CAPACITING);
        canCapaciting = false;
        isOnCooldown = true;
        yield return new WaitForSeconds(capacityDuration);
        state = stateMachine.Transition(EUnitStateMachine.UnitState.ATTACKING);
    }

    private IEnumerator Cooldown(float cd)
    {
        yield return new WaitForSeconds(cd);
        isOnCooldown = false;

    }

}


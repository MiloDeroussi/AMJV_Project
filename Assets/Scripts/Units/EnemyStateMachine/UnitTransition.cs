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
    private Health health;
    private Transform uTransform;
    private Vector3 initialPosition;

    [HideInInspector] public NavMeshAgent agent;

    //Les variables suivantes seront à récupérer sur notre unité:
    private float attackRange;
    private float detectRange;
    private float patrolRange;

    private float cd;
    private float capacityDuration;


    //Les variables suivantes seront à récupérer sur les unités enemies:
    [SerializeField]
    private LayerMask uLayerMask;
    [SerializeField]
    private GameManager gameManager;
    [SerializeField]
    private GameObject UnitSelector;
    private EUnitStateMachine stateMachine;

    [SerializeField]
    private GameObject UnitSelectionSystem;
    private UnitSelections unitSelection;


    void Start()
    {
        

        stateMachine = GetComponent<EUnitStateMachine>();

        state = EUnitStateMachine.UnitState.PATROLLING;

        unit = this.gameObject;
        uTransform = unit.transform;
        initialPosition = uTransform.position;
        health = GetComponent<Health>();

        stateMachine.attackTarget = null;
        agent = this.GetComponent<NavMeshAgent>();

        //Les variables suivantes seront à récupérer sur notre unité
        attackRange = 5f;
        detectRange = 15f;
        cd = 25f;
        capacityDuration = 2f;

        canCapaciting = false;
        isOnCooldown = false;


        

    }

    private void Update()
    {

        if (stateMachine.CurrentState == EUnitStateMachine.UnitState.IDLE)
        {
            IdleToAttacking();
            IdleToMoving();
        }

        if (stateMachine.CurrentState == EUnitStateMachine.UnitState.MOVING)
        {
            MovingToAttacking();
            MovingToIdle();
        }

        if (stateMachine.CurrentState == EUnitStateMachine.UnitState.ATTACKING)
        {
            AttackToCapaciting();
            AttackingToMoving();
            AttackingToIdle();
        }

            


        ToDeath();
        
        
    }

    void ToDeath()
    {
        if (health.getHealth() <= 0)
        {
            this.gameObject.SetActive(false);
        }
    }

    void ToObey()
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
                stateMachine.focusTarget = results[0].gameObject;
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

        if (Physics.OverlapSphereNonAlloc(uTransform.position, detectRange, results, uLayerMask) == 0)
        {
            Debug.Log("je passe ici attacktoidle");
            stateMachine.attackTarget = null;
            stateMachine.focusTarget = null;
            state = stateMachine.Transition(EUnitStateMachine.UnitState.IDLE);
        }

        else
        {
            //stateMachine.attackTarget = results[0].gameObject;
        }


    }

    void AttackingToMoving()
    {
        Collider[] results = new Collider[20];

        if (Physics.OverlapSphereNonAlloc(uTransform.position, attackRange, results, uLayerMask) == 0)
        {
            stateMachine.attackTarget = null;

            state = stateMachine.Transition(EUnitStateMachine.UnitState.MOVING);
        }
        else if (results[0] != null && !ContainsCollider(results, stateMachine.attackTarget))
        {
            stateMachine.focusTarget = results[0].gameObject;
            stateMachine.attackTarget = stateMachine.focusTarget;

        }

    }

    void AttackToCapaciting()
    {
        

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
                Debug.Log(results[0].gameObject);
                stateMachine.focusTarget = results[0].gameObject;
                stateMachine.attackTarget = stateMachine.focusTarget;
                state = stateMachine.Transition(EUnitStateMachine.UnitState.ATTACKING);

            }
        }
    }

    private IEnumerator Cooldown(float cd)
    {
        yield return new WaitForSeconds(cd);
        isOnCooldown = false;

    }




}




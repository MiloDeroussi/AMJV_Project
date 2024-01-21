using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class UnitTransition : Transition
{
    // Start is called before the first frame update

    private EUnitStateMachine.UnitState state { get; set; }

    public bool canCapaciting;
   

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

    private EUnitStateMachine stateMachine;

    [SerializeField]
    private GameObject unitSelectionSystem;

    private UnitSelections unitSelection;
    private UnitControl unitControl;

    private bool isObeying;
    private bool isInAttackRangeObey;

    void Start()
    {
        
        
        stateMachine = GetComponent<EUnitStateMachine>();

        state = EUnitStateMachine.UnitState.PATROLLING;

        unit = this.gameObject;
        uTransform = unit.transform;
        initialPosition = uTransform.position;
        health = GetComponent<Health>();
        unitSelection = unitSelectionSystem.GetComponentInChildren<UnitSelections>();

        stateMachine.attackTarget = null;
        agent = this.GetComponent<NavMeshAgent>();

        //Les variables suivantes seront à récupérer sur notre unité
        attackRange = 5f;
        detectRange = 15f;
        cd = 25f;
        capacityDuration = 2f;

        canCapaciting = false;

        isObeying = false;
        isInAttackRangeObey = false;

    }

    private void Update()
    {
        ToObey();
        ToDeath();
        ToCapaciting();

        if (!isObeying)
        {
            if (stateMachine.CurrentState == EUnitStateMachine.UnitState.IDLE)
            {
                IdleToAttacking();
                IdleToMoving();
            }

            else if (stateMachine.CurrentState == EUnitStateMachine.UnitState.MOVING)
            {
                MovingToAttacking();
                MovingToIdle();
            }

            else if (stateMachine.CurrentState == EUnitStateMachine.UnitState.ATTACKING)
            {
               
                AttackingToMoving();
                AttackingToIdle();
            }
        }
        
        else if (isObeying)
        {
            if (stateMachine.CurrentState == EUnitStateMachine.UnitState.OBEYMOVING)
            {
                ObeyMovingToObeyAttacking();
            }
            if (stateMachine.CurrentState == EUnitStateMachine.UnitState.OBEYATTACKING)
            {
                ObeyAttackingToObeyMoving();
            }
        }


        StopObey();
        
        
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
        
        if ((unitSelection.GetSelectedList().Contains(unit)))
        {
            isObeying = true;
            if (state == EUnitStateMachine.UnitState.ATTACKING || state == EUnitStateMachine.UnitState.CAPACITING)
            {
                state = stateMachine.Transition(EUnitStateMachine.UnitState.OBEYATTACKING);
            }
            else
            {
                state = stateMachine.Transition(EUnitStateMachine.UnitState.OBEYMOVING);
            }
        }
    }

    void StopObey()
    {
        if (!unitSelection.GetSelectedList().Contains(unit) && isObeying)
        {
            isObeying = false;
            state = stateMachine.Transition(EUnitStateMachine.UnitState.IDLE);
        }
    }

    void ObeyMovingToObeyAttacking()
    {
        Collider[] results = new Collider[20];
        if (Physics.OverlapSphereNonAlloc(uTransform.position, attackRange, results, uLayerMask) > 0
            && ContainsCollider(results, stateMachine.focusTarget))
        {
            
            if (!stateMachine.obeyActionIsGiven)
            {
                isInAttackRangeObey = true;
                stateMachine.attackTarget = stateMachine.focusTarget;
                state = stateMachine.Transition(EUnitStateMachine.UnitState.OBEYATTACKING);
            }
            else if (stateMachine.obeyActionIsGiven && !isInAttackRangeObey)
            {
                stateMachine.obeyActionIsGiven = false;
                
            }
        }
        else
        {
            isInAttackRangeObey = false;
        }
        
    }

    void ObeyAttackingToObeyMoving()
    {
        
        if (stateMachine.focusTarget == null)
        {
            state = stateMachine.Transition(EUnitStateMachine.UnitState.OBEYMOVING);
        }
        else if (stateMachine.obeyActionIsGiven)
        {
            stateMachine.focusTarget = null;
            stateMachine.attackTarget = null;
            state = stateMachine.Transition(EUnitStateMachine.UnitState.OBEYMOVING);
            
        }
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

    void MovingToIdle()
    {

        Collider[] results = new Collider[1];

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
             
                stateMachine.focusTarget = results[0].gameObject;
                stateMachine.attackTarget = stateMachine.focusTarget;
                state = stateMachine.Transition(EUnitStateMachine.UnitState.ATTACKING);

            }
        }
    }


    void ToCapaciting()
    {

        if (Input.GetKeyDown(KeyCode.E))
        {
            state = stateMachine.Transition(EUnitStateMachine.UnitState.CAPACITING);

            
        }
    }


}




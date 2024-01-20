using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyTransition : Transition
{
    // Start is called before the first frame update

    private EUnitStateMachine.UnitState state { get; set; }

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
    private KingManager kingManager;

    GameObject king;
    [SerializeField] private bool isRegicing;

    

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

        isRegicing = false;

        //Les variables suivantes seront à récupérer sur les unités enemies:

    }

    private void Update()
    {
        if (!isRegicing)
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

            if (stateMachine.CurrentState == EUnitStateMachine.UnitState.PATROLLING)
            {
                PatrollingToMoving();
            }

            ToRegicing();
            ToDeath();
        }
        else
        {
            if (stateMachine.CurrentState == EUnitStateMachine.UnitState.REGICINGMOVING)
            {
                RegicingMovingToRegicingAttacking();
            }
            if (stateMachine.CurrentState == EUnitStateMachine.UnitState.REGICINGATTACKING)
            {
                RegicingAttackingToRegicingMoving();
            }
        }
    }

    void ToDeath()
    {
        if (health.getHealth() <= 0)
        {
            this.gameObject.SetActive(false);
        }
    }

    void ToRegicing()
    {
        if (kingManager.getKing() != null)
        {
            isRegicing = true;
            king = kingManager.getKing();
            stateMachine.attackTarget = null;
            stateMachine.focusTarget = king;
            state = stateMachine.Transition(EUnitStateMachine.UnitState.REGICINGMOVING);
            
        }
    }

    void RegicingMovingToRegicingAttacking()
    {
        Collider[] results = new Collider[20];

        if (Physics.OverlapSphereNonAlloc(uTransform.position, attackRange, results, uLayerMask) > 0)
        {

            if (results[0] != null)
            {
                if (ContainsCollider(results, king))
                {
                    stateMachine.attackTarget = king;
                }
                else
                {
                    stateMachine.attackTarget = results[0].gameObject ;
                }
                
                state = stateMachine.Transition(EUnitStateMachine.UnitState.REGICINGATTACKING);

            }
        }

    }

    void RegicingAttackingToRegicingMoving()
    {
        Collider[] results = new Collider[1];

        if (Physics.OverlapSphereNonAlloc(uTransform.position, attackRange, results, uLayerMask) == 0)
        {

            stateMachine.attackTarget = null;
            state = stateMachine.Transition(EUnitStateMachine.UnitState.REGICINGMOVING);

        }
        else if (results[0] != null && !ContainsCollider(results, stateMachine.attackTarget))
        {
            if ( ContainsCollider(results, king))
            {
                stateMachine.attackTarget = king;
            }
            else
            {
                stateMachine.attackTarget = results[0].gameObject;
            }
           
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

    void AttackToCapaciting()
    {
        if (canCapaciting == false)
        {
            canCapaciting = true;
            StartCoroutine(RandomTimeCapaciting(capacityDuration));;
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
        /*else
        {
            if (!ContainsCollider(results, stateMachine.focusTarget))
            {
                stateMachine.focusTarget = results[0].gameObject;
            }
        }*/

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

    private IEnumerator RandomTimeCapaciting(float capacityDuration)
    {
        yield return new WaitForSeconds(Random.Range(1f, 5f));
        state = stateMachine.Transition(EUnitStateMachine.UnitState.CAPACITING);
        canCapaciting = false;
        yield return new WaitForSeconds(2f);
        state = stateMachine.Transition(EUnitStateMachine.UnitState.ATTACKING);
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

    

}

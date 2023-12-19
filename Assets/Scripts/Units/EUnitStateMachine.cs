using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EUnitStateMachine: MonoBehaviour
{
    public enum UnitState
    {
        IDLE,
        MOVING,
        ATTACKING,
        CAPACITING,
        PATROLLING,
        DEAD,

    }

    public UnitState CurrentState;
    public bool isRunning;
    public bool isJumping;
    public bool isAttacking;
    public bool isCapaciting;
    public bool isOnCooldown;
    private GameObject unit;
    public GameObject attackTarget;
    public GameObject movingTarget;
    private Transform uTransform;

    //Les variables suivantes seront à récupérer sur notre unité:
    private float attackRange;
    private float detectRange;
    private float cd;
    private float capacityDuration;

    //Les variables suivantes seront à récupérer sur les unités enemies:
    [SerializeField]
    private LayerMask uLayerMask;

    private Dictionary<UnitState, UnitBaseState> stateDictionary;

    private void Start()
    {
        unit = this.gameObject;
        uTransform = unit.transform;
        attackTarget = null;

        //Les variables suivantes seront à récupérer sur notre unité
        attackRange = 5f;
        detectRange = 10f;
        cd = 25f;
        capacityDuration = 2f;

        isCapaciting = false;
        isOnCooldown = false;
        //Les variables suivantes seront à récupérer sur les unités enemies:

        stateDictionary = new Dictionary<UnitState, UnitBaseState>
        {
            {UnitState.IDLE, new IdleState(this) },
            {UnitState.MOVING, new MovingState(this) },
            {UnitState.PATROLLING, new PatrollingState(this) },
            {UnitState.ATTACKING, new AttackingState(this) },
            {UnitState.CAPACITING, new CapacitingState(this) },
            {UnitState.DEAD, new CapacitingState(this) },
        };
        CurrentState = UnitState.PATROLLING;
        stateDictionary[CurrentState].Enter();
    }

    private void Update()
    {
        if(CurrentState == UnitState.IDLE)
        {
            IdleToAttacking();
            IdleToMoving();
        }
        else if (CurrentState == UnitState.MOVING)
        {
            MovingToAttacking();
            MovingToIdle();
        }
        else if (CurrentState == UnitState.ATTACKING)
        {
            AttackingToIdle();
            AttackToCapaciting();

        }
        else if (CurrentState == UnitState.PATROLLING)
        {
            PatrollingToMoving();

        }



        if (stateDictionary.ContainsKey(CurrentState))
        {
            stateDictionary[CurrentState].Update();
        }

    }

    private void OnDrawGizmos()
    {
        if (uTransform == null) return;
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(uTransform.position, attackRange);
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(uTransform.position, detectRange);
    }
    void IdleToAttacking()
    {
        Collider[] results = new Collider[1];
 
        if (Physics.OverlapSphereNonAlloc(uTransform.position, attackRange, results, uLayerMask) > 0)
        {

            if (results[0] != null)
            {
                attackTarget = results[0].gameObject;
                movingTarget = null;
                stateDictionary[CurrentState].Exit();
                CurrentState = UnitState.ATTACKING;
                stateDictionary[CurrentState].Enter();
            }
        }
    }

    void AttackingToIdle()
    {
        Collider[] results = new Collider[1];

        if (Physics.OverlapSphereNonAlloc(uTransform.position, attackRange, results, uLayerMask) == 0)
        {
                
            attackTarget = null;
            movingTarget = null;
            stateDictionary[CurrentState].Exit();
            CurrentState = UnitState.IDLE;
            stateDictionary[CurrentState].Enter();
        }
        else
        {
            attackTarget = results[0].gameObject;
        }

    }
    

    void IdleToMoving()
    {

        Collider[] results = new Collider[1];

        if (Physics.OverlapSphereNonAlloc(uTransform.position, detectRange, results, uLayerMask) > 0)
        {

            if (results[0] != null)
            {
                movingTarget = results[0].gameObject;
                attackTarget = null;
                stateDictionary[CurrentState].Exit();
                CurrentState = UnitState.MOVING;
                stateDictionary[CurrentState].Enter();
            }
        }

    }

    void MovingToIdle()
    {

        Collider[] results = new Collider[1];

        if (Physics.OverlapSphereNonAlloc(uTransform.position, detectRange, results, uLayerMask) == 0)
        {

            movingTarget = null;
            attackTarget = null;
            stateDictionary[CurrentState].Exit();
            CurrentState = UnitState.IDLE;
            stateDictionary[CurrentState].Enter();
        }
        else
        {
            //swap d'aggro si une unité ennemi est à porté de détection
            movingTarget = results[0].gameObject;
        }

    }

    void MovingToAttacking()
    {
        Collider[] results = new Collider[1];

        if (Physics.OverlapSphereNonAlloc(uTransform.position, attackRange, results, uLayerMask) > 0)
        {

            if (results[0] != null)
            {
                attackTarget = results[0].gameObject;
                movingTarget = null;
                stateDictionary[CurrentState].Exit();
                CurrentState = UnitState.ATTACKING;
                stateDictionary[CurrentState].Enter();
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
                movingTarget = results[0].gameObject;
                attackTarget = null;
                stateDictionary[CurrentState].Exit();
                CurrentState = UnitState.MOVING;
                stateDictionary[CurrentState].Enter();
            }
        }
    }

    void ToDeath()
    {

    }

    void AttackToCapaciting()
    {
        if (isCapaciting == false && isOnCooldown == false)
        {
            isCapaciting = true;
            StartCoroutine(RandomTimeCapaciting(capacityDuration));
        }
        
    }
    private IEnumerator RandomTimeCapaciting(float capacityDuration)
    {
        yield return new WaitForSeconds(Random.Range(1f, 5f));
        stateDictionary[CurrentState].Exit();
        CurrentState = UnitState.CAPACITING;
        stateDictionary[CurrentState].Enter();
        isCapaciting = false;
        isOnCooldown = true;
        yield return new WaitForSeconds(capacityDuration);
        stateDictionary[CurrentState].Exit();
        CurrentState = UnitState.ATTACKING;
        stateDictionary[CurrentState].Enter();
    }

    private IEnumerator Cooldown(float cd)
    {
        yield return new WaitForSeconds(cd);
        isOnCooldown = false;
        
    }
}




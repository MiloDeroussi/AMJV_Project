using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

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
        OBEYMOVING,
        OBEYATTACKING,
        REGICINGATTACKING,
        REGICINGMOVING,

    }

    public bool isPatrolling;
    private GameObject unit;
    private Transform uTransform;
    private Vector3 initialPosition;

    private float attackRange;
    private float detectRange;
    private float patrolRange;

    public UnitState CurrentState;
    public GameObject attackTarget;
    public GameObject focusTarget;
    public bool obeyActionIsGiven;
    [HideInInspector] public Pokemon pokemon;
    [HideInInspector] public NavMeshAgent agent;

    private Dictionary<UnitState, UnitBaseState> stateDictionary;
    [SerializeField] private bool isEnemy;

    Transition transition;
    public bool isOnCooldown;

    private void Start()
    {
        unit = this.gameObject;
        initialPosition = unit.transform.position;
        
        attackRange = 5f;
        detectRange = 15f;
        patrolRange = 25f;

        attackTarget = null;
        focusTarget = null;
        agent = this.GetComponent<NavMeshAgent>();
        pokemon = GetComponent<Pokemon>();
        //Les variables suivantes seront à récupérer sur notre unité


        //Les variables suivantes seront à récupérer sur les unités enemies:

        stateDictionary = new Dictionary<UnitState, UnitBaseState>
        {
            {UnitState.IDLE, new IdleState(this) },
            {UnitState.MOVING, new MovingState(this) },
            {UnitState.PATROLLING, new PatrollingState(this) },
            {UnitState.ATTACKING, new AttackingState(this) },
            {UnitState.CAPACITING, new CapacitingState(this) },
            {UnitState.DEAD, new CapacitingState(this) },
            {UnitState.REGICINGMOVING, new RegicingMovingState(this) },
            {UnitState.REGICINGATTACKING, new RegicingAttackingState(this) },
            {UnitState.OBEYMOVING, new ObeyMovingState(this) },
            {UnitState.OBEYATTACKING, new ObeyAttackingState(this) },
        };
        if (!isEnemy)
        {
            CurrentState = UnitState.IDLE;
            transition = GetComponent<UnitTransition>();
        }
        else
        {
            CurrentState = UnitState.PATROLLING;
            transition = GetComponent<EnemyTransition>();
        }
        
        stateDictionary[CurrentState].Enter();
    }

    public bool GetIsEnemy()
    {
        return isEnemy;
    }

    private void OnDrawGizmos()
    {
        Debug.Log("Drawing Gizmos");
        if (uTransform == null) return;
        Gizmos.color = Color.blue;
        Gizmos.DrawWireCube(initialPosition, new Vector3(2 * patrolRange, 0.1f, 2 * patrolRange));
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(uTransform.position, attackRange);
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(uTransform.position, detectRange);
    }

    private void Update()
    {
        uTransform = unit.transform;

        if (stateDictionary.ContainsKey(CurrentState))
        {
            stateDictionary[CurrentState].Update();
        }
        

    }

    public UnitState Transition(UnitState state)
    {
        stateDictionary[CurrentState].Exit();
        CurrentState = state;
        stateDictionary[CurrentState].Enter();
        return state;
    }

    

    public IEnumerator PatrolNewPath()
    {
        isPatrolling = true;
        agent.SetDestination(new Vector3(Random.Range(initialPosition.x - patrolRange, initialPosition.x + patrolRange), initialPosition.y, Random.Range(initialPosition.z - patrolRange, initialPosition.z + patrolRange)));
        yield return new WaitForSeconds(0.5f); //pour s'assurer qu'on ne détecte pas une velocity nulle au début du mouvement
        yield return new WaitUntil(() => agent.velocity.magnitude == 0);
        yield return new WaitForSeconds(5f);
        isPatrolling = false;

    }

    public IEnumerator Cooldown(float cd)
    {
        isOnCooldown = true;
        yield return new WaitForSeconds(cd);
        isOnCooldown = false;
    }


}




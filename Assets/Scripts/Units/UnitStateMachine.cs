using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitStateMachine: MonoBehaviour
{
    public enum UnitState
    {
        IDLE,
        MOVING,
        ATTACKING,
        CAPACITING,
        PATROLLING,

    }

    public UnitState CurrentState;
    public bool isRunning;
    public bool isJumping;
    public bool isAttacking;
    public bool isCapaciting;
    private Dictionary<UnitState, UnitBaseState> stateDictionary;

    private void Start()
    {
        stateDictionary = new Dictionary<UnitState, UnitBaseState>
        {
            {UnitState.IDLE, new IdleState(this) },
            {UnitState.MOVING, new MovingState(this) },
            {UnitState.PATROLLING, new PatrollingState(this) },
            {UnitState.ATTACKING, new AttackingState(this) },
            {UnitState.CAPACITING, new CapacitingState(this) },
        };

        CurrentState = UnitState.IDLE;
        stateDictionary[CurrentState].Enter();
    }

    private void Update()
    {
        if (stateDictionary.ContainsKey(CurrentState))
        {
            stateDictionary[CurrentState].Update();
        }

        ///Transition to Attack
        if (Input.GetKeyDown(KeyCode.A) && CurrentState != UnitState.ATTACKING)
        {
            stateDictionary[CurrentState].Exit();
            CurrentState = UnitState.ATTACKING;
            stateDictionary[CurrentState].Enter();

            //Transition to Idle 
            // For now, only on key pressed
            // Goal is to define later conditions to exit
            if (Input.GetKeyDown(KeyCode.Q))
            {
                stateDictionary[CurrentState].Exit();
                CurrentState = UnitState.IDLE;
                stateDictionary[CurrentState].Enter();
            }
        }

        //Transition to Movement
        if (Input.GetKeyDown(KeyCode.M) && CurrentState != UnitState.MOVING)
        {
            stateDictionary[CurrentState].Exit();
            CurrentState = UnitState.MOVING;
            stateDictionary[CurrentState].Enter();

            //Transition to Idle 
            // For now, only on key pressed
            // Goal is to define later conditions to exit
            if (Input.GetKeyDown(KeyCode.Q))
            {
                stateDictionary[CurrentState].Exit();
                CurrentState = UnitState.IDLE;
                stateDictionary[CurrentState].Enter();
            }
        }
    }
}


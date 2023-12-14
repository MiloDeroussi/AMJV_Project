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

}


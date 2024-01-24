using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IdleState : UnitBaseState
{
    private float detectionRange;

    
    public IdleState(EUnitStateMachine usm) : base(usm)
    { 
    }
    public override void Enter()
    {
    }

    public override void Update()
    {
        Debug.Log("Waiting for order");
    }

    public override void Exit()
    {
    }
}


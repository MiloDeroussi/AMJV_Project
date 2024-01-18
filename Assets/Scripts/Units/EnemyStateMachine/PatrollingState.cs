using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PatrollingState : UnitBaseState
{
    public PatrollingState(EUnitStateMachine usm) : base(usm)
    {
    }
    public override void Enter()
    {

    }

    public override void Update()
    { 
        if (!usm.isPatrolling)
        {
            
            usm.isPatrolling = true;
            usm.StartCoroutine(usm.PatrolNewPath());
        }
    }

    public override void Exit()
    {
        usm.isPatrolling = false;
    }
}

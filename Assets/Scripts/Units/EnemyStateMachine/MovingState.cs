using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingState : UnitBaseState
{
    public MovingState(EUnitStateMachine usm) : base(usm)
    {
    }
    public override void Enter()
    {
        
    }

    public override void Update()
    {
        usm.agent.SetDestination(usm.focusTarget.transform.position);


    }

    public override void Exit()
    {

    }

}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RegicingMovingState : UnitBaseState
{
    public RegicingMovingState(EUnitStateMachine usm) : base(usm)
    {
    }
    public override void Enter()
    {
        Debug.Log("King Tracking");
        usm.agent.isStopped = false;
    }

    public override void Update()
    {
        usm.agent.SetDestination(usm.focusTarget.transform.position);
    }

    public override void Exit()
    {
        usm.agent.isStopped = true;
    }
}

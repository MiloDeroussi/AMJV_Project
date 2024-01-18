using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RegicingAttackingState : UnitBaseState
{
    public RegicingAttackingState(EUnitStateMachine usm) : base(usm)
    {
    }
    public override void Enter()
    {
        usm.agent.isStopped = true;
    }

    public override void Update()
    {

    }

    public override void Exit()
    {
        usm.agent.isStopped = false;
    }
}

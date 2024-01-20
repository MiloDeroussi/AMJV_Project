using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObeyAttackingState : UnitBaseState
{
    public ObeyAttackingState(EUnitStateMachine usm) : base(usm)
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
        //Play Anim
        usm.agent.isStopped = false;
    }

}

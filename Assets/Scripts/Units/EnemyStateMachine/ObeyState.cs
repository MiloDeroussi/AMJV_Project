using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObeyState : UnitBaseState
{
    public ObeyState(EUnitStateMachine usm) : base(usm)
    {
    }
    public override void Enter()
    {
        usm.agent.isStopped = false;
        
    }

    public override void Update()
    {
        usm.agent.SetDestination(usm.focusTarget.transform.position);


    }

    public override void Exit()
    {
        //Play Anim
        usm.agent.isStopped = true;
    }

}

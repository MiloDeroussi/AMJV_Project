using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObeyMovingState : UnitBaseState
{
    public ObeyMovingState(EUnitStateMachine usm) : base(usm)
    {
    }
    public override void Enter()
    {
        usm.agent.isStopped = false;
        usm.attackTarget = null;
        
    }

    public override void Update()
    {
        if (usm.agent.remainingDistance < usm.agent.stoppingDistance )
        {
            usm.obeyActionIsGiven = false;
        }
        


    }

    public override void Exit()
    {
        //Play Anim
        usm.agent.isStopped = true;
    }

}

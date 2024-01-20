using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CapacitingState : UnitBaseState
{
    public CapacitingState(EUnitStateMachine usm) : base(usm)
    {
    }
    public override void Enter()
    {
        usm.agent.isStopped = true;
        
    }

    public override void Update()
    {
        usm.pokemon.Capacity();
    }

    public override void Exit()
    {
        usm.agent.isStopped = false;
    }
}

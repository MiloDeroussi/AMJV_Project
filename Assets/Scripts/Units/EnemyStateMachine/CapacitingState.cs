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
        
    }

    public override void Update()
    {
        usm.attackTarget = usm.focusTarget;
        usm.pokemon.Capacity();
    }

    public override void Exit()
    {
    }
}

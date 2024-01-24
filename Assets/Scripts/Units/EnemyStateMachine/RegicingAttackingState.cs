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
        usm.agent.ResetPath();

    }

    public override void Update()
    {
        usm.pokemon.Attack();
    }

    public override void Exit()
    {
        
    }
}

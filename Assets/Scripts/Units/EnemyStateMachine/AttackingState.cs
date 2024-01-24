using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackingState : UnitBaseState
{
    public AttackingState(EUnitStateMachine usm) : base(usm)
    {
    }
    public override void Enter()
    {
        usm.agent.ResetPath();
    }

    public override void Update()
    {
        if (usm.attackTarget != null)
        {
            usm.pokemon.Attack();
        }

    }

    public override void Exit()
    {
       
    }
}

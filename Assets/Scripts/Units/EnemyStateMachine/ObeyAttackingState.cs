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
        usm.agent.ResetPath();
        Debug.Log("IReadOnlyCollection passe ici ?");
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

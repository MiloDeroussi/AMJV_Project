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
        Debug.Log("Comming to war");
    }

    public override void Update()
    {
        Debug.Log("Target under fire");
    }

    public override void Exit()
    {
        Debug.Log("oh peace ?");
    }
}

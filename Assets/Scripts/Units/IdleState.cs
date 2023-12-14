using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IdleState : UnitBaseState
{
    public IdleState(UnitStateMachine usm) : base(usm)
    { 
    }
    public override void Enter()
    {
        Debug.Log("Hello I'm idle");
    }

    public override void Update()
    {
        Debug.Log("doing nothing zzz");
    }

    public override void Exit()
    {
        Debug.Log("shit i have something to do !");
    }
}


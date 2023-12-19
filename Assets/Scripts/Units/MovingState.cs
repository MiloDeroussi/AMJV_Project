using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingState : UnitBaseState
{
    public MovingState(EUnitStateMachine usm) : base(usm)
    {
    }
    public override void Enter()
    {
        Debug.Log("Oh something is here !");
    }

    public override void Update()
    {
        Debug.Log("On my way...");
        

    }

    public override void Exit()
    {
        Debug.Log("Chase finished");
    }

}

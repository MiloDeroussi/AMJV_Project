using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class UnitBaseState
{
    protected UnitStateMachine usm;

    public UnitBaseState(UnitStateMachine usm)
    {
        this.usm = usm;
    }

    public abstract void Enter();
    public abstract void Update();
    public abstract void Exit();

}
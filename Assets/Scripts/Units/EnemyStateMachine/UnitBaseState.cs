using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class UnitBaseState
{
    protected EUnitStateMachine usm;

    public UnitBaseState(EUnitStateMachine usm)
    {
        this.usm = usm;
    }

    public abstract void Enter();
    public abstract void Update();
    public abstract void Exit();

}
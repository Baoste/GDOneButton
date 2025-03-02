using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlyOutState : FlyState
{
    public FlyOutState(FlyStateMachine stateMachine, Fly fly, string animatorName) : base(stateMachine, fly, animatorName)
    {
    }

    public override void Enter()
    {
        base.Enter();
        fly.DestroySelf();
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void Update()
    {
        base.Update();
    }
}

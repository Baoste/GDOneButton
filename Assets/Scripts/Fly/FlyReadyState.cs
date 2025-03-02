using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlyReadyState : FlyState
{
    private float time;
    public FlyReadyState(FlyStateMachine stateMachine, Fly fly, string animatorName) : base(stateMachine, fly, animatorName)
    {
    }

    public override void Enter()
    {
        base.Enter();
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void Update()
    {
        base.Update();
        time += Time.deltaTime;
        if (time > 1.5f)
        {
            stateMachine.ChangeState(fly.suckState);
        }
    }
}

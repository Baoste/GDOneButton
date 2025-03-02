using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlySuckState : FlyState
{
    private float time;
    private float suckTime;
    public FlySuckState(FlyStateMachine stateMachine, Fly fly, string animatorName) : base(stateMachine, fly, animatorName)
    {
    }

    public override void Enter()
    {
        base.Enter();
        time = 0f;
        suckTime = 0f;
        fly.GetComponent<SpriteRenderer>().color = Color.red;
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void Update()
    {
        base.Update();
        time += Time.deltaTime;
        suckTime += Time.deltaTime;
        if (suckTime > 1f)
        {
            suckTime = 0f;
            fly.player.health -= 5f;
        }
        if (time > 5.1f)
        {
            stateMachine.ChangeState(fly.outState);
        }
    }
}

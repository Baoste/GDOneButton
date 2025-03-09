
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
        fly.player.health -= fly.suckDamage;
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
        if (suckTime > fly.suckDelTime)
        {
            suckTime = 0f;
            fly.player.health -= fly.suckDamage;
        }
        if (time > 5.1f)
        {
            stateMachine.ChangeState(fly.outState);
        }
    }
}

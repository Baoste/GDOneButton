
public class FlyDeadState : FlyState
{
    public FlyDeadState(FlyStateMachine stateMachine, Fly fly, string animatorName) : base(stateMachine, fly, animatorName)
    {
    }

    public override void Enter()
    {
        base.Enter();
        fly.generator.flies.Dequeue();
    }

    public override void Exit()
    {
        base.Exit();
        fly.DestroySelf();
    }

    public override void Update()
    {
        base.Update();
        //float timer = 0;
        //Tween t = DOTween.To(() => timer, x => timer = x, 1, 2f)
        //              .OnStepComplete(() =>
        //              {
        //                  stateMachine.ChangeState(fly.readyState);
        //              });
    }
}

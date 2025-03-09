using DG.Tweening;
using UnityEngine;

public class FlyOutState : FlyState
{
    private BezierCurve curve;
    private float time;
    public FlyOutState(FlyStateMachine stateMachine, Fly fly, string animatorName) : base(stateMachine, fly, animatorName)
    {
    }

    public override void Enter()
    {
        base.Enter();
        fly.generator.flies.Dequeue();
        curve = new BezierCurve(fly.transform.position, fly.curvePos, fly.inPos);
        time = 0f;
    }

    public override void Exit()
    {
        base.Exit();
        fly.DestroySelf();
    }

    public override void Update()
    {
        base.Update();
        fly.GetComponent<SpriteRenderer>().DOFade(0f, fly.flyInTime / 2f);
        time += Time.deltaTime;
        float rate = Mathf.Clamp(time / fly.flyInTime, 0f, 1f);
        // rotation
        Vector3 forward = curve.GetTangent(rate);
        float angle = Vector3.SignedAngle(forward, Vector3.left, Vector3.back);
        fly.transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle));
        // position
        fly.transform.position = curve.GetPosition(rate);
        if (rate >= 1f)
            stateMachine.ChangeState(fly.readyState);
    }
}

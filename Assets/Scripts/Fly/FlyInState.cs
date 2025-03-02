using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class FlyInState : FlyState
{
    private BezierCurve curve;
    private float time;
    public FlyInState(FlyStateMachine stateMachine, Fly fly, string animatorName) : base(stateMachine, fly, animatorName)
    {
    }

    public override void Enter()
    {
        base.Enter();
        curve = new BezierCurve(fly.inPos, fly.curvePos, fly.stopPos);
        time = 0f;
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void Update()
    {
        base.Update();
        time += Time.deltaTime;
        float rate = Mathf.Clamp(time / fly.flyInTime, 0f, 1f);
        fly.transform.position = curve.GetPosition(rate);
        if (rate >= 1f)
            stateMachine.ChangeState(fly.readyState);
    }
}

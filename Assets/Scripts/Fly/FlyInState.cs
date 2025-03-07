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
        fly.transform.localScale = new Vector3(2f, 2f, 2f);
        time = 0f;
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void Update()
    {
        base.Update();
        fly.transform.DOScale(.3f, fly.flyInTime / 2);
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

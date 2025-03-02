using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class FlyState
{
    public FlyStateMachine stateMachine;
    public Fly fly;
    public string animatorName;

    public FlyState(FlyStateMachine stateMachine, Fly fly, string animatorName)
    {
        this.stateMachine = stateMachine;
        this.fly = fly;
        this.animatorName = animatorName;
    }

    public virtual void Enter()
    {
        fly.animator.SetBool(animatorName, true);
    }

    public virtual void Update()
    {
    }

    public virtual void Exit()
    {
        fly.animator.SetBool(animatorName, false);
    }
}

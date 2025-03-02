using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fly : MonoBehaviour
{
    public FlyStateMachine stateMachine { get; private set; }
    public FlyInState inState {  get; private set; }
    public FlyReadyState readyState { get; private set; }
    public FlySuckState suckState { get; private set; }
    public FlyDeadState deadState { get; private set; }
    public FlyOutState outState { get; private set; }
    public Animator animator { get; private set; }
    
    // Attr
    public float flyInTime { get; private set; }
    public Vector3 inPos;
    public Vector3 curvePos;
    public Vector3 stopPos;

    public Player player;

    private void Awake()
    {
        stateMachine = new FlyStateMachine();
        inState = new FlyInState(stateMachine, this, "In");
        readyState = new FlyReadyState(stateMachine, this, "Ready");
        suckState = new FlySuckState(stateMachine, this, "Suck");
        deadState = new FlyDeadState(stateMachine, this, "Dead");
        outState = new FlyOutState(stateMachine, this, "Out");
        
        flyInTime = 3f;
    }

    void Start()
    {
        animator = GetComponent<Animator>();
        player = FindAnyObjectByType<Player>();
        stateMachine.Initialize(inState);
    }

    // Update is called once per frame
    void Update()
    {
        stateMachine.currentState.Update();
    }

    public void DestroySelf()
    { 
        Destroy(gameObject);
    }
}

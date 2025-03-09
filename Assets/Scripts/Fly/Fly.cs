using DG.Tweening;
using DG.Tweening.Core.Easing;
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
    public float flyInTime;
    public float flyReadyTime;
    public Vector3 inPos;
    public Vector3 curvePos;
    public Vector3 stopPos;

    // ÊýÖµ
    // ÎüÑª¼ä¸ô
    public float suckDelTime { get; private set; }
    // ÎüÑªÉËº¦
    public float suckDamage { get; private set; }

    public Player player;
    public GameSwitch gameManager;
    public FlyGenerator generator;

    private void Awake()
    {
        stateMachine = new FlyStateMachine();
        inState = new FlyInState(stateMachine, this, "In");
        readyState = new FlyReadyState(stateMachine, this, "Ready");
        suckState = new FlySuckState(stateMachine, this, "Suck");
        deadState = new FlyDeadState(stateMachine, this, "Dead");
        outState = new FlyOutState(stateMachine, this, "Out");
        
        flyInTime = 2f;
        flyReadyTime = 0.8f;
    }

    void Start()
    {
        animator = GetComponent<Animator>();
        player = FindAnyObjectByType<Player>();
        gameManager = FindAnyObjectByType<GameSwitch>();
        generator = FindAnyObjectByType<FlyGenerator>();
        stateMachine.Initialize(inState);
    }

    // Update is called once per frame
    void Update()
    {
        stateMachine.currentState.Update();
        DataChange();
    }

    public void DestroySelf()
    {
        DOTween.KillAll();
        Destroy(gameObject);
    }

    private void DataChange()
    {
        if (gameManager.gameTime < 20f)
        {
            suckDelTime = 1f;
            suckDamage = 1f;
        }
        else if (gameManager.gameTime < 40f)
        {
            suckDelTime = 0.8f - 0.07f * ((gameManager.gameTime - 40f) / 10f);
            suckDamage = 2f + 0.5f * ((gameManager.gameTime - 40f) / 10f);
        }
        else if (gameManager.gameTime < 100f)
        {
            suckDelTime = 0.38f;
            suckDamage = 2f + 0.2f * ((gameManager.gameTime - 40f) / 10f);
        }
        else
        {
            suckDelTime = 0.4f;
            suckDamage = 4.5f;
        }
    }
}

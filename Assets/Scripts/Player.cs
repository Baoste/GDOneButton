using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    public FlyGenerator generator;
    public Scrollbar Scrollbar;
    public float health;
    public float hitCoolTime {  get; private set; }
    void Start()
    {
        health = 100;
        hitCoolTime = 0f;
    }

    void Update()
    {
        hitCoolTime -= Time.deltaTime;
        Scrollbar.size = health / 100f;
        if (hitCoolTime < 0f && Input.GetKeyDown(KeyCode.Space))
        {
            if (generator.flies.Count <= 0)
            {
                hitCoolTime = 3f;
                Debug.Log("FAIL");
            }
            else
            {
                Fly fly = generator.flies.Dequeue();
                if (fly.stateMachine.currentState != fly.suckState && fly.stateMachine.currentState != fly.readyState)
                {
                    fly.stateMachine.ChangeState(fly.outState);
                    hitCoolTime = 3f;
                    Debug.Log("FAIL");
                }
                else
                {
                    fly.stateMachine.ChangeState(fly.deadState);
                    Debug.Log("SUCCESS");
                }
            }
        }
    }
}

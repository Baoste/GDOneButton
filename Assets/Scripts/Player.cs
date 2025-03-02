using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class Player : MonoBehaviour
{
    public FlyGenerator generator;
    public TMP_Text text;
    public float health;
    void Start()
    {
        health = 100;
    }

    void Update()
    {
        text.text = health.ToString();
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (generator.flies.Count <= 0)
            {
                health -= 5f;
                Debug.Log("FAIL");
            }
            else
            {
                Fly fly = generator.flies.Dequeue();
                if (fly.stateMachine.currentState != fly.suckState)
                {
                    fly.stateMachine.ChangeState(fly.outState);
                    health -= 5f;
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

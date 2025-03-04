using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameSwitch : MonoBehaviour
{
    public float gameTime;
    // Start is called before the first frame update
    void Start()
    {
        gameTime = 0f;
    }

    // Update is called once per frame
    void Update()
    {
        gameTime += Time.deltaTime;
    }
}

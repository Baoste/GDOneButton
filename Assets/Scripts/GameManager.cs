using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

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

    public void Menu()
    {
        SceneManager.LoadScene("MenuScene");
    }
    public void Replay()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}

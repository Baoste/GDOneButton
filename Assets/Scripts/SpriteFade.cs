using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpriteFade : MonoBehaviour
{
    private float timer = 0;
    private SpriteRenderer sp;
    private void Start()
    {
        sp = GetComponent<SpriteRenderer>();
    }
    private void Update()
    {
        timer += Time.deltaTime;
        if (timer > .5f)
        {
            Color color = Color.white;
            color.a = 1.5f - timer;
            sp.color = color;
        }
        if (timer > 1.5f)
            Destroy(gameObject);
    }

}

using Cinemachine;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using static Unity.Burst.Intrinsics.X86.Avx;

public class Player : MonoBehaviour
{
    public GameObject palmPrint;
    public GameObject soundPrint;
    public GameObject angryPrint;
    public GameObject failUI;

    public AudioManager audioManager;
    public FlyGenerator generator;
    public Scrollbar Scrollbar;
    public float health;
    public int hitCount;
    public int flyDeadCount;
    public TMP_Text tmp;

    private CinemachineImpulseSource shake;
    private CinemachineImpulseSource impulseSource;
    private RandMath rmath;
    void Start()
    {
        health = 100f;
        hitCount = 0;
        flyDeadCount = 0;
        rmath = new RandMath();
        shake = GetComponent<CinemachineImpulseSource>();
        impulseSource = GetComponent<CinemachineImpulseSource>();
        angryPrint.SetActive(false);
        failUI.SetActive(false);
        // DG.Tweening.DOTween.SetTweensCapacity(tweenersCapacity: 3000, sequencesCapacity: 200);
    }

    void Update()
    {
        tmp.text = "x " + hitCount;
        Scrollbar.size = health / 100f;
        if (health > 0 && Input.GetKeyUp(KeyCode.Space))
        {
            audioManager.PlaySfx(audioManager.hit);
            if (generator.flies.Count <= 0)
            {
                GenerateHitEffect(null);
                health -= 10f;
                hitCount = 0;
            }
            else
            {
                Fly fly = generator.flies.Peek();
                if (fly.stateMachine.currentState != fly.suckState && fly.stateMachine.currentState != fly.readyState)
                {
                    GenerateHitEffect(fly, false);
                    fly.stateMachine.ChangeState(fly.outState);
                    health -= 10f;
                    hitCount = 0;
                }
                // ³É¹¦
                else
                {
                    hitCount += 1;
                    flyDeadCount += 1;
                    GenerateHitEffect(fly);
                    fly.stateMachine.ChangeState(fly.deadState);
                }
            }
        }
        if (health <= 0)
        {
            failUI.SetActive(true);
            generator.enabled = false;
        }
    }

    private void GenerateHitEffect(Fly fly, bool flag=true)
    {
        if (fly == null)
        {
            audioManager.PlaySfx(audioManager.yall);
            angryPrint.SetActive(true);
            impulseSource.m_DefaultVelocity = new Vector3(.5f, .5f, 0);
            shake.GenerateImpulse();
            Vector3 pos = rmath.RandomPosition(generator.width, generator.height, generator.transform.position);
            Quaternion rot = rmath.RandomRotation();
            Instantiate(palmPrint, pos, rot);
            Instantiate(soundPrint, pos, rot);
        }
        else
        {
            if (!flag)
            {
                audioManager.PlaySfx(audioManager.yall);
                angryPrint.SetActive(true);
                impulseSource.m_DefaultVelocity = new Vector3(.5f, .5f, 0);
            }
            else
            {
                impulseSource.m_DefaultVelocity = new Vector3(.1f, .1f, 0);
                angryPrint.SetActive(false);
            }
            shake.GenerateImpulse();
            Quaternion rot = rmath.RandomRotation();
            Instantiate(palmPrint, fly.transform.position, rot);
            Instantiate(soundPrint, fly.transform.position, rot);
        }
    }
}

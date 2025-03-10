using Cinemachine;
using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    public GameObject palmPrint;
    public GameObject soundPrint;
    public GameObject angryPrint;
    public GameObject failUI;

    [Header("Combo")]
    public SpriteRenderer combo;
    public Sprite combox1;
    public Sprite combox5;
    public Sprite combox10;

    public SpriteRenderer frog;
    private Color frogColor;
    private float frogAlpha;
    
    public bool isflushing
    {
        get
        {
            return _isflushing;
        }
        set
        {
            _isflushing = value;
            if (value)
                audioManager.PlaySfx(audioManager.flush);
        }
    }
    private bool _isflushing;

    public TMP_Text killText;
    public AudioManager audioManager;
    public FlyGenerator generator;
    public Scrollbar Scrollbar;
    public float health;
    public int hitCount {  get; private set; }
    public int flyDeadCount;

    private CinemachineImpulseSource shake;
    private CinemachineImpulseSource impulseSource;
    private RandMath rmath;
    private float spaceDownTime;
    private float flushTime;
    void Start()
    {
        health = 100f;
        hitCount = 0;
        flyDeadCount = 0;
        spaceDownTime = 0;
        flushTime = 0;
        isflushing = false; 
        frogColor = Color.white;
        frogAlpha = 0;
        rmath = new RandMath();
        shake = GetComponent<CinemachineImpulseSource>();
        impulseSource = GetComponent<CinemachineImpulseSource>();
        angryPrint.SetActive(false);
        failUI.SetActive(false);
        // DG.Tweening.DOTween.SetTweensCapacity(tweenersCapacity: 3000, sequencesCapacity: 200);
    }

    void Update()
    {
        Scrollbar.size = health / 100f;
        if (Input.GetKeyDown(KeyCode.Space))
            spaceDownTime = 0;
        if (Input.GetKey(KeyCode.Space))
            spaceDownTime += Time.deltaTime;

        if (health > 0 && Input.GetKeyUp(KeyCode.Space) && spaceDownTime < .5f)
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
                // 成功
                else
                {
                    hitCount = Mathf.Clamp(hitCount + 1, 0, 10);
                    flyDeadCount += 1;
                    GenerateHitEffect(fly);
                    fly.stateMachine.ChangeState(fly.deadState);
                }
            }

            // combo
            if (hitCount <= 0)
            {
                combo.enabled = false;
            }
            else if (hitCount < 5)
            {
                combo.sprite = combox1;
                combo.transform.localScale = new Vector3(2, 2, 2);
                combo.enabled = true;
                combo.transform.rotation = rmath.RandomRotation(-15f, 15f);
                combo.transform.DOScale(1f, .5f);
            }
            else if (hitCount < 10)
            {
                combo.sprite = combox5;
                combo.transform.localScale = new Vector3(2, 2, 2);
                combo.transform.rotation = rmath.RandomRotation(-15f, 15f);
                combo.transform.DOScale(1f, .5f);
            }
            else
            {
                combo.sprite = combox10;
                combo.transform.localScale = new Vector3(2, 2, 2);
                combo.transform.rotation = rmath.RandomRotation(-15f, 15f);
                combo.transform.DOScale(1f, .5f);
            }
        }
        // 长按冲洗
        else if (Input.GetKey(KeyCode.Space) && spaceDownTime > .8f)
        {
            flushTime += Time.deltaTime;
            frogAlpha += Time.deltaTime;

            if (!isflushing)
                isflushing = true;

            frogColor.a = Mathf.Clamp(frogAlpha, 0f, 1f);
            frog.color = frogColor;

            Fly[] allFlies = FindObjectsOfType<Fly>();
            foreach (Fly f in allFlies)
            {
                if (f.stateMachine.currentState == f.deadState && flushTime > .1f)
                {
                    flushTime = 0f;
                    f.stateMachine.ChangeState(f.readyState);
                }
            }
        }
        else if (Input.GetKeyUp(KeyCode.Space))
        {
            isflushing = false;
            frog.DOFade(0, 1f);
            frogAlpha = 0f;
        }

        // fail
        if (health <= 0)
        {
            killText.text = "You Killed\n" + flyDeadCount + "\nMosquitoes";
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
            Quaternion rot = rmath.RandomRotation(-90f, 0f);
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
            Quaternion rot = rmath.RandomRotation(-90f, 0f);
            Instantiate(palmPrint, fly.transform.position, rot);
            Instantiate(soundPrint, fly.transform.position, rot);
        }
    }
}


using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public AudioSource BgmAudio;
    public AudioSource SfxAudio;

    public AudioClip bgm;
    public AudioClip hit;
    public AudioClip yall;
    public AudioClip fly;

    void Start()
    {
        BgmAudio.clip = bgm;
        BgmAudio.Play();
    }

    public void PlaySfx(AudioClip clip)
    {
        SfxAudio.PlayOneShot(clip);
    }
}

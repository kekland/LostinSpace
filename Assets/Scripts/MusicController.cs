using UnityEngine;
using System.Collections;

public class MusicController : MonoBehaviour {

    [Header("Audios here : ")]
    public AudioClip[] clips;
    public AudioClip lastClip;
    AudioSource AS;
    void Start()
    {
        AS = GetComponent<AudioSource>();
        PlayNextSound();
    }

    void PlayNextSound()
    {
        AudioClip soundToPlay = clips[Random.Range(0, clips.Length)];
        if(soundToPlay == lastClip)
        {
            PlayNextSound();
        }
        else
        {
            AS.clip = soundToPlay;
            lastClip = soundToPlay;
            AS.Play();
            Invoke("PlayNextSound", soundToPlay.length + 1f);
        }
    }
}

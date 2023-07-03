using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayRandomAudio : MonoBehaviour
{
    [SerializeField] private AudioSource sfx;
    [SerializeField] private AudioClip[] clips;
    [Space]
    [SerializeField] private bool autoPlay = true;

    protected virtual void OnEnable()
    {
        PlayAudio();
    }

    protected virtual void PlayAudio()
    {
        int index = Random.Range(0, clips.Length);
        sfx.clip = clips[index];
        if(autoPlay) sfx.Play();
    }
}

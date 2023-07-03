using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseAnimAndAudioWhenGamePause : MonoBehaviour
{
    [SerializeField] private Animation anim;
    [SerializeField] private AudioSource sfx;
    private bool pauseState;

    private void Awake()
    {        
        PauseChange(UIManager.Instance.GamePaused);
    }

    private void Update()
    {
        if(pauseState != UIManager.Instance.GamePaused)
        {
            PauseChange(UIManager.Instance.GamePaused);
        }
    }

    private void PauseChange(bool paused)
    {
        pauseState = paused;
        
        if(paused)
        {
            if (anim)
            {
                anim.enabled = false;
            }
            if (sfx) sfx.Pause();
        }
        else
        {
            if (anim)
            {
                anim.enabled = true;
            }
            if (sfx) sfx.Play();
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class SettingsSpecialActions : MonoBehaviour
{
    [SerializeField] private Text creditButtonText;
    [Space]
    [SerializeField] private GameObject credits;
    [SerializeField] private GameObject settings;
    private bool creditsActive = false;

    [Space]
    [SerializeField] private AudioMixer mixer;
    [SerializeField] private Slider masterSlider;
    [SerializeField] private Slider musicSlider;
    [SerializeField] private Slider gameplaySlider;
    [SerializeField] private Slider backgroundSlider;

    public void ToggleCredits()
    {
        creditsActive = !creditsActive;

        if(creditsActive)
        {
            creditButtonText.text = "View Settings";
            credits.SetActive(true);
            settings.SetActive(false);
        }
        else
        {
            creditButtonText.text = "View Credits";
            credits.SetActive(false);
            settings.SetActive(true);
        }
    }

    private void Awake()
    {
        SetSliders();
    }

    private void SetSliders()
    {
        float volume;
        mixer.GetFloat("Master", out volume);
        masterSlider.value = volume;
        mixer.GetFloat("Music", out volume);
        musicSlider.value = volume;
        mixer.GetFloat("Gameplay", out volume);
        gameplaySlider.value = volume;
        mixer.GetFloat("Background", out volume);
        backgroundSlider.value = volume;
    }

    public void SetVolume(string group)
    {
        switch(group.ToLower())
        {
            case "master":
                mixer.SetFloat("Master", masterSlider.value);
                break;
            case "music":
                mixer.SetFloat("Music", musicSlider.value);
                break;
            case "gameplay":
                mixer.SetFloat("Gameplay", gameplaySlider.value);
                break;
            case "background":
                mixer.SetFloat("Background", backgroundSlider.value);
                break;
        }
    }
}

public enum AudioGroupName
{
    Master,
    Music,
    Gameplay,
    Background
}

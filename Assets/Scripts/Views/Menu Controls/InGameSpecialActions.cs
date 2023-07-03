using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class InGameSpecialActions : MonoBehaviour
{
    [SerializeField] private MenuNavigator menuNavigator;
    [SerializeField] private SubMenuNavigator inGameUI;
    [SerializeField] private SubMenuNavigator pauseMenu;
    [Space]
    [SerializeField] private AudioSource music;

    public bool Paused => paused;
    private bool paused = false;

    private void OnEnable()
    {
        PlayerManager.Instance.RemoveAllPlayerData();
        PlayerManager.Instance.EnablePlayerJoining();
    }

    public void ManualEndGame()
    {
        PauseGame(false);
        LevelManager.Instance.EndLevel();
    }

    public void PauseGame(bool pause)
    {
        paused = pause;
        if(pause)
        {
            music.Pause();
            menuNavigator.Navigate(pauseMenu);
        }
        else
        {
            music.Play();
            menuNavigator.Navigate(inGameUI);
        }

        Time.timeScale = pause ? 0 : 1;
    }

    public void DisplayLevelStart()
    {
        menuNavigator.Navigate(inGameUI);
        music.Play();
    }
}

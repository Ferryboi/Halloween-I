using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;

public class UIManager : Singleton<UIManager>
{
    [SerializeField] private CameraControlSystem cControl;

    public EventSystem EventSystem => eventSystem;
    [SerializeField] private EventSystem eventSystem;
    [Space]
    [SerializeField] private GameObject startOnUI;
    [Space]
    [SerializeField] private GameObject menuUI;
    [SerializeField] private GameObject gameplayUI;
    [SerializeField] private GameObject gameOverUI;
    [Space]
    [SerializeField] private InGameSpecialActions inGameActions;
    [Space]
    [SerializeField] private LightningVFX lightningVFX;
    public LightningVFX LightningVFX => lightningVFX;

    public bool GamePaused => inGameActions.enabled ? inGameActions.Paused : false;

    private void Awake()
    {
        menuUI.SetActive(false);
        gameplayUI.SetActive(false);
        gameOverUI.SetActive(false);

        startOnUI.SetActive(true);
    }

    public void StartGame()
    {
        menuUI.SetActive(false);
        gameplayUI.SetActive(true);
        gameOverUI.SetActive(false);

        cControl.PivotCameraToControl(CameraPivots.Gameplay);
    }

    public void MainMenu()
    {
        menuUI.SetActive(true);
        gameplayUI.SetActive(false);
        gameOverUI.SetActive(false);

        LevelManager.Instance.ClearAllEnemies();

        cControl.PivotCameraToControl(CameraPivots.MainMenu);
    }

    public void GameOver()
    {
        menuUI.SetActive(false);
        gameplayUI.SetActive(false);
        gameOverUI.SetActive(true);
    }

    public void PauseGame()
    {
        if(inGameActions.enabled) inGameActions.PauseGame(!GamePaused);
    }

    public void PlayersJoinedAndReady()
    {
        PlayerManager.Instance.DisablePlayerJoining();
        LevelManager.Instance.BeginLevel();

        inGameActions.DisplayLevelStart();
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenuSpecialActions : MonoBehaviour
{
    public void PlayClassic()
    {
        LevelManager.Instance.StartLevel(LevelType.Round);
        UIManager.Instance.StartGame();
    }

    public void PlayEndless()
    {
        LevelManager.Instance.StartLevel(LevelType.Endless);
        UIManager.Instance.StartGame();
    }

    public void PlayExtreme()
    {
        LevelManager.Instance.StartLevel(LevelType.Extreme);
        UIManager.Instance.StartGame();
    }

    public void QuitGame()
    {
        Debug.Log("Ending Game");
        Application.Quit();
    }

    private void OnEnable()
    {
        UIManager.Instance.LightningVFX.PerformWorldLightning();
    }
}

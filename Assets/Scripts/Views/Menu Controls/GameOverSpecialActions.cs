using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameOverSpecialActions : MonoBehaviour
{
    public void PlayAgain()
    {
        LevelManager.Instance.RestartLevel();
        UIManager.Instance.StartGame();
        ResetTombstones();
    }

    public void ReturnToMenu()
    {
        UIManager.Instance.MainMenu();
        ResetTombstones();
    }

    private void ResetTombstones()
    {
        TombstoneManager.Instance.RespawnTombstones();
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.UI;

public class PlayerUIComponent : MonoBehaviour
{
    [SerializeField] private Material[] playerMaterials;
    private PlayerData data;

    private void Awake()
    {
        transform.SetParent(PlayerManager.Instance.transform);
        int playerNum = PlayerManager.Instance.GetNextAvailablePlayerNum();

        PlayerInput input = GetComponent<PlayerInput>();
        Material material = playerMaterials[playerNum];

        data = new PlayerData(playerNum, material, gameObject, input);
        PlayerManager.Instance.AddPlayer(data);
        PlayerManager.Instance.SpawnInPlayer(data, PlayerManager.Instance.SpawnPos.position, PlayerManager.Instance.SpawnPos.rotation);
    }

    public void OnPause(InputValue value)
    {
        if(PlayerManager.Instance.PlayerJoiningActive())
        {
            UIManager.Instance.PlayersJoinedAndReady();
        }
        else
        {
            UIManager.Instance.PauseGame();
        }
    }

    public void OnLeave(InputValue value)
    {
        if (!LevelManager.Instance.LevelActive) PlayerManager.Instance.RemovePlayer(data.GetPlayerNum());
    }
}

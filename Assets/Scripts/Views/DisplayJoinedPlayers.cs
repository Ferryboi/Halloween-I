using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisplayJoinedPlayers : MonoBehaviour
{
    [SerializeField] private GameObject playerJoinedDisplayPrefab;
    private PlayerJoinedDisplayIcon[] playerJoinedDisplays;

    private void Awake()
    {
        playerJoinedDisplays = new PlayerJoinedDisplayIcon[4];

        PlayerManager.Instance.OnPlayerAdded += PlayerJoined;
        PlayerManager.Instance.OnPlayerRemoved += PlayerRemoved;
    }

    private void OnDestroy()
    {
        PlayerManager.Instance.OnPlayerAdded -= PlayerJoined;
        PlayerManager.Instance.OnPlayerRemoved -= PlayerRemoved;
    }

    private void PlayerJoined(PlayerData player)
    {
        if (playerJoinedDisplays[player.GetPlayerNum()] != null) return;

        PlayerJoinedDisplayIcon newPlayerDisplay = Instantiate(playerJoinedDisplayPrefab, transform).GetComponent<PlayerJoinedDisplayIcon>();
        newPlayerDisplay.SetDisplayIcon(player.GetPlayerNum());
        playerJoinedDisplays[player.GetPlayerNum()] = newPlayerDisplay;
    }

    private void PlayerRemoved(PlayerData player)
    {
        if (playerJoinedDisplays[player.GetPlayerNum()] == null) return;

        Destroy(playerJoinedDisplays[player.GetPlayerNum()].gameObject);
        playerJoinedDisplays[player.GetPlayerNum()] = null;
    }
}

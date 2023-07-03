using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerManager : Singleton<PlayerManager>
{
    [SerializeField] private PlayerInputManager inputManager;
    [SerializeField] private GameObject playerPrefab;
    public Transform SpawnPos => spawnPos;
    [Space]
    [SerializeField] private Transform spawnPos;
    [SerializeField] private float spawnSpacing;
    [Space]
    [SerializeField] private Transform respawnPos;
    [SerializeField] private float respawnTimer = 5f;

    private PlayerData[] players = new PlayerData[4];

    public delegate void OnPlayerChangeDelegate(PlayerData pData);
    public OnPlayerChangeDelegate OnPlayerAdded;
    public OnPlayerChangeDelegate OnPlayerRemoved;

    private void Awake()
    {
        SetInstance(this);
    }

    public void AddPlayer(PlayerData data)
    {
        Debug.Log("Player " + data.GetPlayerNum() + " joined!");
        players[data.GetPlayerNum()] = data;
        OnPlayerAdded?.Invoke(data);
    }

    public void RemovePlayer(int playerNum)
    {
        if (players[playerNum] == null) return;

        Debug.Log("Player " + playerNum + " left!");

        OnPlayerRemoved?.Invoke(players[playerNum]);
        players[playerNum].RemovePlayerData();
        players[playerNum] = null;
    }

    public void EnablePlayerJoining()
    {
        if(inputManager) inputManager.EnableJoining();
    }

    public void DisablePlayerJoining()
    {
        if (inputManager) inputManager.DisableJoining();
    }

    public bool PlayerJoiningActive()
    {
        return inputManager.joiningEnabled;
    }

    public void SpawnPlayersBeginning()
    {
        int playerCount = GetPlayerCount();

        for(int i = 0; i < players.Length; i++)
        {
            if (players[i] == null) continue;

            if (playerCount == 1)
            {
                SpawnInPlayer(players[i], spawnPos.position, spawnPos.rotation);
            }
            else
            {
                float spacing = spawnSpacing * (players[i].GetPlayerNum() - (playerCount / 2) + 0.5f);
                SpawnInPlayer(players[i], spawnPos.position + (spacing * spawnPos.right), spawnPos.rotation);
            }
        }
    }

    public void StopSpawning()
    {
        StopAllCoroutines();
    }

    public void SpawnInPlayer(PlayerData data, Vector3 position, Quaternion rotation)
    {
        Player player = PlayerInput.Instantiate(playerPrefab, controlScheme: data.GetPlayerInput().currentControlScheme, pairWithDevices: data.Devices).GetComponent<Player>();
        data.SetPlayerInstance(player);

        player.transform.position = position;
        player.transform.rotation = rotation;
        player.gameObject.name = "Player " + (data.GetPlayerNum() + 1);
        player.SetPlayer(data.GetPlayerNum(), data.GetPlayerMaterial());
    }

    public void DestroyPlayer(int playerNum)
    {
        Player player = players[playerNum].GetPlayerInstance();
        if (player == null) return;

        Destroy(player.gameObject);
        players[playerNum].SetPlayerInstance(null);

        StartCoroutine(RespawnPlayer(playerNum));
    }

    private IEnumerator RespawnPlayer(int playerNum)
    {
        yield return new WaitForSeconds(respawnTimer);

        PlayerData data = players[playerNum];
        if (data == null) yield break;

        Tombstone chosenTombstone = TombstoneManager.Instance.FindActiveTombstone();
        if (chosenTombstone == null) yield break;

        chosenTombstone.DestroyTombstone();
        chosenTombstone.RemoveZombie();
        if (UIManager.Instance.LightningVFX) UIManager.Instance.LightningVFX.PerformUILightning(chosenTombstone.SpawnPos);
        SpawnInPlayer(data, chosenTombstone.SpawnPos, chosenTombstone.transform.rotation);
        LevelManager.Instance.ClearAllEnemies(MonsterType.All);
    }

    public int GetNextAvailablePlayerNum()
    {
        for(int i = 0; i < players.Length; i++)
        {
            if (players[i] == null) return i;
        }
        return -1;
    }

    public int GetPlayerCount()
    {
        int playerCount = 0;
        for (int i = 0; i < players.Length; i++)
        {
            if (players[i] != null) playerCount++;
        }
        return playerCount;
    }

    public int GetActivePlayerCount()
    {
        int count = 0;
        for(int i = 0; i < players.Length; i++)
        {
            if(players[i] != null && players[i].GetPlayerInstance() != null)
            {
                count++;
            }
        }
        return count;
    }

    public List<PlayerData> GetAllActivePlayers()
    {
        List<PlayerData> playerList = new List<PlayerData>(players);

        for (int i = playerList.Count - 1; i >= 0; i--)
        {
            if (playerList[i] == null || playerList[i].GetPlayerInstance() == null) playerList.RemoveAt(i);
        }

        return playerList;
    }

    public Player GetRandomPlayer()
    {
        List<PlayerData> playerList = GetAllActivePlayers();
        if (playerList.Count <= 0) return null;

        int index = Random.Range(0, playerList.Count);
        return playerList[index].GetPlayerInstance();
    }

    public PlayerData GetPlayerData(int playerNum)
    {
        return players[playerNum];
    }

    public PlayerData[] GetAllPlayerData()
    {
        return players;
    }

    public void RemoveAllPlayerData()
    {
        for(int i = 0; i < players.Length; i++)
        {
            if (players[i] == null) continue;

            RemovePlayer(players[i].GetPlayerNum());
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerData
{
    private int playerNum;
    private Player playerInstance = null;
    private PlayerInput input;
    public InputDevice[] Devices { get; private set; }
    private Material material;

    private GameObject dataContainer;

    public PlayerData(int playerNum, Material material, GameObject dataContainer, PlayerInput input)
    {
        this.playerNum = playerNum;
        this.material = material;
        this.dataContainer = dataContainer;
        this.input = input;
        Devices = input.devices.ToArray();
    }

    public void SetPlayerNum(int playerNum)
    {
        this.playerNum = playerNum;
    }

    public int GetPlayerNum()
    {
        return playerNum;
    }

    public void SetPlayerPrefab(Material prefab)
    {
        this.material = prefab;
    }

    public Material GetPlayerMaterial()
    {
        return material;
    }

    public void SetPlayerInput(PlayerInput input)
    {
        this.input = input;
    }

    public PlayerInput GetPlayerInput()
    {
        return input;
    }

    public void SetPlayerInstance(Player playerInstance)
    {
        this.playerInstance = playerInstance;
    }

    public Player GetPlayerInstance()
    {
        return playerInstance;
    }

    public void RemovePlayerData()
    {
        if (playerInstance != null) Object.Destroy(playerInstance.gameObject);
        Object.Destroy(dataContainer);
    }
}

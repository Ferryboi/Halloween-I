using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : Entity
{
    private int playerNum;
    
    [Space]
    [SerializeField] private Light flashlight;
    [SerializeField] private float strongFlashlightIntensity;
    private float defaultIntensity;

    [Space]
    [SerializeField] private SkinnedMeshRenderer skinRenderer;

    protected override void Subscribe()
    {
        base.Subscribe();
        if(flashlight) defaultIntensity = flashlight.intensity;
    }

    public override void KillEntity()
    {
        PlayerManager.Instance.DestroyPlayer(playerNum);
    }

    protected override void SetEntityType()
    {
        _entityType = EntityType.Player;
    }

    public void SetPlayer(int playerNum, Material material)
    {
        this.playerNum = playerNum;
        skinRenderer.material = material;
    }

    public int GetPlayerNum()
    {
        return playerNum;
    }

    public void ToggleFlashlight(bool strong)
    {
        if (!flashlight) return;

        if(strong)
        {
            flashlight.intensity = strongFlashlightIntensity;
        }
        else
        {
            flashlight.intensity = defaultIntensity;
        }
    }
}

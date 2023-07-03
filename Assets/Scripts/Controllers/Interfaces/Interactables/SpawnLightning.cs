using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnLightning : RoundCard
{
    protected override void ActivateCard()
    {
        LightningSpawner spawner = FindObjectOfType<LightningSpawner>();
        if (spawner == null) return;

        spawner.ForceSpawn();
        LevelManager.Instance.ManualStartNextRound();
        Destroy(gameObject);
    }
}

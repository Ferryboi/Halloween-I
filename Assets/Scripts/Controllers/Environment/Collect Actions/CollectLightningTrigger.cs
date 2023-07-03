using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectLightningTrigger : CollectTrigger
{
    protected override void OnCollect(Player player)
    {
        CreateLightningAt(transform.position);
        RemoveAllGhosts();
    }

    private void CreateLightningAt(Vector3 pos)
    {
        LightningVFX lightningVFX = FindObjectOfType<LightningVFX>();
        if (lightningVFX) lightningVFX.PerformUILightning(pos);
    }

    private void RemoveAllGhosts()
    {
        LevelManager.Instance.ClearAllEnemies(MonsterType.All);
    }
}

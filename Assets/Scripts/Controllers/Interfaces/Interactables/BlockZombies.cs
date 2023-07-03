using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockZombies : RoundCard
{
    ZombieSpawner zombieSpawner;

    protected override void ActivateCard()
    {
        MonsterBehaviorController.Instance.ApplyBlockZombie(LevelManager.Instance.RoundDuration);
        LevelManager.Instance.ManualStartNextRound();
    }

    protected override bool CanPurchase()
    {
        zombieSpawner = MonsterBehaviorController.Instance.ZombieSpawner;
        if (zombieSpawner == null) return false;

        return base.CanPurchase() && zombieSpawner.IsActive == true;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TombstoneRebuilder : RoundCard
{
    private Tombstone tombstone;

    protected override bool CanPurchase()
    {
        tombstone = TombstoneManager.Instance.FindDestroyedTombstone();
        return base.CanPurchase() && tombstone != null;
    }

    protected override void ActivateCard()
    {
        if (tombstone.AssociatedZombie != null)
        {
            tombstone.AssociatedZombie.KillEntity();
        }

        tombstone.RebuildTombstone();
        LevelManager.Instance.ManualStartNextRound();
    }
}

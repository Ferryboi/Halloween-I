using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZombieArm : Monster
{
    private Tombstone tombstone;
    private bool unearthed;

    public void SetUnearthed(bool isUnearthed)
    {
        unearthed = isUnearthed;
    }

    public void SetTombstone(Tombstone tombstone)
    {
        this.tombstone = tombstone;
    }

    public override void KillEntity()
    {
        if (unearthed && tombstone)
        {
            tombstone.DestroyTombstone();
        }
        Destroy(gameObject);
    }

    protected override void SetMonsterType()
    {
        _monsterType = MonsterType.Zombie;
    }
}

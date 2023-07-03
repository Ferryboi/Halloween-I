using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BatSwarm : Monster
{
    public override void KillEntity()
    {
        Destroy(gameObject);
    }

    protected override void SetMonsterType()
    {
        _monsterType = MonsterType.Bats;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ghost : Monster
{
    public override void KillEntity()
    {
        Destroy(gameObject);
    }

    protected override void SetMonsterType()
    {
        _monsterType = MonsterType.Ghost;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Zombie : Monster
{

    public override void KillEntity()
    {
        Destroy(gameObject);
    }

    protected override void SetMonsterType()
    {
        _monsterType = MonsterType.Zombie;
    }
}

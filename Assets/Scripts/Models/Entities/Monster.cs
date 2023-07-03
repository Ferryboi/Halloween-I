using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Monster : Entity
{
    public MonsterType MonsterType => _monsterType;
    protected MonsterType _monsterType;

    protected override void SetEntityType()
    {
        _entityType = EntityType.Monster;
        SetMonsterType();
    }

    protected abstract void SetMonsterType();
}

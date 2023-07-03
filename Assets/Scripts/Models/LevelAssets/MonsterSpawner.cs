using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class MonsterSpawner : MonoBehaviour
{
    public MonsterType MonsterType => monsterType;
    [SerializeField] private MonsterType monsterType;

    [HideInInspector] public bool IsActive;
    public float SpawnRate => spawnRate;
    private float spawnRate = 0;

    public float Chance => chance;
    private float chance = 100f;

    private void Start()
    {
        IsActive = true;
        StartCoroutine(SpawnerLoop());
    }

    public void SetSpawnRate(float spawnRate)
    {
        this.spawnRate = spawnRate;
    }

    public void SetChance(float chance)
    {
        this.chance = chance;
    }

    private IEnumerator SpawnerLoop()
    {
        while(true)
        {
            if (IsActive == false || spawnRate <= 0) yield return new WaitForSeconds(1);
            else
            {
                yield return new WaitForSeconds(1 / spawnRate);

                if (LevelManager.Instance.RoundActive && Random.Range(0, 100) <= chance) SpawnMonster();
            }
        }
    }

    protected abstract void SpawnMonster();
}

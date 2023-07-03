using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GhostSpawner : MonsterSpawner
{
    [SerializeField] private GameObject ghostPrefab;
    [SerializeField] private List<Transform> spawnPositions;

    protected override void SpawnMonster()
    {
        int index = Random.Range(0, spawnPositions.Count);
        Transform spawnPos = spawnPositions[index].transform;

        Instantiate(ghostPrefab, spawnPos.position, spawnPos.rotation);
    }
}

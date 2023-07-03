using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicMonsterSpawner : MonsterSpawner
{
    [SerializeField] private GameObject monsterPrefab;

    private float levelWidth;
    private float levelHeight;
    private const float LEVEL_MARGIN = 0.5f;

    private void Awake()
    {
        levelHeight = LevelManager.Instance.LevelHalfHeight - LEVEL_MARGIN;
        levelWidth = LevelManager.Instance.LevelHalfWidth - LEVEL_MARGIN;
    }

    protected override void SpawnMonster()
    {
        float xPos = Random.Range(transform.position.x + -levelWidth, transform.position.x + levelWidth);
        float zPos = Random.Range(transform.position.z + -levelHeight, transform.position.z + levelHeight);

        Instantiate(monsterPrefab, new Vector3(xPos, transform.position.y, zPos), transform.rotation);
    }
}

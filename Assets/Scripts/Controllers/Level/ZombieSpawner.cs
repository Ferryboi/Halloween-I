using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZombieSpawner : MonsterSpawner
{
    [SerializeField] private GameObject zombiePrefab;
    private TombstoneManager tManager;

    private void Awake()
    {
        tManager = TombstoneManager.Instance;
    }

    protected override void SpawnMonster()
    {
        Tombstone chosenTombstone = tManager.FindZombielessTombstone();
        if (chosenTombstone == null) return;

        ZombieArm arm = Instantiate(zombiePrefab, chosenTombstone.SpawnPos, Quaternion.Euler(-chosenTombstone.transform.right)).GetComponentInParent<ZombieArm>();
        chosenTombstone.AddZombie(arm);
        arm.SetTombstone(chosenTombstone);
    }
}

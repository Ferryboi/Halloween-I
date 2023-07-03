using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeactivateSpawnerInGamemode : MonoBehaviour
{
    [SerializeField] private LevelType type;
    [SerializeField] private MonsterSpawner spawner;

    private void Awake()
    {
        LevelManager.Instance.OnStartLevel += ToggleSpawner;
    }

    private void OnDestroy()
    {
        LevelManager.Instance.OnStartLevel -= ToggleSpawner;
    }

    private void ToggleSpawner()
    {
        LevelType currentType = LevelManager.Instance.LevelType;
        if(currentType == type)
        {
            spawner.IsActive = false;
        }
        else
        {
            spawner.IsActive = true;
        }
    }
}

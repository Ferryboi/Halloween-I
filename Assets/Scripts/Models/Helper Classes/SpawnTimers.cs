using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[Serializable]
public class SpawnTimers
{
    public float Chance => chance;
    [SerializeField] private float chance;

    public float Interval => interval;
    [SerializeField] private float interval;

    public float Duration => duration;
    [SerializeField] private float duration;

    public IEnumerator StartSpawner(Action perInterval, Action callback)
    {
        if (duration <= 0) duration = float.MaxValue;

        for (float i = 0; i < duration; i += interval)
        {
            yield return new WaitForSeconds(interval);
            perInterval();
        }
        callback();
    }

    public bool ShouldSpawn()
    {
        if (!LevelManager.Instance.LevelActive) return false;

        float chance = UnityEngine.Random.Range(0, 100);
        return chance < this.chance;
    }
}

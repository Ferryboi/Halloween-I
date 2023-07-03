using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using SpecialRoundSettings;

public abstract class RoundSystem : ScriptableObject
{
    public LevelType Type => _type;
    [SerializeField] protected LevelType _type;

    [SerializeField] protected RoundSpawnerData[] spawnerData;
    [SerializeField] protected float _startingRoundDuration;
    [SerializeField] protected float _roundDurationIncr;
    [SerializeField] protected float _playerMultiplier;
    protected float _playerMultiplierValue;

    public delegate void OnRoundActiveChange();
    public OnRoundActiveChange OnRoundComplete;
    public OnRoundActiveChange OnRoundStart;

    public int RoundNum => _roundNum;
    protected int _roundNum = 0;

    public float RoundDuration => _roundDuration;
    protected float _roundDuration;

    public bool RoundActive => _roundActive;
    protected bool _roundActive = false;

    protected List<MonsterSpawner> _spawners;

    public void ResetRoundNum()
    {
        _roundNum = 0;
    }

    public IEnumerator StartRoundSystem(MonsterSpawner[] spawners)
    {
        ResetRoundNum();
        _roundDuration = _startingRoundDuration;

        _spawners = new List<MonsterSpawner>(spawners);
        for (int i = 0; i < _spawners.Count; i++)
        {
            RoundSpawnerData data = FindSpawnerData(_spawners[i].MonsterType);
            if (data == null)
            {
                _spawners[i].IsActive = false;
                continue;
            }

            _spawners[i].SetSpawnRate(data.StartingSpawnRate);
            _spawners[i].SetChance(data.Chance);
            _spawners[i].IsActive = true;
        }

        //Set the player multiplier value to 1 for one player. Add in an additional value for every additional player
        _playerMultiplierValue = 1 + ((PlayerManager.Instance.GetPlayerCount() - 1) * _playerMultiplier);

        SystemSetup();
        return StartNextRound();
    }

    public void EndRoundSystem()
    {
        for(int i = 0; i < _spawners.Count; i++)
        {
            _spawners[i].IsActive = false;
        }
        _roundActive = false;
    }

    protected virtual void SystemSetup() { }

    public abstract IEnumerator StartNextRound(SpecialRound specialRound = null);

    protected RoundSpawnerData FindSpawnerData(MonsterType type)
    {
        for(int i = 0; i < spawnerData.Length; i++)
        {
            if(spawnerData[i].MonsterType == type)
            {
                return spawnerData[i];
            }
        }
        return null;
    }
}

[Serializable]
public class RoundSpawnerData
{
    public MonsterType MonsterType => monsterType;
    [SerializeField] private MonsterType monsterType;

    public float StartingSpawnRate => startingSpawnRate;
    [SerializeField] private float startingSpawnRate;

    public float SpawnRateIncrease => spawnRateIncrease;
    [SerializeField] private float spawnRateIncrease;

    public float Chance => chance;
    [SerializeField] private float chance;
}



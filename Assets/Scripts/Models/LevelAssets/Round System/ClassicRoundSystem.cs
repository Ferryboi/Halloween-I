using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SpecialRoundSettings;
using System;

[CreateAssetMenu(fileName = "NewRoundSystem", menuName = "Data/RoundSystem/Classic")]
public class ClassicRoundSystem : RoundSystem
{
    [Space]
    [SerializeField] private SpecialRound[] specialRounds;
    [SerializeField] private int minSpecialRoundInterval = 2;
    [SerializeField] private int maxSpecialRoundInterval = 6;
    private int specialRoundInterval;
    private int intervalCounter;

    private SpecialRound currentSpecialRound = null;


    protected override void SystemSetup()
    {
        SetSpecialRoundInterval();
        intervalCounter = 0;
    }

    private void SetSpecialRoundInterval()
    {
        specialRoundInterval = UnityEngine.Random.Range(minSpecialRoundInterval, maxSpecialRoundInterval);
    }

    private void PerformRoundInterval()
    {
        if (specialRounds.Length <= 0) return;

        if (intervalCounter >= specialRoundInterval)
        {
            intervalCounter = 0;
            SetSpecialRoundInterval();

            int index = UnityEngine.Random.Range(0, specialRounds.Length);
            currentSpecialRound = specialRounds[index];
            Debug.Log("Special round loaded: " + currentSpecialRound.RoundName);
        }
        else
        {
            currentSpecialRound = null;
            intervalCounter++;
        }
    }

    public override IEnumerator StartNextRound(SpecialRound specialRound = null)
    {
        _roundActive = true;

        //Loads in a special round if assigned. If not, check interval for special round
        if(specialRound != null) currentSpecialRound = specialRound;
        else PerformRoundInterval();

        //Applies special round
        if (currentSpecialRound != null)
        {
            currentSpecialRound.StartSpecialRound();

            for (int i = 0; i < _spawners.Count; i++)
            {
                currentSpecialRound.ApplySpecialRound(_spawners[i]);
            }
        }

        _roundNum++;
        OnRoundStart?.Invoke();

        yield return new WaitForSeconds(_roundDuration);

        OnRoundComplete?.Invoke();

        for (int i = 0; i < _spawners.Count; i++)
        {
            //Remove all special round changes
            if (currentSpecialRound != null) currentSpecialRound.RemoveSpecialRound(_spawners[i]);

            //Increase default spawn rate at end of round
            RoundSpawnerData data = FindSpawnerData(_spawners[i].MonsterType);
            if (data == null) continue;

            //Spawn rate is added to based on how many rounds and the player multiplier value
            float spawnRateIncrease = data.SpawnRateIncrease * _playerMultiplierValue * _roundNum;
            _spawners[i].SetSpawnRate(data.StartingSpawnRate + spawnRateIncrease);
            _spawners[i].IsActive = true;
        }

        if (currentSpecialRound != null) currentSpecialRound.EndSpecialRound();

        _roundActive = false;
        _roundDuration += _roundDurationIncr;
    }
}



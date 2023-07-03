using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SpecialRoundSettings;

[CreateAssetMenu(fileName = "NewRoundSystem", menuName = "Data/RoundSystem/Endless")]
public class EndlessRoundSystem : RoundSystem
{
    public override IEnumerator StartNextRound(SpecialRound specialRound = null)
    {
        _roundActive = true;

        yield return new WaitForSeconds(_roundDuration);

        for (int i = 0; i < _spawners.Count; i++)
        {
            RoundSpawnerData data = FindSpawnerData(_spawners[i].MonsterType);
            if (data == null) continue;

            float increaseSpawnRate = data.SpawnRateIncrease * _playerMultiplierValue * _roundNum;
            _spawners[i].SetSpawnRate(data.StartingSpawnRate + increaseSpawnRate);
        }

        _roundNum++;
        _roundDuration += _roundDurationIncr;
        yield return StartNextRound();
    }
}

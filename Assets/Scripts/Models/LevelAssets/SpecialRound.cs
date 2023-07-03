using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

namespace SpecialRoundSettings
{
    public enum SpecialRoundBonusEnum { Add, Set, Disable };

    [CreateAssetMenu(fileName = "NewSpecialRound", menuName = "Data/SpecialRound")]
    public class SpecialRound : ScriptableObject
    {
        public string RoundName => roundName;
        [SerializeField] private string roundName;

        [SerializeField] private SpecialRoundBonus[] settings;

        public void StartSpecialRound()
        {

        }

        public void ApplySpecialRound(MonsterSpawner spawner)
        {
            for(int i = 0; i < settings.Length; i++)
            {
                if(settings[i].MonsterType == spawner.MonsterType)
                {
                    settings[i].ApplyBonus(spawner);
                }
            }
        }

        public void RemoveSpecialRound(MonsterSpawner spawner)
        {
            for (int i = 0; i < settings.Length; i++)
            {
                if (settings[i].MonsterType == spawner.MonsterType)
                {
                    settings[i].RemoveBonus(spawner);
                }
            }
        }

        public void EndSpecialRound()
        {

        }
    }

    [Serializable]
    public class SpecialRoundBonus
    {
        public MonsterType MonsterType => monsterType;
        [SerializeField] private MonsterType monsterType;

        public SpecialRoundBonusEnum BonusType => bonusType;
        [SerializeField] private SpecialRoundBonusEnum bonusType;

        [SerializeField] private float bonusSpawnRate;
        private float previousSpawnRate;

        public void ApplyBonus(MonsterSpawner spawner)
        {
            previousSpawnRate = spawner.SpawnRate;

            switch(BonusType)
            {
                case SpecialRoundBonusEnum.Add:
                    spawner.SetSpawnRate(previousSpawnRate + bonusSpawnRate);
                    break;
                case SpecialRoundBonusEnum.Set:
                    spawner.SetSpawnRate(bonusSpawnRate);
                    break;
                case SpecialRoundBonusEnum.Disable:
                    spawner.IsActive = false;
                    break;
            }
        }

        public void RemoveBonus(MonsterSpawner spawner)
        {
            float tempSpawnRate = spawner.SpawnRate;

            switch (BonusType)
            {
                case SpecialRoundBonusEnum.Add:
                    spawner.SetSpawnRate(spawner.SpawnRate - bonusSpawnRate);
                    break;
                case SpecialRoundBonusEnum.Set:
                    spawner.SetSpawnRate(previousSpawnRate);
                    break;
                case SpecialRoundBonusEnum.Disable:
                    spawner.IsActive = true;
                    break;
            }

            previousSpawnRate = tempSpawnRate;
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StrengthenFlashlight : RoundCard
{
    [Space]
    [SerializeField] private float flashlightStrengthIncrease = 3f;

    protected override void ActivateCard()
    {
        MonsterBehaviorController.Instance.ApplyFlashScaleChange(flashlightStrengthIncrease, LevelManager.Instance.RoundDuration);
        LevelManager.Instance.ManualStartNextRound();
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlowGhosts : RoundCard
{
    [Space]
    [SerializeField] private GameObject cardContents; 
    [SerializeField] private float fractionOfSpeed = 2/3;

    protected override void ActivateCard()
    {
        MonsterBehaviorController.Instance.ApplyMovementScaleChange(fractionOfSpeed, LevelManager.Instance.RoundDuration);
        LevelManager.Instance.ManualStartNextRound();
    }
}

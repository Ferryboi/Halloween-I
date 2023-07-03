using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class CollectBoneTrigger : CollectTrigger
{
    [SerializeField] private int addedScore;

    protected override void OnCollect(Player player)
    {
        ScoreManager.Instance.AddScore(addedScore);
    }
}

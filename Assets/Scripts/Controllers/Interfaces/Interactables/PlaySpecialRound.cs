using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SpecialRoundSettings;

public class PlaySpecialRound : RoundCard
{
    [SerializeField] private SpecialRound newNextRound;

    protected override void Awake()
    {
        base.Awake();
        //Undo cost change, then increase money earned based on num of purchases
        _cost -= (_numOfPurchases * 2);
    }

    protected override void ActivateCard()
    {
        LevelManager.Instance.ManualStartNextRound(newNextRound);
    }
}

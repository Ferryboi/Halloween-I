using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewRoundCardContainer", menuName = "Data/RoundCardContainer")]
public class RoundCardContainer : ScriptableObject
{
    [SerializeField] private GameObject[] roundCards;

    public GameObject GetRoundCard()
    {
        int index = Random.Range(0, roundCards.Length);
        return roundCards[index];
    }
}

using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class CardInfoDisplay : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI title;
    [SerializeField] private TextMeshProUGUI cost;

    private void Awake()
    {
        RoundCard card = GetComponentInParent<RoundCard>();
        if(card)
        {
            DisplayName(card.Description);
            DisplayCost(card.Cost);
        }
    }

    private void DisplayName(string name)
    {
        title.text = name;
    }

    private void DisplayCost(int cost)
    {
        if (cost > 0)
        {
            this.cost.text = "Costs " + cost + " bones";
        }
        else if (cost < 0)
        {
            this.cost.text = "Earn " + (cost * -1) + " bones";
        }
        else
        {
            this.cost.text = "FREE";
        }
    }
}

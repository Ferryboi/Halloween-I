using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NextRound : MonoBehaviour, IInteractable
{
    public Interaction GetInteraction()
    {
        return null;
    }

    public void OnInteract()
    {
        LevelManager.Instance.ManualStartNextRound();
    }
}

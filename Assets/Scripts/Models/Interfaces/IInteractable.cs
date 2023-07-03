using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IInteractable
{
    public Interaction GetInteraction();

    public void OnInteract();
}

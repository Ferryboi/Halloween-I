using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractionTrigger : MonoBehaviour
{
    [SerializeField] private AudioSource sfx;

    private void OnTriggerEnter(Collider other)
    {
        IInteractable[] oInteractables = other.GetComponentsInParent<IInteractable>();

        for (int i = 0; i < oInteractables.Length; i++)
        {
            oInteractables[i].OnInteract();
            if (sfx && !sfx.isPlaying) sfx.Play();
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectLightningPowerup : MonoBehaviour, IInteractable
{
    public Interaction GetInteraction()
    {
        return null;
    }

    public void OnInteract()
    {
        CreateLightningAt(transform.position);
        RemoveAllGhosts();
        Destroy(gameObject);
    }

    private void CreateLightningAt(Vector3 pos)
    {
        LightningVFX lightningVFX = FindObjectOfType<LightningVFX>();
        if (lightningVFX) lightningVFX.PerformUILightning(pos);
    }

    private void RemoveAllGhosts()
    {
        Ghost[] ghosts = FindObjectsOfType<Ghost>();
        for (int i = 0; i < ghosts.Length; i++)
        {
            ghosts[i].KillEntity();
        }
    }
}

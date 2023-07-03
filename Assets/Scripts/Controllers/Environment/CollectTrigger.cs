using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class CollectTrigger : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        Player player = other.GetComponentInParent<Player>();

        if(player != null)
        {
            OnCollect(player);
            Destroy(gameObject); //THIS WILL NOT WORK IF CLASS IS NOT IN PARENT. NEED TO CHANGE
        }
    }

    protected abstract void OnCollect(Player player);
}

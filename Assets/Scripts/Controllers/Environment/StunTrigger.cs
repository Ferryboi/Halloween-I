using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StunTrigger : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        IStunnable[] oStunnables = other.GetComponentsInParent<IStunnable>();

        for(int i = 0; i < oStunnables.Length; i++)
        {
            oStunnables[i].OnStun();
        }
    }
}

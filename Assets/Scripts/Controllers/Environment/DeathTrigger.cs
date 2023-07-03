using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathTrigger : MonoBehaviour
{
    [SerializeField] private AudioSource sfx;

    private void OnTriggerEnter(Collider other)
    {
        IAttackable[] oDamageables = other.GetComponentsInParent<IAttackable>();

        for (int i = 0; i < oDamageables.Length; i++)
        {
            oDamageables[i].OnAttacked();
            if (sfx && !sfx.isPlaying) sfx.Play();
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageTrigger : MonoBehaviour
{
    [SerializeField] private int damage = 1;

    private void OnTriggerEnter(Collider other)
    {
        IAttackable[] oDamageables = other.GetComponentsInParent<IAttackable>();

        for (int i = 0; i < oDamageables.Length; i++)
        {
            oDamageables[i].OnAttacked(damage);
        }
    }
}

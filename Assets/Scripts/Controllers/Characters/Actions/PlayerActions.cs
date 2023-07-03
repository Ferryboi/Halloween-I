using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerActions : MonoBehaviour
{
    [SerializeField] private float interactCooldown;
    private bool onCooldown = false;

    [Space]
    [SerializeField] private Animator animator;
    [SerializeField] private string attackAnim;

    public void OnInteract(InputValue value)
    {
        if (onCooldown) return;

        StartCoroutine(Cooldown());
        PerformAttack();
    }

    private IEnumerator Cooldown()
    {
        onCooldown = true;

        yield return new WaitForSeconds(interactCooldown);

        onCooldown = false;
    }

    private void PerformAttack()
    {
        animator.Play(attackAnim);
    }
}

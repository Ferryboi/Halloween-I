using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerStunned : MonoBehaviour, IStunnable
{
    [SerializeField] private Movement movement;
    [SerializeField] private GameObject stunEffect;

    private bool isStunned = false;
    private Vector2 prevDirection;
    private float prevSpeed;

    private const int NUM_OF_WIGGLES = 20;
    private int wigglesRemaining;

    private const float STUN_LOCK_DURATION = 1f;
    private bool stunLocked;

    private void Awake()
    {
        stunEffect.SetActive(false);
    }

    public void OnStun()
    {
        if (isStunned && stunLocked) return;

        isStunned = true;
        stunLocked = true;
        prevDirection = Vector2.zero;
        wigglesRemaining = NUM_OF_WIGGLES;

        prevSpeed = movement.GetSpeed();
        movement.SetSpeed(0.1f);

        stunEffect.SetActive(true);
    }

    public void OnStunStopped()
    {
        if (!isStunned) return;

        movement.SetSpeed(prevSpeed);
        isStunned = false;

        stunEffect.SetActive(false);
    }

    private IEnumerator StunLock()
    {
        stunLocked = true;
        yield return new WaitForSeconds(STUN_LOCK_DURATION);
        stunLocked = false;
    }

    public void OnMove(InputValue value)
    {
        if (!isStunned) return;

        Vector2 direction = value.Get<Vector2>();
        if(direction != prevDirection)
        {
            wigglesRemaining--;
            if(wigglesRemaining == 0)
            {
                OnStunStopped();
                StartCoroutine(StunLock());
            }
        }
    }
}

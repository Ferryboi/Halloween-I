using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementAnimator : MonoBehaviour
{
    [SerializeField] private Movement movement;
    [SerializeField] private float runThreshold = 4f;
    
    [Space]
    [SerializeField] private Animator animator;
    [SerializeField] private string idleAnim;
    [SerializeField] private string walkAnim;
    [SerializeField] private string runAnim;

    private bool isRunning;
    private bool isWalking;

    private void Awake()
    {
        animator.enabled = true;
        isRunning = false;
        isWalking = false;
    }

    private void Update()
    {
        if (movement.IsMoving)
        {
            //Is runnning
            if (!isRunning && movement.GetSpeed() > runThreshold)
            {
                animator.SetBool("isRunning", true);
                isRunning = true;
            }
            //Is walking
            else if (!isWalking)
            {
                animator.SetBool("isWalking", true);
                isWalking = true;
            }
        }
        //Is idle
        else
        {
            if(isWalking)
            {
                animator.SetBool("isWalking", false);
                isWalking = false;
            }
            if(isRunning)
            {
                animator.SetBool("isRunning", false);
                isRunning = false;
            }
        }
    }
}

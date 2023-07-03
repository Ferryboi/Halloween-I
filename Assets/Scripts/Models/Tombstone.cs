using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tombstone : MonoBehaviour
{
    [SerializeField] private Animator animator;
    [SerializeField] private string rebuildAnim = "Rebuild";
    [SerializeField] private string destroyAnim = "Destroy";

    public Vector3 SpawnPos => transform.position + spawnOffset;
    [SerializeField] private Vector3 spawnOffset;

    public bool Destroyed { get; private set; }
    public ZombieArm AssociatedZombie { get; private set; }

    public void DestroyTombstone()
    {
        if (Destroyed == true) return;

        Destroyed = true;
        animator.Play(destroyAnim);
    }

    public void RebuildTombstone()
    {
        if (Destroyed == false) return;

        Destroyed = false;
        animator.Play(rebuildAnim);
    }

    public void AddZombie(ZombieArm zombie)
    {
        AssociatedZombie = zombie;
    }

    public void RemoveZombie()
    {
        if(AssociatedZombie != null)
        {
            AssociatedZombie.KillEntity();
            AssociatedZombie = null;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        Destroyed = false;
        AssociatedZombie = null;
    }
}

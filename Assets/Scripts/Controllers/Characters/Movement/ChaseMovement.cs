using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChaseMovement : BasicMovement
{
    private PlayerManager pManager;
    private Transform playerTransform;

    private const float ROUND_SCALE = 0.3f;

    private void Awake()
    {
        pManager = PlayerManager.Instance;
        FindPlayer();

        SetSpeed(GetSpeed() + (LevelManager.Instance.RoundNum * ROUND_SCALE));
    }

    private void Update()
    {
        if(playerTransform)
        {
            Vector3 difference = playerTransform.position - transform.position;

            SetDirection(new Vector3(difference.x, 0, difference.z));

            Move();
        }
        else
        {
            IsMoving = false;
            FindPlayer();
        }
    }

    private void FindPlayer()
    {
        Player player = pManager.GetRandomPlayer();
        if (player != null) playerTransform = player.transform;
    }
}

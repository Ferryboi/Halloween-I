using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BounceAroundMovement : BasicMovement
{
    private Vector3 targetPos = Vector3.zero;

    private float width;
    private float height;

    [Space]
    [SerializeField] private float targetRadius = 1.5f;
    [Space]
    [SerializeField] private bool finiteTargets = false;
    [SerializeField] private int numOfTargets = 5;
    private int targetsReached = 0;

    private void Start()
    {
        width = LevelManager.Instance.LevelHalfWidth;
        height = LevelManager.Instance.LevelHalfHeight;

        FindTargetPos();
    }

    private void Update()
    {
        Move();
    }

    private void FindTargetPos()
    {
        targetPos = new Vector3(Random.Range(-width, width), transform.position.y, Random.Range(-height, height));
        SetDirection(targetPos - transform.position);
    }

    public override void Move(float scale = 1)
    {
        base.Move(scale);

        Vector3 difference = transform.position - targetPos;
        if(difference.sqrMagnitude <= targetRadius * targetRadius)
        {
            targetsReached++;

            if (finiteTargets && targetsReached >= numOfTargets)
            {
                EndBounceAround();
            }
            else
            {
                FindTargetPos();
            }
        }
    }

    private void EndBounceAround()
    {
        IsActive = false;
    }
}

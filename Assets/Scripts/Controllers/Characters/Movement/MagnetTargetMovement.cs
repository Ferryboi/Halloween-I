using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MagnetTargetMovement : Movement
{
    [Space]
    [SerializeField] private Transform target;
    [SerializeField] private float maxDistance;

    public override MovementType GetMovementType()
    {
        return MovementType.Walk;
    }

    public override void Move(float scale = 1)
    {
        if (!IsActive) return;
        _movement = Vector3.ClampMagnitude(_movement + _direction * _speed * scale * Time.deltaTime, _speed);
        transform.position += _movement;

        Vector3 difference = transform.position - target.position;
        if(difference.sqrMagnitude > maxDistance * maxDistance)
        {
            transform.position = target.position + (difference.normalized * maxDistance);
        }

        _lastValidPosition = transform.position;
    }

    private void Update()
    {
        SetDirection(target.position - transform.position);
        Move();
    }

    public void SetTarget(Transform target)
    {
        this.target = target;
    }

    public void SetMaxDistance(float maxDistance)
    {
        this.maxDistance = maxDistance;
    }
}

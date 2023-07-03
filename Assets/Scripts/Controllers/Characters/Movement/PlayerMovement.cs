using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : BasicMovement
{
    private void Update()
    {
        if(IsMoving)
        {
            Move();
        }
    }

    public void OnMove(InputValue value)
    {
        Vector3 direction = value.Get<Vector2>();
        direction.z = direction.y;
        direction.y = 0;

        if (direction.x == 0 && direction.z == 0)
        {
            IsMoving = false;
            return;
        }
        
        IsMoving = true;
        SetDirection(direction);
    }
}

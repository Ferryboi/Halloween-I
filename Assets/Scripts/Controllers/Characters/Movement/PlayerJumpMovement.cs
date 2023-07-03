using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerJumpMovement : JumpMovement
{
    public bool IsHoldingJump => isHoldingJump;
    private bool isHoldingJump;

    public void OnJump(InputValue value)
    {
        if(IsActive) Move();
    }
}

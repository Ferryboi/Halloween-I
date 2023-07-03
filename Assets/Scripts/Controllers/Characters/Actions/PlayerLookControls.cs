using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerLookControls : MonoBehaviour
{
    LayerMask groundLayer;
    Camera mainCamera;

    [HideInInspector] public bool IsActive = true;

    private void Awake()
    {
        mainCamera = Camera.main;
        groundLayer = LayerMask.GetMask(new string[] { "Ground" });
    }

    public void OnLook(InputValue value)
    {
        if (!IsActive || Time.timeScale == 0) return;

        Vector3 inputVal = value.Get<Vector2>();

        if (inputVal.sqrMagnitude != 0)
        {
            transform.LookAt(transform.position + new Vector3(inputVal.x, 0, inputVal.y));
        }
    }

    public void OnLookMouse(InputValue value)
    {
        if (!IsActive || Time.timeScale == 0) return;

        Vector3 inputVal = value.Get<Vector2>();

        Ray ray = mainCamera.ScreenPointToRay(inputVal);
        if(Physics.Raycast(ray, out RaycastHit hit, int.MaxValue, groundLayer))
        {
            transform.LookAt(new Vector3(hit.point.x, transform.position.y, hit.point.z));
        }
    }

    public void OnLookController(InputValue value)
    {
        if (!IsActive || Time.timeScale == 0) return;

        Vector3 inputVal = value.Get<Vector2>();

        if (inputVal.sqrMagnitude != 0)
        {
            transform.LookAt(transform.position + new Vector3(inputVal.x, 0, inputVal.y));
        }
    }
}

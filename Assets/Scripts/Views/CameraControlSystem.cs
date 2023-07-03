using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class CameraControlSystem : MonoBehaviour
{
    [SerializeField] private List<CameraControl> cameraPivots;
    [SerializeField] private CameraPivots defaultPivot;

    private Transform mainCamera;

    private void Awake()
    {
        mainCamera = Camera.main.transform;
        SetCameraToControl(defaultPivot);
    }

    private CameraControl FindControlOfType(CameraPivots pivot)
    {
        for(int i = 0; i < cameraPivots.Count; i++)
        {
            if (cameraPivots[i].Pivot == pivot) return cameraPivots[i];
        }

        return null;
    }

    public void SetCameraToControl(CameraPivots pivot)
    {
        CameraControl control = FindControlOfType(pivot);
        if (control == null) return;
        StopAllCoroutines();

        mainCamera.position = control.Transform.position;
        mainCamera.rotation = control.Transform.rotation;
    }

    public void PivotCameraToControl(CameraPivots pivot)
    {
        CameraControl control = FindControlOfType(pivot);
        if (control == null) return;

        StopAllCoroutines();
        StartCoroutine(control.PivotToPosition(mainCamera));
    }
}

[Serializable]
public class CameraControl
{
    public CameraPivots Pivot => pivot;
    [SerializeField] private CameraPivots pivot;

    public Transform Transform => transform;
    [SerializeField] private Transform transform;

    public float Duration => duration;
    [SerializeField] private float duration;

    public IEnumerator PivotToPosition(Transform camera)
    {
        Vector3 startPos = camera.position;
        Quaternion startRot = camera.rotation;

        for (float time = 0; time < Duration; time += Time.deltaTime)
        {
            camera.position = Vector3.Lerp(startPos, transform.position, time / Duration);
            camera.rotation = Quaternion.Lerp(startRot, transform.rotation, time / Duration);
            yield return 0;
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardHoverEffect : MonoBehaviour
{
    private Vector3 startPos;
    private const float FLOAT_FRQUENCY = 0.25f;
    private const float FLOAT_ALTITUDE = 0.1f;
    private float startingDegree;

    private void Awake()
    {
        startingDegree = Random.Range(0, 360);
        startPos = transform.localPosition;
    }

    private void Update()
    {
        transform.localPosition = startPos + new Vector3(0, Mathf.Sin(startingDegree + (Time.fixedTime * Mathf.PI * FLOAT_FRQUENCY)) * FLOAT_ALTITUDE);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotatingModel : MonoBehaviour
{
    [SerializeField] private Transform model;
    [SerializeField] private float speed = 1f;

    private void Awake()
    {
        model.Rotate(0, Random.Range(0, 360), 0);
    }

    // Update is called once per frame
    void Update()
    {
        model.Rotate(0, speed * Time.deltaTime, 0);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomModel : MonoBehaviour
{
    [SerializeField] private GameObject[] models;

    private void Awake()
    {
        int chosenIndex = Random.Range(0, models.Length);
        for(int i = 0; i < models.Length; i++)
        {
            if (i == chosenIndex) models[i].SetActive(true);
            else models[i].SetActive(false); 
        }
    }
}

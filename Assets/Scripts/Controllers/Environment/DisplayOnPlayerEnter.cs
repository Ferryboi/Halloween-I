using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisplayOnPlayerEnter : MonoBehaviour
{
    [SerializeField] private GameObject display;
    private int numOfPlayers = 0;

    private void Awake()
    {
        display.SetActive(false);
    }

    private void OnDisable()
    {
        numOfPlayers = 0;
        ToggleDisplay();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponentInParent<Player>())
        {
            numOfPlayers++;
            ToggleDisplay();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.GetComponentInParent<Player>())
        {
            numOfPlayers--;
            if (numOfPlayers < 0) numOfPlayers = 0;
            ToggleDisplay();
        }
    }

    private void ToggleDisplay()
    {
        if(numOfPlayers > 0)
        {
            display.SetActive(true);
        }
        else
        {
            display.SetActive(false);
        }
    }
}

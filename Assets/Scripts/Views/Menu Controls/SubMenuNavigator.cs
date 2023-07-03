using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SubMenuNavigator : MonoBehaviour
{
    [SerializeField] private Button startingButton;

    public GameObject GetStartingButton()
    {
        if (startingButton == null) return null;

        return startingButton.gameObject;
    }
}

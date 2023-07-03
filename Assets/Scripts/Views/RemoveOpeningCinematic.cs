using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RemoveOpeningCinematic : MonoBehaviour
{
    [SerializeField] private MenuNavigator menuNav;
    [SerializeField] private SubMenuNavigator newOpeningScreen;

    private void OnDisable()
    {
        menuNav.SetStartingMenu(newOpeningScreen);
        Destroy(gameObject);
    }
}

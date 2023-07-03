using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class MenuNavigator : MonoBehaviour
{
    [SerializeField] private EventSystem eventSystem;
    [SerializeField] private SubMenuNavigator startingMenu;
    private SubMenuNavigator currentMenu = null;

    private void OnEnable()
    {
        if (currentMenu != null) currentMenu.gameObject.SetActive(false);

        if (startingMenu == null) return;
        startingMenu.gameObject.SetActive(true);
        currentMenu = startingMenu;

        if (startingMenu.GetStartingButton() != null) eventSystem.SetSelectedGameObject(startingMenu.GetStartingButton());
    }

    public void Navigate(SubMenuNavigator newMenu)
    {
        currentMenu.gameObject.SetActive(false);
        newMenu.gameObject.SetActive(true);
        currentMenu = newMenu;

        if (newMenu.GetStartingButton() != null) eventSystem.SetSelectedGameObject(newMenu.GetStartingButton());
    }

    public void SetStartingMenu(SubMenuNavigator newStartingMenu)
    {
        startingMenu = newStartingMenu;
    }
}

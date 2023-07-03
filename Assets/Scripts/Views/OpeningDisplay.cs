using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class OpeningDisplay : MonoBehaviour
{
    [SerializeField] private MenuNavigator menuNav;
    [SerializeField] private SubMenuNavigator newStartingScreen;
    [Space]
    [SerializeField] private CameraControlSystem cControl;
    [SerializeField] private float duration;

    private void Start()
    {
        cControl.PivotCameraToControl(CameraPivots.OpeningEnd);
        StartCoroutine(SwitchToMenu());
    }

    private IEnumerator SwitchToMenu()
    {
        yield return new WaitForSeconds(duration);

        EndOpening();
    }

    public void EndOpening()
    {
        cControl.SetCameraToControl(CameraPivots.MainMenu);
        menuNav.Navigate(newStartingScreen);
        menuNav.SetStartingMenu(newStartingScreen);
        UIManager.Instance.LightningVFX.PerformWorldLightning();
        Destroy(gameObject);
    }
}

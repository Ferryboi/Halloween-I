using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightningVFX : MonoBehaviour
{
    [SerializeField] private AudioSource sfx;

    [Space]
    [SerializeField] private GameObject worldFlash;
    [SerializeField] private GameObject worldStrike;
    [SerializeField] private GameObject uiFlash;
    [SerializeField] private GameObject uiStrike;

    [Space]
    [SerializeField] private Light directionalLighting;
    [SerializeField] private Color flashColor;

    [Space]
    [SerializeField] private bool worldFlashOnLoop = false;
    [SerializeField] private bool worldStrikeOnLoop = true;
    [SerializeField] private bool uiFlashOnLoop = false;
    [SerializeField] private bool uiStrikeOnLoop = false;

    [Space]
    [SerializeField] private float minFlashIntermission = 15f;
    [SerializeField] private float maxFlashIntermission = 60f;

    [Space]
    [SerializeField] private float worldStrikeMinX;
    [SerializeField] private float worldStrikeMaxX;

    private bool isOn;
    private float defaultIntensity;
    private Color defaultColor;

    public void TurnOn()
    {
        if (isOn) return;

        isOn = true;
        worldFlash.SetActive(false);
        worldStrike.SetActive(false);
        uiFlash.SetActive(false);
        uiStrike.SetActive(false);

        defaultIntensity = directionalLighting.intensity;
        defaultColor = directionalLighting.color;

        StartCoroutine(FlashLoop());
    }

    public void TurnOff()
    {
        if (!isOn) return;

        isOn = false;
        StopAllCoroutines();
        worldFlash.SetActive(false);
        worldStrike.SetActive(false);
    }

    private IEnumerator FlashLoop()
    {
        while(true)
        {
            float intermissionTime = Random.Range(minFlashIntermission, maxFlashIntermission);

            yield return new WaitForSeconds(intermissionTime);

            if (LevelManager.Instance.LevelActive) StartCoroutine(PerformLightning(uiFlashOnLoop ? uiFlash : null, uiStrikeOnLoop ? uiStrike : null));
            else StartCoroutine(PerformLightning(worldFlashOnLoop ? worldFlash : null, worldStrikeOnLoop ? worldStrike : null));

            MoveWorldLightning();

            yield return new WaitForSeconds(1);
        }
    }
    
    private IEnumerator PerformLightning(GameObject flash = null, GameObject strike = null)
    {
        if(strike)
        {
            strike.SetActive(true);
            yield return new WaitForSeconds(0.1f);
        }

        //Flash on
        if (flash) flash.SetActive(true);
        directionalLighting.intensity = 500;
        directionalLighting.color = flashColor;
        yield return new WaitForSeconds(0.125f);

        //Flash off
        if (flash) flash.SetActive(false);
        directionalLighting.intensity = defaultIntensity;
        directionalLighting.color = defaultColor;
        yield return new WaitForSeconds(0.05f);

        //Second flash
        if (flash) flash.SetActive(true);
        directionalLighting.intensity = 500;
        directionalLighting.color = flashColor;
        yield return new WaitForSeconds(0.125f);
        if (sfx) sfx.Play();

        //Flash off
        if (flash) flash.SetActive(false);
        directionalLighting.intensity = defaultIntensity;
        directionalLighting.color = defaultColor;

        if (strike)
        {
            strike.SetActive(false);
        }
    }

    public void PerformWorldLightning()
    {
        StartCoroutine(PerformLightning(worldFlash, worldStrike));
    }

    public void PerformUILightning(Vector3 worldPos)
    {
        Vector3 screenPos = Camera.main.WorldToScreenPoint(worldPos);
        uiStrike.transform.position = screenPos;

        StartCoroutine(PerformLightning(uiFlash, uiStrike));
    }

    private void OnDisable()
    {
        TurnOff();
    }

    private void OnEnable()
    {
        TurnOn();
    }

    private void MoveWorldLightning()
    {
        float xPos = Random.Range(worldStrikeMinX, worldStrikeMaxX);
        worldStrike.transform.position = new Vector3(xPos, worldStrike.transform.position.y, worldStrike.transform.position.z);
    }
}

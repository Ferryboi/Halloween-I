using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenuVFX : MonoBehaviour
{
    [SerializeField] private LightningVFX lightning;
    [SerializeField] private float maxXOffset;
    [SerializeField] private float yOffset;
    [SerializeField] private float zOffset;

    private void Awake()
    {
        StartCoroutine(ShiftLightning());
    }

    private IEnumerator ShiftLightning()
    {
        ChooseLightningPos();

        while(true)
        {
            float time = Random.Range(10f, 30f);
            yield return new WaitForSeconds(time);
            ChooseLightningPos();
        }
    }

    private void ChooseLightningPos()
    {
        float xOffset = Random.Range(-maxXOffset, maxXOffset);
        
        //lightning.PerformUILightning(new Vector3(xOffset, yOffset, zOffset), false);
    }
}

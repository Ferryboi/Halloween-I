using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Unearth : MonoBehaviour, IInteractable
{
    [SerializeField] private GameObject unearthedPrefab;
    [SerializeField] private float undergroundOffset;
    [Space]
    [SerializeField] private GameObject climbVFX;
    [SerializeField] private GameObject unearthVFX;
    [Space]
    [SerializeField] private float maxDelay;
    [SerializeField] private float minDelay;
    [SerializeField] private float duration;

    private Vector3 initialPos;
    private float offsetPos;

    private bool unearthing;

    private void Awake()
    {
        initialPos = transform.position;
        offsetPos = undergroundOffset;
        ResetPos();
    }

    private void ResetPos()
    {
        unearthing = false;
        transform.position = initialPos + (Vector3.down * -undergroundOffset);

        StopAllCoroutines();
        StartCoroutine(UnearthDelay());
    }

    private IEnumerator UnearthDelay()
    {
        //float waitTime = Random.Range(minDelay, maxDelay);

        //yield return new WaitForSeconds(waitTime);

        SpawnAtInitPos(climbVFX);
        //StartCoroutine(ClimbUp());
        yield return ClimbUp();
    }

    private IEnumerator ClimbUp()
    {
        unearthing = true;
        float heightPerFrame = -undergroundOffset / duration;
        while (offsetPos < 0)
        {
            float heightIncrease = heightPerFrame * Time.deltaTime;
            transform.position += Vector3.up * heightIncrease;
            offsetPos += heightIncrease;
            yield return 0;
        }

        transform.position = initialPos;
        yield return new WaitForSeconds(1f);
        CompleteUnearth();
    }

    private void CompleteUnearth()
    {
        unearthing = false;
        SpawnAtInitPos(unearthVFX);
        SpawnAtInitPos(unearthedPrefab);

        ZombieArm entity = GetComponentInParent<ZombieArm>();
        if (entity)
        {
            entity.SetUnearthed(true);
            entity.KillEntity();
        }
    }

    private void SpawnAtInitPos(GameObject prefab)
    {
        if (prefab) Instantiate(prefab, initialPos, transform.rotation);
    }

    public Interaction GetInteraction()
    {
        return null;
    }

    public void OnInteract()
    {
        if (unearthing == false) return;

        offsetPos -= (-undergroundOffset / 2);
        if (offsetPos < undergroundOffset)
        {
            ZombieArm entity = GetComponentInParent<ZombieArm>();
            if (entity)
            {
                entity.SetUnearthed(false);
                entity.KillEntity();
            }
        }
        else transform.position = initialPos - (Vector3.down * offsetPos);
    }
}

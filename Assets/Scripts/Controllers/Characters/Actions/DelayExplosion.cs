using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DelayExplosion : MonoBehaviour
{
    [SerializeField] private GameObject bombModel;
    [SerializeField] private GameObject explosionModel;
    [SerializeField] private GameObject chargingModel;
    [SerializeField] private GameObject debrisModel;
    [Space]
    [SerializeField] private float minDelay;
    [SerializeField] private float maxDelay;
    [Space]
    [SerializeField] private float explosionDuration;
    [SerializeField] private float debrisDuration;

    [Space]
    [SerializeField] private AudioSource startupSFX;
    [SerializeField] private AudioSource explosionChargeSFX;
    [SerializeField] private AudioSource explosionSFX;


    // Start is called before the first frame update
    void Awake()
    {
        bombModel.SetActive(true);
        explosionModel.SetActive(false);
        debrisModel.SetActive(false);
        if(chargingModel) chargingModel.SetActive(false);

        StartCoroutine(Explosion());
    }

    private IEnumerator Explosion()
    {
        if (startupSFX) startupSFX.Play();

        float delay = Random.Range(minDelay, maxDelay);
        yield return new WaitForSeconds(delay);

        if(explosionChargeSFX)
        {
            chargingModel.SetActive(true);
            explosionChargeSFX.Play();
            yield return new WaitForSeconds(explosionChargeSFX.clip.length);
            chargingModel.SetActive(false);
        }

        bombModel.SetActive(false);
        explosionModel.SetActive(true);
        debrisModel.SetActive(true);

        if (explosionSFX) explosionSFX.Play();
        yield return new WaitForSeconds(explosionDuration);

        explosionModel.SetActive(false);

        yield return new WaitForSeconds(debrisDuration);

        debrisModel.SetActive(false);

        Entity entity = GetComponentInParent<Entity>();
        if (entity != null) entity.KillEntity();
    }
}

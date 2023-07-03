using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DieFromFlash : MonoBehaviour, IFlashable
{
    [SerializeField] private Movement movement;
    [SerializeField] private float flashDuration = 1.5f;
    public static float FlashScale = 1f;
    [Space]
    [SerializeField] private AudioSource deathSFX;
    [SerializeField] private GameObject model;

    private float defaultSpeed;
    private float decreaseSpeed;
    private float currentSpeed;

    private const float DEATH_DELAY = 0.2f;
    private bool flashed = false;
    private bool dying = false;
    private bool dead = false;

    private float numOfFlashers = 0;

    private void Start()
    {
        if (flashDuration == 0) flashDuration = 1;

        defaultSpeed = movement.GetSpeed();
        currentSpeed = defaultSpeed;
        decreaseSpeed = defaultSpeed / flashDuration;
    }

    private void Update()
    {
        if (dead || dying) return;

        if(flashed)
        {
            DecreaseSpeed();
        }
        else if(currentSpeed < defaultSpeed)
        {
            IncreaseSpeed();
        }
    }

    private void DecreaseSpeed()
    {
        currentSpeed -= decreaseSpeed * FlashScale * Time.deltaTime;

        if (currentSpeed <= 0)
        {
            currentSpeed = 0;
            StartCoroutine(Death());
        }

        movement.SetSpeed(currentSpeed);
    }

    private void IncreaseSpeed()
    {
        currentSpeed += decreaseSpeed * Time.deltaTime;
        if (currentSpeed >= defaultSpeed) currentSpeed = defaultSpeed;

        movement.SetSpeed(currentSpeed);
    }

    public void OnFlashStart()
    {
        if (dead) return;

        flashed = true;
        numOfFlashers++;
    }

    public void OnFlashEnd()
    {
        if (dead) return;

        numOfFlashers--;
        if (numOfFlashers > 0) return;

        flashed = false;
        dying = false;
        StopAllCoroutines();
    }

    private IEnumerator Death()
    {
        dying = true;
        yield return new WaitForSeconds(DEATH_DELAY);

        dead = true;
        if (deathSFX)
        {
            deathSFX.Play();
            model.SetActive(false);

            while (deathSFX.isPlaying)
            {
                yield return 0;
            }
        }

        Entity entity = GetComponentInParent<Entity>();
        if (entity) entity.KillEntity();
    }
}

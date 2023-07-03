using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterBehaviorController : MonoBehaviour
{
    public static MonsterBehaviorController Instance => instance;
    private static MonsterBehaviorController instance = null;

    public ZombieSpawner ZombieSpawner => zombieSpawner;
    [SerializeField] private ZombieSpawner zombieSpawner;

    private void Awake()
    {
        if (Instance == null) instance = this;
    }

    public void ApplyMovementScaleChange(float newScale, float duration)
    {
        StartCoroutine(MovementScaleChange(newScale, duration));
    }

    private IEnumerator MovementScaleChange(float newScale, float duration)
    {
        float originalScale = ContainedForwardMovement.StaticScale;
        ContainedForwardMovement.StaticScale = newScale;

        yield return new WaitForSeconds(duration);

        ContainedForwardMovement.StaticScale = originalScale;
    }

    public void ApplyFlashScaleChange(float newScale, float duration)
    {
        StartCoroutine(FlashScaleChange(newScale, duration));
    }

    private IEnumerator FlashScaleChange(float newScale, float duration)
    {
        List<PlayerData> players = PlayerManager.Instance.GetAllActivePlayers();
        for(int i = 0; i < players.Count; i++)
        {
            if (players[i].GetPlayerInstance()) players[i].GetPlayerInstance().ToggleFlashlight(true);
        }

        float originalScale = DieFromFlash.FlashScale;
        DieFromFlash.FlashScale = newScale;

        yield return new WaitForSeconds(duration);

        DieFromFlash.FlashScale = originalScale;

        for (int i = 0; i < players.Count; i++)
        {
            if (players[i].GetPlayerInstance()) players[i].GetPlayerInstance().ToggleFlashlight(false);
        }
    }

    public void ApplyBlockZombie(float duration)
    {
        if (zombieSpawner) StartCoroutine(BlockZombie(duration));
        else Debug.LogWarning("No zombie spawner assigned to " + this);
    }

    private IEnumerator BlockZombie(float duration)
    {
        zombieSpawner.IsActive = false;

        yield return new WaitForSeconds(duration);

        zombieSpawner.IsActive = true;
    }
}

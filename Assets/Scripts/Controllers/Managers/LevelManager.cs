using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SpecialRoundSettings;

public class LevelManager : Singleton<LevelManager>
{
    public bool LevelActive { get; private set; }
    public bool RoundActive => currentRoundSystem != null && currentRoundSystem.RoundActive;

    public LevelType LevelType => levelType;

    private LevelType levelType = LevelType.None;

    public float GameplayTime => gameplayTime;
    private float gameplayTime = 0f;

    public int RoundNum => currentRoundSystem ? currentRoundSystem.RoundNum : 0;
    public float RoundDuration => currentRoundSystem ? currentRoundSystem.RoundDuration : 0;

    public delegate void OnStartLevelDelegate();
    public OnStartLevelDelegate OnStartLevel;

    public delegate void OnRoundChangeDelegate();
    public OnRoundChangeDelegate OnRoundChange;

    public delegate void OnRoundEndDelegate();
    public OnRoundEndDelegate OnRoundEnd;

    public float LevelWidth => levelWidth;
    public float LevelHalfWidth => levelWidth / 2f;
    [SerializeField] private float levelWidth = 10f;
    public float LevelHeight => levelHeight;
    public float LevelHalfHeight => levelHeight / 2f;
    [SerializeField] private float levelHeight = 10f;

    public MonsterSpawner[] MonsterSpawners => monsterSpawners;
    private MonsterSpawner[] monsterSpawners;

    [Space]
    [SerializeField] private GameObject nextRoundButton;

    [SerializeField] private RoundCardContainer roundCards;
    private List<RoundCard> spawnedCards;

    [SerializeField] private RoundSystem[] roundSystems;
    private RoundSystem currentRoundSystem;

    private void Start()
    {
        SetInstance(this);

        monsterSpawners = FindObjectsOfType<MonsterSpawner>();
        spawnedCards = new List<RoundCard>();
        LevelActive = false;
        nextRoundButton.SetActive(false);
    }

    private void Update()
    {
        if (LevelActive == true && RoundActive == true)
        {
            if (TombstoneManager.Instance.FindActiveTombstoneCount() == 0 && PlayerManager.Instance.GetActivePlayerCount() == 0)
            {
                EndLevel();
            }

            gameplayTime += Time.deltaTime;
        }
    }

    private void SetRoundSystem(RoundSystem system)
    {
        currentRoundSystem = system;
        currentRoundSystem.ResetRoundNum();
        currentRoundSystem.OnRoundStart += RoundStart;
        currentRoundSystem.OnRoundComplete += RoundEnd;
    }

    private void EndCurrentRoundSystem()
    {
        if (currentRoundSystem == null) return;
        currentRoundSystem.EndRoundSystem();

        currentRoundSystem.OnRoundStart -= RoundStart;
        currentRoundSystem.OnRoundComplete -= RoundEnd;
        currentRoundSystem = null;
    }

    public void RestartLevel()
    {
        if (LevelActive) return;
        StartLevel(levelType);
    }

    public void StartLevel(LevelType type)
    {
        currentRoundSystem = null;
        for (int i = 0; i < roundSystems.Length; i++)
        {
            if (roundSystems[i].Type == type)
            {
                SetRoundSystem(roundSystems[i]);
                levelType = type;
                break;
            }
        }
        if (currentRoundSystem == null)
        {
            LevelActive = false;
            return;
        }

        gameplayTime = 0f;

        ScoreManager.Instance.ResetScore();
        ClearAllEnemies();
    }

    public void BeginLevel()
    {
        //PlayerManager.Instance.SpawnPlayersBeginning();

        LevelActive = true;

        StartCoroutine(currentRoundSystem.StartRoundSystem(monsterSpawners));
        OnStartLevel?.Invoke();
    }

    public void ManualStartNextRound(SpecialRound specialRound = null)
    {
        if (RoundActive == true || LevelActive == false) return;

        StartCoroutine(currentRoundSystem.StartNextRound(specialRound));
    }

    private void RoundStart()
    {
        nextRoundButton.SetActive(false);
        ClearAllRoundCards();

        OnRoundChange?.Invoke();
    }

    private void RoundEnd()
    {
        //Spawn bonus cards
        List<Tombstone> activeTombstones = TombstoneManager.Instance.FindAllActiveTombstones();
        for (int i = 0; i < activeTombstones.Count; i++)
        {
            spawnedCards.Add(Instantiate(roundCards.GetRoundCard(), activeTombstones[i].SpawnPos, activeTombstones[i].transform.rotation).GetComponent<RoundCard>());
        }

        OnRoundEnd?.Invoke();
        nextRoundButton.SetActive(true);
        if(UIManager.Instance.LightningVFX) UIManager.Instance.LightningVFX.PerformUILightning(nextRoundButton.transform.position);
        ClearAllEnemies();
    }

    public void EndLevel()
    {
        StopAllCoroutines();
        LevelActive = false;
        nextRoundButton.SetActive(false);

        ClearAllCollectibles();
        ClearAllRoundCards();
        PlayerManager.Instance.StopSpawning();
        PlayerManager.Instance.RemoveAllPlayerData();
        UIManager.Instance.GameOver();
        EndCurrentRoundSystem();
    }

    public void ClearAllEnemies(MonsterType type = MonsterType.All)
    {
        Monster[] allMonsters = FindObjectsOfType<Monster>();
        for(int i = 0; i < allMonsters.Length; i++)
        {
            if (type == MonsterType.All || type == allMonsters[i].MonsterType) allMonsters[i].KillEntity();
        }
    }

    private void ClearAllCollectibles()
    {
        CollectTrigger[] collectibles = FindObjectsOfType<CollectTrigger>();
        for(int i = 0; i < collectibles.Length; i++)
        {
            Destroy(collectibles[i].gameObject);
        }
    }

    private void ClearAllRoundCards()
    {
        for (int i = 0; i < spawnedCards.Count; i++)
        {
            spawnedCards[i].RemoveCard();
        }
        spawnedCards = new List<RoundCard>();
    }
}

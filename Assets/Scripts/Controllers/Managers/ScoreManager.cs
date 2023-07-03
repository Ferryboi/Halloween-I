using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreManager : Singleton<ScoreManager>
{
    public int Score => score;
    private int score = 0;

    [SerializeField] private int startingScore = 0;

    public delegate void OnScoreChangeDelegate(int addedScore);
    public OnScoreChangeDelegate OnScoreChange;

    private void Awake()
    {
        SetInstance(this);
        ResetScore();
    }

    public void ResetScore()
    {
        score = startingScore;
        OnScoreChange?.Invoke(0);
    }

    public void AddScore(int addedScore)
    {
        score += addedScore;
        OnScoreChange?.Invoke(addedScore);
    }

    public void RemoveScore(int removedScore)
    {
        if (removedScore > score) removedScore = score;

        score -= removedScore;
        OnScoreChange?.Invoke(-removedScore);
    }
}

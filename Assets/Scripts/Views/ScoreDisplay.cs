using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ScoreDisplay : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI roundText;
    [SerializeField] private TextMeshProUGUI scoreText;

    private ScoreManager scoreManager;
    private LevelManager levelManager;

    private void Awake()
    {
        scoreManager = ScoreManager.Instance;
        scoreManager.OnScoreChange += ScoreChanged;
        ScoreChanged(scoreManager.Score);

        levelManager = LevelManager.Instance;
        levelManager.OnRoundChange += RoundChanged;
        RoundChanged();
    }

    private void OnDestroy()
    {
        scoreManager.OnScoreChange -= ScoreChanged;
        levelManager.OnRoundChange -= RoundChanged;
    }

    private void ScoreChanged(int addedScore)
    {
        scoreText.text = "Bones: " + scoreManager.Score;
    }

    private void RoundChanged()
    {
        if (levelManager.LevelType != LevelType.Round) roundText.text = "";
        else roundText.text = "Round: " + levelManager.RoundNum;
    }
}

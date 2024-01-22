using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Zenject;

public interface IScoreManager
{
    void AddScore(int points);
}

public interface IScoreUI
{
    void UpdateScoreText(int score);
}

public class ScoreManager : MonoBehaviour, IScoreManager
{
    public int Score => score;

    private int score = 50;

    private static ScoreManager instance;

    public static ScoreManager Instance => instance;

    private IScoreUI scoreUI;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void SetScoreUI(IScoreUI ui)
    {
        scoreUI = ui;
        UpdateScoreText();
        Debug.Log("SetScoreUI called");
    }

    public void AddScore(int points)
    {
        score += points;
        Debug.Log("Score added: " + points);
        UpdateScoreText();
    }

    private void UpdateScoreText()
    {
        if (scoreUI != null)
        {
            scoreUI.UpdateScoreText(score);
            Debug.Log("Score updated: " + score);
        }
    }
}


using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Zenject;
using System;

public interface IScoreManager
{
    int Score { get; }
	event Action<int> OnScoreChanged;
	void AddScore(int points);
}

public class ScoreManager : MonoBehaviour, IScoreManager
{
    public event Action<int> OnScoreChanged;
    public int Score => score;

    private int score = 50;

    private static ScoreManager instance;

    public static ScoreManager Instance => instance;

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

    public void AddScore(int points)
    {
        score += points;
        OnScoreChanged(score);
    }
}

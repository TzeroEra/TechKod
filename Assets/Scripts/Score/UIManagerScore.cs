using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Zenject;

public class UIManagerScore : MonoBehaviour, IScoreUI
{
    public TextMeshProUGUI scoreText;

    private void Start()
    {
        ScoreManager.Instance.SetScoreUI(this);
        Debug.Log("UIManagerScore Start called");
    }

    public void UpdateScoreText(int score)
    {
        if (scoreText != null)
        {
            scoreText.text = "Досвід: " + score.ToString();
            Debug.Log("Score updated: " + score);
        }
    }
}
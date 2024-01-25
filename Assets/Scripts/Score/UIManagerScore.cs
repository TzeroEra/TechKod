using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Zenject;

public class UIManagerScore : MonoBehaviour
{
    public TextMeshProUGUI scoreText;

    [Inject]
    private void Construct(IScoreManager scoreManager)
    {
        _scoreManager = scoreManager;

        UpdateScoreText(_scoreManager.Score);

        _scoreManager.OnScoreChanged += UpdateScoreText;
    }
	IScoreManager _scoreManager;

	private void OnDestroy()
	{
        _scoreManager.OnScoreChanged -= UpdateScoreText;
	}

	public void UpdateScoreText(int score)
    {
        if (scoreText != null)
        {
            scoreText.text = "Досвід: " + score.ToString();
        }
    }
}
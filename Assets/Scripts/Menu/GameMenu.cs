using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameMenu : MonoBehaviour
{
    public GameObject mainMenuPanel;
    public GameObject optionsPanel;

    public Button startButton;
    public Button optionsButton;
    public Button backButton;

    public void Start()
    {
        startButton.onClick.AddListener(StartGame);
        optionsButton.onClick.AddListener(ShowOptions);
        backButton.onClick.AddListener(BackToMainMenu);

        ShowMainMenu();
    }

    public void StartGame()
    {
        SceneManager.LoadScene("Test_Enemy");
    }

    public void ShowOptions()
    {
        mainMenuPanel.SetActive(false);
        optionsPanel.SetActive(true);
    }

    public void BackToMainMenu()
    {
        mainMenuPanel.SetActive(true);
        optionsPanel.SetActive(false);
    }

    public void ShowMainMenu()
    {
        mainMenuPanel.SetActive(true);
        optionsPanel.SetActive(false);
    }

    public void ExitGame()
    {
        Application.Quit();
    }
}
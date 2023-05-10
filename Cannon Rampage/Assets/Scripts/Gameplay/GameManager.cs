using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class GameManager : MonoBehaviour
{
    private bool isGameFinished = false;

    public GameObject gamePanel, winPanel, losePanel;

    public delegate void Game();
    public static event Game GameStarted;
    public static Game GameIsWon;
    public static Game GameIsLost;
    private void Awake()
    {
        isGameFinished = false;

        Application.targetFrameRate = -1;

        GameIsWon += LevelFinished;
        GameIsLost += LevelFailed;
    }


    private void OnDisable()
    {
        GameIsWon -= LevelFinished;
        GameIsLost -= LevelFailed;
    }

    private void Start()
    {
        StartCoroutine(StartTheGame());
    }

    IEnumerator StartTheGame()
    {
        yield return new WaitForSeconds(1f);
        GameStarted();
    }


    public void TimerUp()
    {
        GameIsLost();
    }

    public void LevelFailed()
    {
        if (isGameFinished == false)
        {
            gamePanel.SetActive(false);
            losePanel.SetActive(true);
            isGameFinished = true;
        }
    }

    public void LevelFinished()
    {
        if (isGameFinished == false)
        {
            gamePanel.SetActive(false);
            winPanel.SetActive(true);
            isGameFinished = true;
        }
    }
}

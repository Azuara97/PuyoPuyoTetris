using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MenusManager : MonoBehaviour
{
    //MainMenu
    GameObject mainMenuPanel;

    //InGame
    GameObject gamePanel;
    GameObject pausePanel;

    //GameOver
    GameObject gameOverPanel;
    Text scoreText;

    //Others
    MyGameManager gameManager;

    void Start()
    {
        mainMenuPanel = transform.GetChild(0).gameObject;
        gamePanel = transform.GetChild(1).gameObject;
        pausePanel = gamePanel.transform.GetChild(1).gameObject;
        gameOverPanel = transform.GetChild(2).gameObject;
        scoreText = gameOverPanel.transform.GetChild(1).GetComponent<Text>();
        gamePanel.SetActive(false);
        gameOverPanel.SetActive(false);
        gameManager = FindObjectOfType<MyGameManager>();
    }

    //----------ButtonsFunctions----------
    //-----MainMenu-----
    public void startGameButton()
    {
        mainMenuPanel.SetActive(false);
        gamePanel.SetActive(true);
        pausePanel.SetActive(false);
        gameManager.RestartGame();
    }
    //-----InGame-----
    public void pauseGameButton()
    {
        pausePanel.SetActive(true);
        gameManager.SetPauseGame(true);
    }
    public void exitPauseButton()
    {
        pausePanel.SetActive(false);
        gameManager.SetPauseGame(false);
    }
    public void returnMainMenu()
    {
        gamePanel.SetActive(false);
        mainMenuPanel.SetActive(true);
    }
    public void gameOver()
    {
        gamePanel.SetActive(false);
        gameOverPanel.SetActive(true);
        scoreText.text = gameManager.GetScore().ToString();
    }
    //-----GameOver-----
    public void playAgain()
    {
        gameOverPanel.SetActive(false);
        gamePanel.SetActive(true);
        pausePanel.SetActive(false);
        gameManager.SetPauseGame(true);
        gameManager.RestartGame();
    }
}

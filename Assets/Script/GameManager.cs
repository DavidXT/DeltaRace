using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    public TextMeshProUGUI text;
    public TextMeshProUGUI bestScoreText;
    public int currentScore;
    public GameObject gameOverPanel;
    public GameObject WinPanel;
    public int numberOfCoin;
    public TextMeshProUGUI coinText;

    private void Awake()
    {
        if (instance != null && instance != this)
            Destroy(this.gameObject);
        else
        {
            instance = this;
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        if(GameState.Instance != null)
        {
            if(GameState.Instance.currentGameMod == GameMod.FLAPPY)
            {
                UpdateScore();
            }
        }
        numberOfCoin = 0; 
}

    // Update is called once per frame
    void Update()
    {

    }

    public void UpdateScore()
    {
        text.SetText("Score : "+currentScore);
    }

    public void WinLevel()
    {
        WinPanel.SetActive(true);
        Time.timeScale = 0;
    }

    public void GameOverLevel()
    {
        gameOverPanel.SetActive(true);
        Time.timeScale = 0;
    }
    public void GameOver()
    {
        if (PlayerPrefs.GetInt("score") <= currentScore)
        {
            PlayerPrefs.SetInt("score", currentScore);
        }
        bestScoreText.SetText("HIGH SCORE\n " + PlayerPrefs.GetInt("score"));

        gameOverPanel.SetActive(true);
        Time.timeScale = 0;
    }

    public void RestartGame()
    {
        Scene scene = SceneManager.GetActiveScene();
        SceneManager.LoadScene(scene.name);
        Time.timeScale = 1;
    }

    public void ReturnMainMenu()
    {
        SceneManager.LoadScene("MainMenu");
        Time.timeScale = 1;
    }

    public void UpdateCoin()
    {
        coinText.SetText("x" + numberOfCoin);
    }
}

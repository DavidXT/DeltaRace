using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    public GameObject MenuLayout;
    public GameObject LevelLayout;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void StartGame()
    {
        if(GameState.Instance != null)
        {
            GameState.Instance.currentGameMod = GameMod.FLAPPY;
        }
        SceneManager.LoadScene("MainGame");
    }

    public void LeaveGame()
    {
        Application.Quit();
    }

    public void CheckLevel()
    {
        MenuLayout.SetActive(false);
        LevelLayout.SetActive(true);
    }

    public void LoadLevel(string levelNumber)
    {
        if (GameState.Instance != null)
        {
            GameState.Instance.currentGameMod = GameMod.LEVEL;
        }
        SceneManager.LoadScene("Scenes/Level" + levelNumber);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum GameMod {MAINMENU,FLAPPY,LEVEL}
public class GameState : MonoBehaviour
{
    public static GameState Instance;
    public GameMod currentGameMod;

    private void Awake()
    {
        if (Instance != null && Instance != this)
            Destroy(this.gameObject);
        else
        {
            Instance = this;
            DontDestroyOnLoad(this.gameObject);
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        currentGameMod = GameMod.MAINMENU;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

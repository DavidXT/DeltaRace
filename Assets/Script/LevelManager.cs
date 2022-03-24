using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    public static LevelManager Instance;

    public float TimeToDoLevel;
    public float numberOfCoinToCollect;

    public float globalPercentageForWin;

    public int lifePlayerInLevel;

    public float numberCoinsInLevel;

    private void Awake()
    {
        if(Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        Player.Instance.Life = lifePlayerInLevel;
        Debug.Log("NUMBER OF COINS IN LEVEL : " + numberCoinsInLevel);
    }
}

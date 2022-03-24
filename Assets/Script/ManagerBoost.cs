using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ManagerBoost : MonoBehaviour
{
    public static ManagerBoost Instance;

    [System.NonSerialized]
    public float numberCoinsInLevel = 0;

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
        
    }
}

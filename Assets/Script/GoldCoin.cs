using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoldCoin : MonoBehaviour
{
    public float IncreaseMaxSpeedBy;
    public float speedForTurn;

    private void Start()
    {
        //LevelManager.Instance.numberCoinsInLevel += 1;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            other.gameObject.GetComponentInParent<PlayerController>().increaseMaxSpeed(IncreaseMaxSpeedBy);
            if(GameManager.instance != null)
            {
                GameManager.instance.numberOfCoin += 1;
                GameManager.instance.UpdateCoin();
            }
            Destroy(gameObject);
        }
    }

    private void Update()
    {
        transform.Rotate(0, speedForTurn, 0);
    }
}

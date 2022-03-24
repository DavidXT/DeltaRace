using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinsToSpawn : MonoBehaviour
{
    public float IncreaseMaxSpeedBy;
    bool alreadyTake = false;

    public float speedForTurn;

    private void Start()
    {
        LevelManager.Instance.numberCoinsInLevel += 1;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            if (!alreadyTake)
            {
                alreadyTake = true;
                Debug.Log("collisiondetect COIN");
                other.gameObject.GetComponentInParent<Player>().increaseMaxSpeed(IncreaseMaxSpeedBy);
                other.gameObject.GetComponentInParent<Player>().numberofCoin += 1;
                UIManager.Instance.UploadTextCoins();
                Destroy(gameObject);
            }
        }
    }

    private void Update()
    {
        transform.Rotate(speedForTurn, 0, 0);
    }
}

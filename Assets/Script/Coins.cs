using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coins : MonoBehaviour
{
    public float IncreaseMaxSpeedBy;
    public bool makeARow;
    public float numberCoinInRow;
    public float distanceBetweenCoinZ;
    public float distanceBetweenCoinY;
    public float distanceBetweenCoinX;
    public GameObject coin;

    public float speedForTurn;


    private void Start()
    {
        //LevelManager.Instance.numberCoinsInLevel += 1;

        if (makeARow)
        {
            float posLastCoinZ = transform.position.z;
            float posLastCoinY = transform.position.y;
            float posLastCoinX = transform.position.x;

            for (int i = 0; i < numberCoinInRow; i++)
            {
                posLastCoinZ += distanceBetweenCoinZ;
                posLastCoinY += distanceBetweenCoinY;
                posLastCoinX += distanceBetweenCoinX;
                GameObject instance = Instantiate(coin);
                instance.transform.position = new Vector3(posLastCoinX, posLastCoinY, posLastCoinZ);
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            //other.gameObject.GetComponentInParent<Player>().increaseMaxSpeed(IncreaseMaxSpeedBy);
            //other.gameObject.GetComponentInParent<Player>().numberofCoin += 1;
            //UIManager.Instance.UploadTextCoins();
            Destroy(gameObject);
        }
    }

    private void Update()
    {
        transform.Rotate(speedForTurn, 0, 0);
    }
}

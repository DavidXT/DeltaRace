using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FinishLinePaul : MonoBehaviour
{
    bool alreadyTake = false;

    private void OnTriggerEnter(Collider other)
    {
        if (!alreadyTake)
        {
            if (other.gameObject.CompareTag("Player"))
            {
                alreadyTake = true;
                Debug.Log("ARRIVEE");
                UIManager.Instance.EndOfLevel();
                Destroy(gameObject);
            }
        }
    }
}

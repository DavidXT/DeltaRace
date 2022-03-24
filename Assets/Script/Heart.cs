using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Heart : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("collisiondetect HEART");
        if (other.gameObject.CompareTag("Player"))
        {
            other.gameObject.GetComponentInParent<Player>().winLife(1);
            Destroy(gameObject);
        }
    }
}

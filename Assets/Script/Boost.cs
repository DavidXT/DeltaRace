using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boost : MonoBehaviour
{
    public float dureeMontee;
    public float forceMontee;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            Debug.Log("BOOST");
            other.gameObject.GetComponentInParent<Player>().boostUP(dureeMontee, forceMontee);
            Destroy(gameObject);
        }
    }
}


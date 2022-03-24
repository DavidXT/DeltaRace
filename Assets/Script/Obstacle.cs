using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Obstacle : MonoBehaviour
{
    public GameObject explosionToSpawn;

    public int degatToPlayer;

    public float distancewithplayerBeforeDestroy;

    [Range(0,1)]
    public float alphaWhenPassed;
    bool alreadyPass = false;

    public Material transparentMaterial;

    Camera MainCam;

    private void Start()
    {
        MainCam = Camera.main;
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            Handheld.Vibrate();
            other.gameObject.GetComponentInParent<Player>().looseLife(degatToPlayer);
            Instantiate(explosionToSpawn, transform.position, Quaternion.identity);
            Destroy(gameObject);
        }
    }
    private void Update()
    {
        if (Player.Instance)
        {
            if (transform.position.z + distancewithplayerBeforeDestroy < Player.Instance.transform.position.z && !alreadyPass)
            {
                alreadyPass = true;
                GetComponent<MeshRenderer>().material = transparentMaterial;
                /*Color actualColor = GetComponent<MeshRenderer>().material.color;
                GetComponent<MeshRenderer>().material.color = new Color(actualColor.r, actualColor.g, actualColor.b, alphaWhenPassed);*/
            }

            if(transform.position.z < MainCam.transform.position.z + 1)
            {
                Destroy(gameObject);
            }
        } 
    }
}

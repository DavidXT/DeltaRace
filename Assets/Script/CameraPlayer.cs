using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraPlayer : MonoBehaviour
{
    [SerializeField]
    GameObject player;

    [SerializeField]
    float distanceWithplayer;

    float distanceHauteurPlayerCam;
    // Start is called before the first frame update
    void Start()
    {
        distanceHauteurPlayerCam = player.transform.position.y - transform.position.y;
    }

    // Update is called once per frame
    void LateUpdate()
    {
        if (player)
        {
            transform.position = new Vector3(transform.position.x, player.transform.position.y - distanceHauteurPlayerCam, player.transform.position.z - distanceWithplayer);
        }
    }


}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Alpha_Tower : MonoBehaviour
{
    public float distancewithplayerBeforeDestroy;
    [Range(0, 1)]
    public float alphaWhenPassed;
    bool alreadyPass = false;
    public Material transparentMaterial;
    Camera MainCam;

    private void Start()
    {
        MainCam = Camera.main;
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

            if (transform.position.z < MainCam.transform.position.z + 1)
            {
                Destroy(gameObject);
            }
        }
    }
}

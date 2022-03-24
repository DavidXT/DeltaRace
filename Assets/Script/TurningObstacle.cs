using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurningObstacle : MonoBehaviour
{
    public bool TurnInXaxis;
    public float speedX;

    public bool TurnInYaxis;
    public float speedY;

    public bool TurnInZaxis;
    public float speedZ;


    // Update is called once per frame
    void Update()
    {
        if (TurnInXaxis)
        {
            transform.Rotate(speedX * Time.deltaTime, 0, 0);
        }

        if (TurnInYaxis)
        {
            transform.Rotate(0, speedY * Time.deltaTime, 0);
        }

        if (TurnInZaxis)
        {
            transform.Rotate(0, 0, speedZ * Time.deltaTime);
        }
    }
}

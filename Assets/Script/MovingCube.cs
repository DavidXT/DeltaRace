using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingCube : MonoBehaviour
{
    public bool movingInXaxis;
    public float margeToMoveX;
    public float speedX;

    public bool movingInYaxis;
    public float margeToMoveY;
    public float speedY;

    public bool movingInZaxis;
    public float margeToMoveZ;
    public float speedZ;

    Vector3 initialPos;

    private void Start()
    {
        initialPos = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        if (movingInXaxis)
        {
            if (transform.position.x >= initialPos.x + margeToMoveX)
            {
                transform.position = new Vector3(initialPos.x + margeToMoveX, transform.position.y, transform.position.z);
                speedX = -speedX;
            }
            if (transform.position.x <= initialPos.x - margeToMoveX)
            {
                transform.position = new Vector3(initialPos.x - margeToMoveX, transform.position.y, transform.position.z);
                speedX = -speedX;
            }
            transform.position = new Vector3(transform.position.x + speedX * Time.deltaTime, transform.position.y, transform.position.z);
        }

        if (movingInYaxis)
        {
            if (transform.position.y >= initialPos.y + margeToMoveY)
            {
                transform.position = new Vector3(transform.position.x, initialPos.y + margeToMoveY, transform.position.z);
                speedY = -speedY;
            }
            if (transform.position.y <= initialPos.y - margeToMoveY)
            {
                transform.position = new Vector3(transform.position.x, initialPos.y - margeToMoveY, transform.position.z);
                speedY = -speedY;
            }
            transform.position = new Vector3(transform.position.x, transform.position.y + speedY * Time.deltaTime, transform.position.z);
        }

        if (movingInZaxis)
        {
            if (transform.position.z >= initialPos.z + margeToMoveZ)
            {
                transform.position = new Vector3(transform.position.x, transform.position.y, initialPos.z + margeToMoveZ) ;
                speedZ = -speedZ;
            }
            if (transform.position.z <= initialPos.z - margeToMoveZ)
            {
                transform.position = new Vector3(transform.position.x, transform.position.y, initialPos.z - margeToMoveZ);
                speedZ = -speedZ;
            }
            transform.position = new Vector3(transform.position.x, transform.position.y, transform.position.z + speedZ * Time.deltaTime);
        }
    }
}

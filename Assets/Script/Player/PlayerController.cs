using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public PlayerData m_playerData;

    public GameObject controlplayer;
    public GameObject controlCharacter;

    public float defaultSpeed;

    public GameObject deltaplane;

    bool isTouching = false;

    public float speedY;
    public float coefMult = 1.2f;

    // Start is called before the first frame update
    void Start()
    {
        m_playerData.maxSpeedY = defaultSpeed;
        m_playerData.maxSpeedDescenteY = defaultSpeed;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.touchCount > 0 && Input.touches[0].phase == TouchPhase.Began || Input.GetButton("Jump"))
        {
            isTouching = true;
        }

        if (Input.touchCount > 0 && Input.touches[0].phase == TouchPhase.Ended || Input.GetButtonUp("Jump"))
        {
            isTouching = false;
        }

        //dif entre nos angles max et min
        float difAngle = m_playerData.minAngle - m_playerData.maxAngle;


        //calcul du coef de vitesse en Y
        float difSpeedY = m_playerData.maxSpeedY - m_playerData.minSpeedY;
        float coefSpeedYAngle = m_playerData.stepAngle * difSpeedY / difAngle;
         
        if (isTouching) 
        {
            Quaternion target = Quaternion.Euler(20, 0, 0);
            controlCharacter.transform.rotation = target;
            if (controlplayer.transform.eulerAngles.x + (m_playerData.stepAngle * 2) < m_playerData.minAngle)
            {
                controlplayer.transform.Rotate(m_playerData.stepAngle * 1.5f, 0, 0);
            }
            //descente
            if (speedY - (coefSpeedYAngle * m_playerData.coefDeccelYDescendre) > -m_playerData.maxSpeedDescenteY)
            {
                speedY -= (coefSpeedYAngle * m_playerData.coefDeccelYDescendre);
            }
            transform.Translate(transform.up * speedY * Time.deltaTime, Space.World);
        }
        else
        {
            Quaternion target = Quaternion.Euler(-20, 0, 0);
            controlCharacter.transform.rotation = target;
            if (controlplayer.transform.eulerAngles.x > m_playerData.maxAngle)
            {
                controlplayer.transform.Rotate(-m_playerData.stepAngle * 1.5f, 0, 0);
            }
            //monter
            if (speedY + coefSpeedYAngle * m_playerData.coefAccelYMonter < m_playerData.maxSpeedY)
            {
                speedY += coefSpeedYAngle * m_playerData.coefAccelYMonter;
            }
            transform.Translate(transform.up * speedY * Time.deltaTime, Space.World);
        }
    }

    public void increaseMaxSpeed(float increase)
    {
        m_playerData.maxSpeedDescenteY += increase;
        m_playerData.maxSpeedY += increase;
    }
}

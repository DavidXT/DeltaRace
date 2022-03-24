using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public static Player Instance; //For singleton

    bool isTouching = false;

    public int Life;

    public float speed;
    public float speedY;
    // FAIRE DES GETTERS ET SETTER ET PASSEZ LA VARIABLE EN SERIALIZED FIELD
    //float initialSpeed;

    public float minAngleX;
    public float maxAngleX;
    float angleBetween;

    public float stepAngle;

    public float minSpeed;
    public float maxSpeed;

    //Variables mouvements en Y
    public float minSpeedY;
    public float maxSpeedY;
    public float maxSpeedDescenteY;

    //Variables mouvements en X
    Vector2 startPos;
    Vector2 direction;
    public float margeManoeuvreX;
    public float middleX;
    public float stepMovmentX;

    float coefMult = 1;

    public GameObject controlplayer;

    public GameObject deltaplane;

    public float coefAccelX;
    public float coefDeccelX;

    public float coefAccelYMonter;
    public float coefDeccelYDescendre;

    //Power of Boost Variables
    bool boostUp = false;
    float timeOfBoost;
    float powerOfBoost;

    public int numberofCoin = 0;

    public float TopMaxDistance;

    private void Awake() // Make a singleton, car un seul vrai joueur dans la partie. 
    {
        if(Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }
    private void Start()
    {
        Life = LevelManager.Instance.lifePlayerInLevel;
        UIManager.Instance.uploadHeartUI();
        angleBetween = maxAngleX + ((minAngleX - maxAngleX) / 2);
        numberofCoin = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.touchCount > 0 && Input.touches[0].phase == TouchPhase.Began)
        {
            Touch touch = Input.GetTouch(0);
            startPos = touch.position;

            Debug.Log("Tuch Screen");
            isTouching = true;
        }

        if (Input.touchCount > 0 && Input.touches[0].phase == TouchPhase.Moved)
        {
            Touch touch = Input.GetTouch(0);
            
            direction = (touch.position - startPos);

            if (direction.x < 0 && transform.position.x > middleX - margeManoeuvreX) //si notre doigt va vers la gauche
            {
                transform.Translate(transform.right * -stepMovmentX * Time.deltaTime, Space.World);
                if(speed < minSpeed + 10)
                {
                    isTouching = false;
                }
                else
                {
                    isTouching = true;
                }
            }

            if(direction.x > 0 && transform.position.x < middleX + margeManoeuvreX) //si notre doigt va vers la droite
            {
                transform.Translate(transform.right * stepMovmentX * Time.deltaTime, Space.World);
                if (speed < minSpeed + 10)
                {
                    isTouching = false;
                }
                else
                {
                    isTouching = true;
                }
            }

            if(direction.x < 80 && direction.x > -80)
            {
                isTouching = true;
            }
        }

        if (Input.touchCount > 0 && Input.touches[0].phase == TouchPhase.Ended) //si notre doigt arette de toucher l'écran
        {
            Debug.Log("Stop Touching Screen");
            isTouching = false;
        }

        if (isTouching) // si notre doigt reste appuyé sans bouger
        {
            coefMult = coefAccelX; // Coef d'acceleration en vitesse X
            HoldToDive();
        }
        else // si notre doigt n'est plus sur l'écran
        {
            if (controlplayer.transform.eulerAngles.x > maxAngleX)
            {
                coefMult = -coefDeccelX; // Coef de ralentissement en vitesse X
                controlplayer.transform.Rotate(-stepAngle * 1.5f, 0, 0);
            }
        }

        //dif entre nos angles max et min
        float difAngle = minAngleX - maxAngleX;

        //calcul du coef de vitesse en X
        float difSpeed = maxSpeed - minSpeed;
        float coefSpeedAngle = stepAngle * difSpeed / difAngle;

        coefSpeedAngle = coefSpeedAngle / 3;

        //calcul du coef de vitesse en Y
        float difSpeedY = maxSpeedY - minSpeedY;
        float coefSpeedYAngle = stepAngle * difSpeedY / difAngle;


        // CALCUL DE LA VITESSE EN X (avancer)
        if (speed + (coefSpeedAngle* coefMult) < maxSpeed && speed + (coefSpeedAngle * coefMult) > minSpeed)
        {
            speed += coefSpeedAngle * coefMult;   
        }
        transform.Translate(transform.forward * speed  * Time.deltaTime, Space.World);


        // CALCUL DE LA VITESSE EN Y (monter et descendre)
        if(controlplayer.transform.eulerAngles.x > angleBetween)
        {
            //descendre
            if(speedY - (coefSpeedYAngle * coefDeccelYDescendre) > -maxSpeedDescenteY)
            {
                speedY -= (coefSpeedYAngle * coefDeccelYDescendre); 
            }
            transform.Translate(transform.up * speedY * Time.deltaTime, Space.World);
        }
        else
        {
            //monter
            if (speedY + coefSpeedYAngle * coefAccelYMonter < maxSpeedY)
            {
                    speedY += coefSpeedYAngle * coefAccelYMonter;
            }
            // Pour que le joueur ne dépasse pas une certaine hauteur.
            if (transform.position.y < TopMaxDistance)
            {
                transform.Translate(transform.up * speedY * Time.deltaTime, Space.World);
            }
        }

        //Boost Power
        if (boostUp)
        {
            timeOfBoost -= Time.deltaTime;

            if (timeOfBoost <= 0)
            {
                boostUp = false;
                powerOfBoost = 0;
                timeOfBoost = 0;
            }
            else
            {
                powerOfBoost -= Time.deltaTime * powerOfBoost / timeOfBoost;
                transform.Translate(transform.up * powerOfBoost * Time.deltaTime, Space.World);
            }
        }
    }

    void HoldToDive()
    {
        if (controlplayer.transform.eulerAngles.x + (stepAngle * 2) < minAngleX)
        {
            controlplayer.transform.Rotate(stepAngle * 2, 0, 0);
        }
    }

    public void boostUP(float dureeMontee, float forceMontee)
    {
        timeOfBoost = dureeMontee;
        powerOfBoost = forceMontee;
        boostUp = true;
    }
 
    public void looseLife(int lifeLoose)
    {
        Life -= lifeLoose;
        UIManager.Instance.destroyHeartUI();

        if (Life <= 0)
        {
            UIManager.Instance.showGameoverPanel();
            Destroy(gameObject);
        }
    }

    public void winLife(int lifeWin)
    {
        Life += lifeWin;
        UIManager.Instance.addHeartUI();
    }

    public void increaseMaxSpeed(float increase)
    {
        maxSpeed += increase;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Delta_Race : MonoBehaviour
{
    #region Variables

    //Make this script a singleton
    public static Player_Delta_Race Instance; 

    //Variables Global of Player
    bool playerPressedScreen = false;
    public int Life;
    public int numberofCoin;
    [SerializeField] float TopMaxDistance;
    [SerializeField] GameObject controlplayer = null;

    //Variables Rotation Player
    [SerializeField] float minAngleX;
    [SerializeField] float maxAngleX;
    [SerializeField] float stepAngle;
    float middleAngle;

    //Variables mouvements en Y
    [SerializeField] float speedY;
    [SerializeField] float minSpeedY;
    [SerializeField] float maxSpeedY;
    [SerializeField] float maxSpeedDescenteY;
    [Range(1, 8)] [SerializeField] float coefAccelY;
    [Range(1, 8)] [SerializeField] float coefDeccelY;
    float coefMult = 1;

    //Variables mouvements en X
    [SerializeField] float speedX;
    [SerializeField] float minSpeedX;
    [SerializeField] float maxSpeedX;
    [Range(1, 8)] [SerializeField] float coefAccelX;
    [Range(1, 8)] [SerializeField] float coefDeccelX;
    [SerializeField] float minimumFingerMove; //Distance to do with finger before doing any side movment. 
    [SerializeField] float middleScreenX;
    [SerializeField] float stepMovmentX;
    [SerializeField] float minDetectNumber = 80;
    Vector2 startPos; //Start pos of finger when tuch screen
    Vector2 direction; //Direction of the finger when move on screen

    //Boost Power Variables
    private bool boostUp = false;
    private float timeOfBoost;
    private float powerOfBoost;

    #endregion

    #region MonoBehaviourFunctions

    private void Awake()  
    {
        // Make a singleton, because only one player in game
        if (Instance == null)
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
        //Ajust life of player for the level
        Life = LevelManager.Instance.lifePlayerInLevel;
        //Make UI hearts matches with player life
        UIManager.Instance.uploadHeartUI();
        //Calculate MiddleAngle with maxAngle and minAngle
        middleAngle = maxAngleX + ((minAngleX - maxAngleX) / 2);
        numberofCoin = 0;
    }

    void Update()
    {
        //If the player start touches the screen 
        if (Input.touchCount > 0 && Input.touches[0].phase == TouchPhase.Began)
        {
            Touch touch = Input.GetTouch(0);
            startPos = touch.position;
            playerPressedScreen = true;
        }

        //If the player move finger on the screen 
        if (Input.touchCount > 0 && Input.touches[0].phase == TouchPhase.Moved)
        {
            Touch touch = Input.GetTouch(0);
            direction = (touch.position - startPos);

            //If the finger go to the left
            if (direction.x < 0 && transform.position.x > middleScreenX - minimumFingerMove) 
            {
                transform.Translate(transform.right * -stepMovmentX * Time.deltaTime, Space.World);
                if (speedX < minSpeedX + 10)
                {
                    playerPressedScreen = false;
                }
                else
                {
                    playerPressedScreen = true;
                }
            }

            //If the finger go to the right
            if (direction.x > 0 && transform.position.x < middleScreenX + minimumFingerMove) 
            {
                transform.Translate(transform.right * stepMovmentX * Time.deltaTime, Space.World);
                if (speedX < minSpeedX + 10)
                {
                    playerPressedScreen = false;
                }
                else
                {
                    playerPressedScreen = true;
                }
            }

            if (direction.x < minDetectNumber && direction.x > -minDetectNumber)
            {
                playerPressedScreen = true;
            }
        }

        //If the player stop touches the screen 
        if (Input.touchCount > 0 && Input.touches[0].phase == TouchPhase.Ended) 
        {
            playerPressedScreen = false;
        }

        //If the player touches the screen without moving it
        if (playerPressedScreen) 
        {
            coefMult = coefAccelX; //Acceleration coefficient X
            HoldToDive();
        }
        else //If the player stop touches the screen 
        {
            if (controlplayer.transform.eulerAngles.x > maxAngleX)
            {
                coefMult = -coefDeccelX; //Acceleration coefficient X
                controlplayer.transform.Rotate(-stepAngle, 0, 0);
            }
        }

        float difAngle = minAngleX - maxAngleX;

        //Calcul coef X speed
        float difSpeed = maxSpeedX - minSpeedX;
        float coefSpeedAngle = stepAngle * difSpeed / difAngle;

        //Calcul coef Y speed
        float difSpeedY = maxSpeedY - minSpeedY;
        float coefSpeedYAngle = stepAngle * difSpeedY / difAngle;


        //Calcul speed X (move forward)
        if (speedX + (coefSpeedAngle * coefMult) < maxSpeedX && speedX + (coefSpeedAngle * coefMult) > minSpeedX)
        {
            speedX += coefSpeedAngle * coefMult;
        }
        transform.Translate(transform.forward * speedX * Time.deltaTime, Space.World);


        //Calcul speed Y (move up and down)
        if (controlplayer.transform.eulerAngles.x > middleAngle)
        {
            //Move down
            if (speedY - (coefSpeedYAngle * coefDeccelY) > -maxSpeedDescenteY)
            {
                speedY -= (coefSpeedYAngle * coefDeccelY);
            }
            transform.Translate(transform.up * speedY * Time.deltaTime, Space.World);
        }
        else
        {
            //Move up
            if (speedY + coefSpeedYAngle * coefAccelY < maxSpeedY)
            {
                speedY += coefSpeedYAngle * coefAccelY;
            }
            //The player does not exceed a certain height (TopMaxDistance) 
            if (transform.position.y < TopMaxDistance)
            {
                transform.Translate(transform.up * speedY * Time.deltaTime, Space.World);
            }
        }

        //Boost Power with timer (timeOfBoost)
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

    private void OnDestroy()
    {
        controlplayer = null;
    }

    #endregion

    #region Functions
    void HoldToDive()
    {
        if (controlplayer.transform.eulerAngles.x + (stepAngle) < minAngleX)
        {
            controlplayer.transform.Rotate(stepAngle, 0, 0);
        }
    }

    public void boostUP(float dureeMontee, float forceMontee)
    {
        timeOfBoost = dureeMontee;
        powerOfBoost = forceMontee;
        //boostUp will start the timer in the udpate
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
        maxSpeedX += increase;
    }

    public void decreaseMaxSpeed(float decrease)
    {
        maxSpeedX -= decrease;
    }

    #endregion
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance; //For singleton

    public GameObject panelGameOver;
    public GameObject panelPlayerStats;
    public GameObject heart;
    float spaceBetweenHeart = 50;
    float compteurSpaceHeart;

    public Text textNumberCoins;

    public GameObject aiguilleCadran;
    public Text textVitesseKmh;
    float speedMaxAtteignable;
    public float maxKMHText;

    //End Panel
    public GameObject panelEndLevel;
    public Text textFinalCoins;
    public Text textFinalHeart;
    public Text textFinalTime;
    public Text textFinalPercentage;
    public Text textFinalMessage;
    public GameObject buttonNextLevel;
    bool gameIsFinish = false;
    float timerLevel;

    public Color colorTextGood;
    public Color colorTextBad;

    List<GameObject> listOfheart = new List<GameObject>();

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        Time.timeScale = 1; // REMET LE JEU EN MARCHE
        UploadTextCoins();
        speedMaxAtteignable = Player.Instance.maxSpeed + 2;
        Debug.Log("SPEED MAX ATTEINGABLE = " + speedMaxAtteignable);

        panelGameOver.SetActive(false);
        panelPlayerStats.SetActive(true);
        panelEndLevel.SetActive(false);

        //uploadHeartUI();
    }

    public void uploadHeartUI()
    {
        compteurSpaceHeart = 0;

        for (int i = 0; i < Player.Instance.Life; i++)
        {
            GameObject heartInstantiate = Instantiate(heart, panelPlayerStats.transform);
            listOfheart.Add(heartInstantiate);
            RectTransform trsf = heartInstantiate.GetComponent<RectTransform>();
            trsf.localPosition = new Vector3(trsf.localPosition.x + compteurSpaceHeart, trsf.localPosition.y, trsf.localPosition.z);
            compteurSpaceHeart += spaceBetweenHeart;
        }
        compteurSpaceHeart = 0;
    }

    public void showGameoverPanel()
    {
        panelGameOver.SetActive(true);
        panelPlayerStats.SetActive(false);
    }

    public void destroyHeartUI()
    {
        if (listOfheart.Count > 0)
        {
            GameObject heartToDestroy = listOfheart[listOfheart.Count - 1];
            listOfheart.RemoveAt(listOfheart.Count - 1);
            Destroy(heartToDestroy);
        }
    }

    public void addHeartUI()
    {
        GameObject heartInstantiate = Instantiate(heart, panelPlayerStats.transform);
        RectTransform trsf = heartInstantiate.GetComponent<RectTransform>();
        trsf.localPosition = new Vector3(listOfheart[listOfheart.Count - 1].GetComponent<RectTransform>().localPosition.x + spaceBetweenHeart, trsf.localPosition.y, trsf.localPosition.z);
        listOfheart.Add(heartInstantiate);
    }

    public void PressReplayButton()
    {
        Scene scene = SceneManager.GetActiveScene();
        Time.timeScale = 1; // REMET LE JEU EN MARCHE
        SceneManager.LoadScene(scene.name);
    }

    public void PressMenuButton()
    {
        Time.timeScale = 1; // REMET LE JEU EN MARCHE
        SceneManager.LoadScene("MenuPrincipal");
    }
    public void PressNextLevelButton()
    {
        if(ScenesManager.Instance.nextSceneExist()) //ScenesManager.Instance.currentSceneIndex + 1 < ScenesManager.Instance.sceneNameArray.Length
        {
            Time.timeScale = 1; // REMET LE JEU EN MARCHE
            ScenesManager.Instance.loadNextScene();
        }
    }

    public void UploadTextCoins()
    {
        textNumberCoins.text = "X " + Player.Instance.numberofCoin.ToString();
    }

    private void Update()
    {
        adaptRotationOfAiguille();

        if (!gameIsFinish)
        {
            timerLevel += Time.deltaTime;
        }
    }

    void adaptRotationOfAiguille()
    {
        float speed = Player.Instance.speed - Player.Instance.minSpeed;
        float maxspeed = Player.Instance.maxSpeed - Player.Instance.minSpeed;
        speed = Mathf.Clamp(speed, 0, maxspeed);
        
        //On obtient l'équation linéaire de l'angle de l'aiguille suivante lorsque l'on sait que le minAngle = 359 et le maxAngle = 145.
        float rotAiguilleZ = (-214 / maxspeed) * speed + 359;

        aiguilleCadran.transform.eulerAngles = new Vector3(aiguilleCadran.transform.eulerAngles.x, aiguilleCadran.transform.eulerAngles.y, rotAiguilleZ);

        textVitesseKmh.text = timerLevel.ToString("F0") + " Sec";
    }

    public void EndOfLevel()
    {
        gameIsFinish = true;
        panelPlayerStats.SetActive(false);
        panelEndLevel.SetActive(true);
        textFinalCoins.text = Player.Instance.numberofCoin.ToString() + "/" + LevelManager.Instance.numberCoinsInLevel.ToString("F0");
        textFinalHeart.text = Player.Instance.Life.ToString() + "/" + LevelManager.Instance.lifePlayerInLevel;
        textFinalTime.text = timerLevel.ToString("F0") + " Sec";

        //COINS
        float percentageCoins = Player.Instance.numberofCoin * 100 / LevelManager.Instance.numberCoinsInLevel;
        percentageCoins = Mathf.Clamp(percentageCoins, 0, 100);
        if (Player.Instance.numberofCoin >= LevelManager.Instance.numberCoinsInLevel / 2){
            textFinalCoins.color = colorTextGood;
        }
        else { textFinalCoins.color = colorTextBad; }

        ScenesManager.Instance.stockPieces += Player.Instance.numberofCoin;

        //HEART
        float percentageHeart = Player.Instance.Life * 100 / LevelManager.Instance.lifePlayerInLevel;
        percentageHeart = Mathf.Clamp(percentageHeart, 0, 100);
        if (Player.Instance.Life >= LevelManager.Instance.lifePlayerInLevel)
        {
            textFinalHeart.color = colorTextGood;
        }
        else { textFinalHeart.color = colorTextBad; }

        //TIME
        float percentageTime;
        if (timerLevel <= LevelManager.Instance.TimeToDoLevel)
        {
            percentageTime = 100;
            textFinalTime.color = colorTextGood;
        }
        else 
        {
            percentageTime = 100 - ((timerLevel - LevelManager.Instance.TimeToDoLevel)* 5);
            textFinalTime.color = colorTextBad;
        }
        percentageTime = Mathf.Clamp(percentageTime, 0, 100);

        //FINAL
        float finalPercentage = (percentageCoins + percentageHeart + percentageTime) / 3;
        textFinalPercentage.text = finalPercentage.ToString("F0") + "%";

        if (finalPercentage < LevelManager.Instance.globalPercentageForWin)
        {
            buttonNextLevel.SetActive(false);
            textFinalPercentage.color = colorTextBad;

            textFinalMessage.color = colorTextGood;
            textFinalMessage.text = "Let's try again!";
        }
        else
        {
            buttonNextLevel.SetActive(true);
            textFinalPercentage.color = colorTextGood;

            textFinalMessage.color = colorTextGood;
            textFinalMessage.text = "Well Done!";

            if (finalPercentage == 100)
            {
                textFinalMessage.text = "Perfect!";
            }
        }


        //ACTUALISER LE POURCENTAGE DU NIVEAU ET DELOCKER LE NIVEAU SUIVANT.
        ScenesManager.Instance.percentageOfSuccessArray[ScenesManager.Instance.currentSceneIndex] = (int)finalPercentage;
        if (ScenesManager.Instance.nextSceneExist()) //ScenesManager.Instance.currentSceneIndex + 1 < ScenesManager.Instance.sceneNameArray.Length
        {
            ScenesManager.Instance.isUnlockArray[ScenesManager.Instance.currentSceneIndex + 1] = true;
        }
        else
        {
            buttonNextLevel.SetActive(false);
        }

        //ENREGISTRE TOUTE LES DONNEES DANS LES PLAYERS PREFS
        ScenesManager.Instance.savingAllDatas();

        Time.timeScale = 0; // MET LE JEU EN PAUSE
    }

}

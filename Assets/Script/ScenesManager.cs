using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ScenesManager : MonoBehaviour
{
    public static ScenesManager Instance;

    public string[] sceneNameArray;
    public bool[] isUnlockArray;
    public int[] percentageOfSuccessArray;

    public int currentSceneIndex;

    public int stockPieces;
    public Color colorPerso;

    public bool[] ColorIsLock;
    public ColorButton[] ColorButtonArray;

    void Awake()
    {
        if (Instance == null)
        {
            DontDestroyOnLoad(gameObject); // Pour passer entre les scenes. 
            Instance = this;
        }
        else if (Instance != this)
        {
            Destroy(gameObject);
        }

        //PlayerPrefs.DeleteAll(); // SUPPRIMER
        getAllColorButtonInScene();

        startGameFirstLoad();  
    }

    public void getAllColorButtonInScene()
    {
        ColorButtonArray = Resources.FindObjectsOfTypeAll<ColorButton>();
        Debug.Log("NOMBRE DE BOUTON COLOR TROUVER :" + ColorButtonArray.Length);
        if (ColorButtonArray.Length > 0)
        {
            ColorIsLock = new bool[ColorButtonArray.Length];
        }
    }

    private void OnValidate()
    {
        int numberScene = sceneNameArray.Length;
        isUnlockArray = new bool[numberScene];
        percentageOfSuccessArray = new int[numberScene];
        // Mettre des valeurs de base dans les tableaux et delock le premier niveau
        for (int i = 0; i < sceneNameArray.Length; i++)
        {
            isUnlockArray[i] = false;
            percentageOfSuccessArray[i] = 0;
        }
        isUnlockArray[0] = true;
    }

    public bool nextSceneExist()
    {
        if (currentSceneIndex + 1 < sceneNameArray.Length)
        {
            return true;
        }
        else
        { return false; }
    }

    public void loadNextScene()
    {
        currentSceneIndex += 1;
        SceneManager.LoadScene(sceneNameArray[currentSceneIndex]);
    }

    public void startGameFirstLoad()
    {
        if (!PlayerPrefs.HasKey("FirstTimeGameIsLaunch")) // Si le jeux n'a jamais été lancé avant. 
        {
            PlayerPrefs.SetInt("FirstTimeGameIsLaunch", 1);

            savingAllDatas();
            savingColorData();
        }
        else // Si le jeux a deja été lancé avant car "FirstTimeGameIsLaunch" n'existe pas.
        {
            loadAllDatas();
            loadColorDatas();
        }
    }

    public void savingAllDatas()
    {
        Debug.Log("SAVING ALL DATAS");

        for (int i = 0; i < sceneNameArray.Length; i++)
        {
            // Enregistrement du lock ou unlock des scenes
            string nameOfIndexBool = "SceneUnlock" + i.ToString();
            Debug.Log("UNLOCK of scene : " + nameOfIndexBool);
            if (isUnlockArray[i])
            {
                PlayerPrefs.SetInt(nameOfIndexBool, 1);
            }
            else
            {
                PlayerPrefs.SetInt(nameOfIndexBool, 0);
            }

            // Enregistrement du percent of success / percentageOfSuccessArray
            string nameOfIndexInt = "ScenePercent" + i.ToString();
            PlayerPrefs.SetInt(nameOfIndexInt, percentageOfSuccessArray[i]);

            //Color
            PlayerPrefs.SetFloat("ColorRed", colorPerso.r);
            PlayerPrefs.SetFloat("ColorGreen", colorPerso.g);
            PlayerPrefs.SetFloat("ColorBlue", colorPerso.b);
            PlayerPrefs.SetFloat("ColorAlpha", colorPerso.a);

            //Money
            PlayerPrefs.SetInt("stockPieces", stockPieces);
        }
    }

    public void savingColorData()
    {
        if (ColorButtonArray.Length > 0)
        {
            foreach (ColorButton button in ColorButtonArray)
            {
                int indexButton = button.buttonColorIndex;
                bool IsLockButon = button.ColorIsLock;
                ColorIsLock[indexButton] = IsLockButon;

                string nameButton = "ButtonColor" + indexButton.ToString();
                if (IsLockButon)
                {
                    PlayerPrefs.SetInt(nameButton, 1);
                }
                else
                {
                    PlayerPrefs.SetInt(nameButton, 0);
                }
            }
        }
    }

    public void loadAllDatas()
    {
        Debug.Log("LOAD ALL DATAS");

        for (int i = 0; i < sceneNameArray.Length; i++)
        {
            // Load du lock ou unlock des scenes
            string nameOfIndexBool = "SceneUnlock" + i.ToString();
            if (PlayerPrefs.GetInt(nameOfIndexBool) == 1)
            {
                isUnlockArray[i] = true;
            }
            else
            {
                isUnlockArray[i] = false;
            }

            // Load du percent of success / percentageOfSuccessArray
            string nameOfIndexInt = "ScenePercent" + i.ToString();
            Debug.Log("LOAD PERCENT of scene : " + nameOfIndexBool);
            percentageOfSuccessArray[i] = PlayerPrefs.GetInt(nameOfIndexInt);

            //Color
            colorPerso = new Color(PlayerPrefs.GetFloat("ColorRed"), PlayerPrefs.GetFloat("ColorGreen"), PlayerPrefs.GetFloat("ColorBlue"), PlayerPrefs.GetFloat("ColorAlpha"));

            //Money
            stockPieces = PlayerPrefs.GetInt("stockPieces");
        }

    }

    public void loadColorDatas()
    {
        if (PlayerPrefs.HasKey("FirstTimeGameIsLaunch")) // Si le jeux a deja été lancé avant. 
        {
            if (ColorButtonArray.Length > 0)
            {
                foreach (ColorButton button in ColorButtonArray)
                {
                    int indexButton = button.buttonColorIndex;

                    string nameButton = "ButtonColor" + indexButton.ToString();

                    if (PlayerPrefs.GetInt(nameButton) == 1)
                    {
                        ColorIsLock[indexButton] = true;
                        button.ColorIsLock = true;
                        button.adaptButton();
                    }
                    if (PlayerPrefs.GetInt(nameButton) == 0)
                    {
                        ColorIsLock[indexButton] = false;
                        button.ColorIsLock = false;
                        button.adaptButton();
                    }
                }
            }
        }
    }
}
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ButtonMenu : MonoBehaviour
{
    public int levelNumber;

    public Image ImageRef;
    public Sprite spriteLock;
    public Sprite spriteUnlock;

    public Text textNumberLevel;
    public Text textPercentageDone;

    private void Start()
    {
        loadVisuelOfButton();
    }

    public void loadVisuelOfButton()
    {
        if (ScenesManager.Instance.isUnlockArray[levelNumber])
        {
            ImageRef.sprite = spriteUnlock;

            textNumberLevel.enabled = true;
            textPercentageDone.enabled = true;

            textNumberLevel.text = levelNumber.ToString();
            textPercentageDone.text = ScenesManager.Instance.percentageOfSuccessArray[levelNumber].ToString("F0") + "%";
        }
        else
        {
            ImageRef.sprite = spriteLock;

            textNumberLevel.enabled = false;
            textPercentageDone.enabled = false;
        }
    }

    public void loadSceneOfThisButton()
    {
        if (ScenesManager.Instance.isUnlockArray[levelNumber]) //charge le niveau uniquement si le niveau est delock. 
        {
            ScenesManager.Instance.currentSceneIndex = levelNumber;
            SceneManager.LoadScene(ScenesManager.Instance.sceneNameArray[levelNumber]);
        }
    }
}

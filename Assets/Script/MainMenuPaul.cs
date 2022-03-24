using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class MainMenuPaul : MonoBehaviour
{
    public static MainMenuPaul Instance;
    public GameObject PanelLevel;
    public GameObject PanelShop;
    public GameObject PanelDevelop;

    public GameObject decoPerso;

    public Text money;
    public Text money2;

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

    private void Start()
    {
        adaptMoneyText();
        ScenesManager.Instance.getAllColorButtonInScene();
        ScenesManager.Instance.loadColorDatas();
    }

    public void shopButton()
    {
        PanelShop.SetActive(true);
        PanelLevel.SetActive(false);
    }
    public void levelButton()
    {
        PanelShop.SetActive(false);
        PanelLevel.SetActive(true);
    }
    public void adaptColor()
    {
        Color colorButton = EventSystem.current.currentSelectedGameObject.GetComponent<Image>().color; // Get the color of the button we just clicked. 
        Debug.Log(colorButton);
        decoPerso.GetComponent<MeshRenderer>().sharedMaterial.SetColor("_Color", colorButton); //Set the color parameter for the material Player Use. 
    }

    public void adaptMoneyText()
    {
        money.text = ScenesManager.Instance.stockPieces.ToString();
        money2.text = ScenesManager.Instance.stockPieces.ToString();
    }

    public void ResetApp()
    {
        PlayerPrefs.DeleteAll();
        Application.Quit();
    }

    public void devMenu()
    {
        if (PanelDevelop.activeInHierarchy)
        {
            PanelDevelop.SetActive(false);
            PanelLevel.SetActive(true);
        }
        else
        {
            PanelDevelop.SetActive(true);
            PanelLevel.SetActive(false);
        }
    }

    public void delockAllLevels()
    {
        ButtonMenu[] allButtonsMenu = Resources.FindObjectsOfTypeAll<ButtonMenu>();

        for (int i = 0; i < ScenesManager.Instance.isUnlockArray.Length; i++)
        {
            ScenesManager.Instance.isUnlockArray[i] = true;
        }

        foreach (ButtonMenu item in allButtonsMenu)
        {
            item.loadVisuelOfButton();
        }

        ScenesManager.Instance.savingAllDatas();
        devMenu();
    }
}

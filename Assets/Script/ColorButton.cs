using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class ColorButton : MonoBehaviour
{
    
    public bool ColorIsLock = true;
    public int buttonColorIndex;
    public int priceOfColor;
    public Text priceText;

    public Color colorLock;
    public Color colorUnlock;

    public Sprite spriteLock;
    public Sprite spriteunlock;

    public GameObject decoPerso;

    private void Start()
    {
        adaptButton();
    }

    public void adaptButton()
    {
        Image imagecomp = gameObject.GetComponent<Image>();

        if (ColorIsLock)
        {
            priceText.text = priceOfColor.ToString();
            imagecomp.color = colorLock;
            imagecomp.sprite = spriteLock;
        }
        else
        {
            delockButton();
        }
    }

    public void clickOnButton()
    {
        if (ColorIsLock)
        {
            if (ScenesManager.Instance.stockPieces >= priceOfColor)
            {
                ScenesManager.Instance.stockPieces -= priceOfColor;
                MainMenuPaul.Instance.adaptMoneyText();
                delockButton();
                ScenesManager.Instance.savingColorData();
                ScenesManager.Instance.savingAllDatas();
            }
        }
        else
        {
            //Changer couleur perso. 
            Color colorButton = EventSystem.current.currentSelectedGameObject.GetComponent<Image>().color; // Get the color of the button we just clicked. 
            Debug.Log(colorButton);
            decoPerso.GetComponent<MeshRenderer>().sharedMaterial.SetColor("_Color", colorButton); //Set the color parameter for the material Player Use. 
        } 
    }

    public void delockButton()
    {
        Image imagecomp = gameObject.GetComponent<Image>();

        ColorIsLock = false;
        priceText.text = "";
        imagecomp.color = colorUnlock;
        imagecomp.sprite = spriteunlock;
    }
}

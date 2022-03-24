using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Sensi_EventSystem : MonoBehaviour
{
    void Start()
    {
        int defaultValue = EventSystem.current.pixelDragThreshold;
        //Prendre la plus grosse valeur entre le pixelDrag par default ou le calcul en fonction des DPI. 
        EventSystem.current.pixelDragThreshold = Mathf.Max(defaultValue, (int)(defaultValue * Screen.dpi / 160f));
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Events;

public class TextButton : MonoBehaviour, IPointerClickHandler
{
    public UnityEvent onClick;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void OnPointerClick(PointerEventData pointerEventData)
    {
        // invoke your event
        onClick.Invoke();
    }
}

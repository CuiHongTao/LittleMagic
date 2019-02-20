using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class CS_TurnOver : MonoBehaviour, IPointerDownHandler
{

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void OnPointerDown(PointerEventData eventData)
    {
        if (!StaticScript.lockMouse)
        {
            GameObject.Find("TurnOverPanel").GetComponent<CS_TurnOverPanel>().TurnChanged();
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class CS_Conscribe : MonoBehaviour, IPointerDownHandler
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
        if (eventData.button == 0)
            GameObject.Find("StatePanel").GetComponent<CS_StatePanel>().ChangeShow(name);
    }
}

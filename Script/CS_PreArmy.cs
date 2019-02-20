using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class CS_PreArmy : MonoBehaviour, IPointerDownHandler
{

    public CharacterScript characterScript;

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
        if (characterScript.localPosition.StartsWith("-"))
        {
            GameObject go;
            if (CS_TurnOverPanel.turn == FinalVar.RED)
            {
                go = GameObject.Find(MapManagerScript.kingOfRed);
            }
            else
            {
                go = GameObject.Find(MapManagerScript.kingOfBlue);
            }
            if (CS_ArmyManager.GetCharacter(go.name) != null)
            {
                CharacterScript cs = CS_ArmyManager.GetCharacter(go.name);
                cs.MoveTo(characterScript.gameObject);
            }
            GameObject.Find("StatePanel").GetComponent<CS_StatePanel>().OverShow();
            GameObject.Find("MainCamera").GetComponent<CameraScript>().MoveTo(go);
            characterScript.MoveTo(go);
        }
        else
        {
            GameObject.Find("StatePanel").GetComponent<CS_StatePanel>().OverShow();
            GameObject.Find("MainCamera").GetComponent<CameraScript>().MoveTo(GameObject.Find(characterScript.localPosition));
        }
    }
}
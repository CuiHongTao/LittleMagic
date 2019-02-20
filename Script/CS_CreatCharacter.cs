using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class CS_CreatCharacter : MonoBehaviour, IPointerDownHandler
{

    public string characterName;
    private static int no = 0;

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
        GameObject go;
        Transform tr;
        if (CS_TurnOverPanel.turn == FinalVar.RED)
        {
            go = Resources.Load(characterName + "Red", typeof(GameObject)) as GameObject;
            tr = GameObject.Find("RedArmy").transform;
        }
        else
        {
            go = Resources.Load(characterName + "Blue", typeof(GameObject)) as GameObject;
            tr = GameObject.Find("BlueArmy").transform;
        }
        GameObject gObject = Instantiate(go) as GameObject;
        gObject.transform.position = new Vector3(0, -2, 0);
        gObject.GetComponent<CharacterScript>().localPosition = (--no).ToString();
        gObject.transform.SetParent(tr);
        if (CS_TurnOverPanel.turn == FinalVar.RED)
        {
            CS_ArmyManager.AddRedCharacter(no.ToString(), gObject.GetComponent<CharacterScript>());
        }
        else
        {
            CS_ArmyManager.AddBlueCharacter(no.ToString(), gObject.GetComponent<CharacterScript>());
        }
    }
}

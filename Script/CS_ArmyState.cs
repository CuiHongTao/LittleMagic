using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class CS_ArmyState : MonoBehaviour, IPointerDownHandler
{
    public GameObject armyStateList;

    // Use this for initialization
    void Start()
    {
        armyStateList = GameObject.Find("ArmyStateList");
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void OnPointerDown(PointerEventData eventData)
    {
        if (eventData.button == 0)
        {
            GameObject.Find("StatePanel").GetComponent<CS_StatePanel>().ChangeShow(name);
            Refresh();
        }
    }

    public void Refresh()
    {
        int armyCount;
        int i = armyStateList.transform.childCount;
        if (CS_TurnOverPanel.turn == FinalVar.RED)
        {
            armyCount = CS_ArmyManager.GetRedCount();
        }
        else
        {
            armyCount = CS_ArmyManager.GetBlueCount();
        }
        while (i > armyCount)
        {
            Destroy(armyStateList.transform.GetChild(--i).gameObject);
        }
        while (i < armyCount)
        {
            GameObject go = Resources.Load("PreArmy") as GameObject;
            GameObject preArmy = Instantiate(go) as GameObject;
            preArmy.transform.SetParent(armyStateList.transform, false);
            preArmy.GetComponent<RectTransform>().sizeDelta = new Vector2(Screen.width, preArmy.GetComponent<RectTransform>().rect.height);
            preArmy.transform.position = armyStateList.transform.position;
            preArmy.transform.Translate(Vector3.down * i++ * preArmy.GetComponent<RectTransform>().rect.height);
        }
        while (i > 0)
        {
            i--;
            CharacterScript cs = CS_ArmyManager.GetCharacter(i);
            armyStateList.transform.GetChild(i).GetComponent<CS_PreArmy>().characterScript = cs;
            armyStateList.transform.GetChild(i).transform.GetChild(1).GetComponent<Text>().text = cs.charactorName;
            armyStateList.transform.GetChild(i).transform.GetChild(3).GetComponent<Text>().text = i.ToString();
            armyStateList.transform.GetChild(i).transform.GetChild(5).GetComponent<Text>().text = cs.hp.ToString();
            armyStateList.transform.GetChild(i).transform.GetChild(7).GetComponent<Text>().text = cs.mp.ToString();
            armyStateList.transform.GetChild(i).transform.GetChild(9).GetComponent<Text>().text = cs.ap.ToString();
            armyStateList.transform.GetChild(i).transform.GetChild(11).GetComponent<Text>().text = cs.dp.ToString();
            armyStateList.transform.GetChild(i).transform.GetChild(13).GetComponent<Text>().text = cs.lv.ToString();
            if (cs.moved)
            {
                armyStateList.transform.GetChild(i).transform.GetChild(14).GetComponent<Text>().text = "移动后";
            }
            else
            {
                armyStateList.transform.GetChild(i).transform.GetChild(14).GetComponent<Text>().text = "移动前";
            }
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CS_TurnOverPanel : MonoBehaviour
{

    private float bluePosition;
    public static bool turn = FinalVar.RED;
    public GameObject go;

    // Use this for initialization
    void Start()
    {
        bluePosition = Screen.width + go.transform.GetChild(1).GetComponent<RectTransform>().rect.width / 2;
        go.transform.GetChild(1).position = (new Vector2(bluePosition, go.transform.GetChild(1).position.y));
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void TurnChanged()
    {
        if (turn)
        {
            go.transform.GetChild(0).GetComponent<Text>().text = "回合结束";
            go.transform.GetChild(1).GetComponent<Text>().text = "回合开始";
            GameObject.Find("MainCamera").GetComponent<CameraScript>().MoveTo(GameObject.Find(MapManagerScript.kingOfBlue));
        }
        else
        {
            go.transform.GetChild(0).GetComponent<Text>().text = "回合开始";
            go.transform.GetChild(1).GetComponent<Text>().text = "回合结束";
            GameObject.Find("MainCamera").GetComponent<CameraScript>().MoveTo(GameObject.Find(MapManagerScript.kingOfRed));
        }
        int j;
        if(turn == FinalVar.RED)
        {
            j = CS_ArmyManager.GetRedCount();
        }
        else
        {
            j = CS_ArmyManager.GetBlueCount();
        }
        for (int i = 0; i < j; i++)
        {
            CS_ArmyManager.GetCharacter(i).moved = false;
        }
        turn = !turn;
        GameObject.Find("StatePanel").GetComponent<CS_StatePanel>().OverShow();
        StartCoroutine(GradualChange());
    }

    IEnumerator GradualChange()
    {
        StaticScript.lockMouse = true;
        while (GetComponent<CanvasGroup>().alpha < 0.7)
        {
            GetComponent<CanvasGroup>().alpha += 0.05f;
            yield return 0;
        }
        if (!turn)
        {
            while (go.transform.localPosition.x > -bluePosition + 0.5 * Screen.width)
            {
                go.transform.Translate(Vector3.left * Time.deltaTime * 2000);
                yield return 0;
            }
        }
        else
        {
            while (go.transform.localPosition.x < 0)
            {
                go.transform.Translate(Vector3.right * Time.deltaTime * 2000);
                yield return 0;
            }
        }
        while (GetComponent<CanvasGroup>().alpha > 0)
        {
            GetComponent<CanvasGroup>().alpha -= 0.05f;
            yield return 0;
        }
        StaticScript.lockMouse = false;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterScript : MonoBehaviour
{
    public bool color;
    public string localPosition;
    public int moveCose = 6;
    public Animator characterAnimator;
    public string charactorName = "王子";
    public int lv = 1;
    public int hp = 100;
    public int mp = 100;
    public int ap = 10;
    public int dp = 5;
    public bool moved;

    // Use this for initialization
    void Start()
    {
        characterAnimator = GetComponent<Animator>();
        moved = false;
    }

    // Update is called once per frame
    void Update()
    {

    }

    public int MoveCose(int type)
    {
        switch (type)
        {
            case FinalVar.GRASS: return 1;
            case FinalVar.TREE: return 2;
            case FinalVar.BEACH: return 2;
            case FinalVar.ICE: return 1;
            case FinalVar.SEA: return 10;
            case FinalVar.SHOAL: return 2;
            case FinalVar.TOWER: return 1;
            case FinalVar.VILLAGE: return 1;
            case FinalVar.TOWN: return 1;
            default: return 100;
        }
    }

    public void Move(List<string> list)
    {
        characterAnimator.SetBool("IsRun", true);
        list.RemoveAt(0);
        StaticScript.lockMouse = true;
        moved = true;
        StartCoroutine(MoveToPosition(list));
    }

    public void MoveTo(GameObject go)
    {
        transform.position = go.transform.position;
    }

    IEnumerator MoveToPosition(List<string> list)
    {
        GameObject aimGameObject = GameObject.Find(list[0]);

        Quaternion qua1 = transform.rotation;
        transform.LookAt(aimGameObject.transform);
        Quaternion qua2 = transform.rotation;
        transform.rotation = qua1;
        StartCoroutine(SlerpToQua(qua2));

        MapManagerScript.ShowBuilding(localPosition);
        localPosition = list[0];
        MapManagerScript.HidBuilding(localPosition);
        while (transform.localPosition != aimGameObject.transform.position)
        {
            transform.localPosition = Vector3.MoveTowards(transform.localPosition, aimGameObject.transform.position, 3 * Time.deltaTime);
            yield return 0;
        }
        if (list.Count > 1)
        {
            list.RemoveAt(0);
            StartCoroutine(MoveToPosition(list));
        }
        else
        {
            characterAnimator.SetBool("IsRun", false);
            StartCoroutine(SlerpToQua(Quaternion.Euler(new Vector3(0, 180, 0))));
            StaticScript.lockMouse = false;
        }
    }

    IEnumerator SlerpToQua(Quaternion qua)
    {
        while (Quaternion.Angle(qua, transform.rotation) > 1)
        {
            //  用 slerp 进行插值平滑的旋转
            transform.rotation = Quaternion.Slerp(transform.rotation, qua, 15 * Time.deltaTime);
            // 当初始角度跟目标角度小于1,将目标角度赋值给初始角度,让旋转角度是我们需要的角度
            if (Quaternion.Angle(qua, transform.rotation) < 1)
            {
                transform.rotation = qua;
            }
            yield return 0;
        }
    }

}

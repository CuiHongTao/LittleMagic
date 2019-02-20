using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class MapScript : MonoBehaviour
{
    private MeshRenderer mr;
    bool isOne = false;// 代表 达不达到 1，默认一开始不为1 【按照i去算】
    public bool mouseOn = false;
    public static bool mapOn = false;
    public int type = FinalVar.GRASS;
    public static Dictionary<string, List<string>> rageMap = new Dictionary<string, List<string>>();
    private static string lastMousedown;
    private float i = 0.4f;

    void Start()
    {
        mr = this.GetComponent<MeshRenderer>();
        MapOn(mapOn);
    }

    // Update is called once per frame
    void Update()
    {
        if (!StaticScript.lockMouse && Input.GetMouseButton(1))
        {
            mouseOn = false;
            MapOn(mapOn);
            rageMap.Clear();
        }
    }

    void OnMouseDown()
    {
        if (EventSystem.current.IsPointerOverGameObject())
        {
            return;
        }

        //锁定鼠标
        if (StaticScript.lockMouse)
        {
            return;
        }

        //点击移动范围内的格子
        else if (rageMap.ContainsKey(name))
        {
            CS_ArmyManager.Move(lastMousedown, name, rageMap[name]);
            MapManagerScript.strs = new List<string>()
            {
                name
            };
            rageMap.Clear();
        }

        //点击角色
        else if (CS_ArmyManager.GetCharacter(name) != null)
        {
            ClickCharacter();
        }

        //其他
        else
        {
            MapManagerScript.strs = new List<string>()
            {
                name
            };
            rageMap.Clear();
        }
        lastMousedown = name;
    }

    public void ClickCharacter()
    {
        rageMap = GetMoveRage(name, CS_ArmyManager.GetCharacter(name).moveCose, null);
        MapManagerScript.strs = rageMap.Keys;
    }

    public void OnMouseEnter()
    {
        if (EventSystem.current.IsPointerOverGameObject())
        {
            return;
        }
        if (CS_ArmyManager.GetCharacter(name) != null)
        {
            GameObject.Find("Canvas").GetComponent<CS_Canvas>().GetCaracterState();
        }
    }

    public void OnMouseExit()
    {
        GameObject.Find("Canvas").GetComponent<CS_Canvas>().DestroyCaracterState();
    }

    public void MapOn(bool setTrue)
    {
        if (setTrue)
        {
            mr.material.color = new Color(mr.material.color.r, mr.material.color.g, mr.material.color.b, 0.4f);
        }
        else
        {
            mr.material.color = new Color(mr.material.color.r, mr.material.color.g, mr.material.color.b, 0.0f);
        }
    }

    public void ShinTrue()
    {
        i = 0.4f;
        if (!mouseOn)
        {
            mouseOn = true;
            StartCoroutine(Shin());
        }
    }

    private IEnumerator Shin()
    {
        while (mouseOn)
        {
            if (!isOne)
            {
                i += Time.deltaTime / 2;
                mr.material.color = new Color(mr.material.color.r, mr.material.color.g, mr.material.color.b, i);
                if (i >= 0.4)
                    isOne = true;
            }
            else
            {
                i -= Time.deltaTime / 2;
                mr.material.color = new Color(mr.material.color.r, mr.material.color.g, mr.material.color.b, i);
                if (i <= 0)
                    isOne = false;
            }
            yield return 0;
        }
    }

    public static int GetX(string self)
    {
        return int.Parse(self.Split(',')[0]);
    }

    public static int GetY(string self)
    {
        return int.Parse(self.Split(',')[1]);
    }

    private Dictionary<string, List<string>> GetMoveRage(string self, int remMoveFar, Dictionary<string, List<string>> resoult)
    {

        if (resoult == null)
        {
            resoult = new Dictionary<string, List<string>>();
            List<string> list = new List<string>
            {
                self
            };
            list[0] = remMoveFar.ToString();
            resoult.Add(self, list);
        }//如果参数结果集为空，创建结果集

        if (remMoveFar == 0)
        {
            return resoult;
        }//如果可移动距离为0，返回结果集（空）

        int selfX = GetX(self);//获取位置x坐标
        int selfY = GetY(self);//获取位置y坐标

        if (selfX < MapManagerScript.sizeX - 1)
        {
            string s = (selfX + 1).ToString() + "," + selfY.ToString();
            AddResoult(s, remMoveFar, resoult, self);
        }//当x坐标不在正反向范围边缘，向正方向取一格范围并添加进结果集，addResoult方法中递归调用

        if (selfX > 0)
        {
            string s = (selfX - 1).ToString() + "," + selfY.ToString();
            AddResoult(s, remMoveFar, resoult, self);
        }//当x坐标不在负反向范围边缘，向负方向取一格范围并添加进结果集，addResoult方法中递归调用

        if (selfY < MapManagerScript.sizeY - 1)
        {
            string s = selfX.ToString() + "," + (selfY + 1).ToString();
            AddResoult(s, remMoveFar, resoult, self);
        }//当y坐标不在正反向范围边缘，向正方向取一格范围并添加进结果集，addResoult方法中递归调用

        if (selfY > 0)
        {
            string s = selfX.ToString() + "," + (selfY - 1).ToString();
            AddResoult(s, remMoveFar, resoult, self);
        }//当y坐标不在负反向范围边缘，向负方向取一格范围并添加进结果集，addResoult方法中递归调用

        return resoult;
    }

    private void AddResoult(string s, int remMoveFar, Dictionary<string, List<string>> resoult, string last)
    {
        int rem = remMoveFar - CS_ArmyManager.GetCharacter(name).MoveCose(GameObject.Find(s).GetComponent<MapScript>().type);//获取剩余移动距离

        if (CS_ArmyManager.GetCharacter(s) != null || rem < 0)
        {
            return;
        }

        if (!resoult.ContainsKey(s))
        {
            List<string> list = new List<string>(resoult[last].ToArray())
                {
                    s
                };
            list[0] = rem.ToString();
            resoult.Add(s, list);
            GetMoveRage(s, rem, resoult);
        }//如果结果集中不存在，则添加进结果集并设置闪烁，调用getMoveRage递归方法

        else
        {
            if (int.Parse(resoult[s][0]) < rem)
            {
                List<string> list = new List<string>(resoult[last].ToArray())
                {
                    s
                };
                list[0] = rem.ToString();
                resoult[s] = list;
                GetMoveRage(s, rem, resoult);
            }
        }//如果结果集中存在，并且本次结果优于上次结果，则本次结果覆盖上次结果，调用getMoveRage递归方法
    }
}

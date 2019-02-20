using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapManagerScript : MonoBehaviour
{
    public static int sizeX = 16;
    public static int sizeY = 10;
    public static int[,] cose ={
        {FinalVar.GRASS,FinalVar.GRASS,FinalVar.GRASS,FinalVar.TREE,FinalVar.TREE,FinalVar.TREE,FinalVar.TREE,FinalVar.BEACH,FinalVar.SHOAL,FinalVar.SHOAL },
        {FinalVar.GRASS,FinalVar.TOWN,FinalVar.GRASS,FinalVar.GRASS,FinalVar.TOWER,FinalVar.GRASS,FinalVar.BEACH,FinalVar.SHOAL,FinalVar.SHOAL,FinalVar.SEA },
        {FinalVar.GRASS,FinalVar.GRASS,FinalVar.GRASS,FinalVar.TREE,FinalVar.TREE,FinalVar.TREE,FinalVar.BEACH,FinalVar.SHOAL,FinalVar.SEA,FinalVar.SEA },
        {FinalVar.GRASS,FinalVar.GRASS,FinalVar.GRASS,FinalVar.GRASS,FinalVar.TREE,FinalVar.BEACH,FinalVar.SHOAL,FinalVar.SHOAL,FinalVar.SEA,FinalVar.SEA },
        {FinalVar.BEACH,FinalVar.GRASS,FinalVar.TREE,FinalVar.VILLAGE,FinalVar.TREE,FinalVar.BEACH,FinalVar.SHOAL,FinalVar.SEA,FinalVar.SEA,FinalVar.SEA },
        {FinalVar.SHOAL,FinalVar.BEACH,FinalVar.TREE,FinalVar.GRASS,FinalVar.TREE,FinalVar.BEACH,FinalVar.SHOAL,FinalVar.SEA,FinalVar.SEA,FinalVar.SEA },
        {FinalVar.SHOAL,FinalVar.SHOAL,FinalVar.BEACH,FinalVar.GRASS,FinalVar.GRASS,FinalVar.BEACH,FinalVar.SHOAL,FinalVar.SHOAL,FinalVar.SEA,FinalVar.SEA },
        {FinalVar.SEA,FinalVar.SHOAL,FinalVar.SHOAL,FinalVar.BEACH,FinalVar.GRASS,FinalVar.GRASS,FinalVar.BEACH,FinalVar.SHOAL,FinalVar.SEA,FinalVar.SEA },
        {FinalVar.SEA,FinalVar.SEA,FinalVar.SHOAL,FinalVar.BEACH,FinalVar.GRASS,FinalVar.GRASS,FinalVar.BEACH,FinalVar.SHOAL,FinalVar.SHOAL,FinalVar.SEA },
        {FinalVar.SEA,FinalVar.SEA,FinalVar.SHOAL,FinalVar.SHOAL,FinalVar.BEACH,FinalVar.GRASS,FinalVar.GRASS,FinalVar.BEACH,FinalVar.SHOAL,FinalVar.SHOAL },
        {FinalVar.SEA,FinalVar.SEA,FinalVar.SEA,FinalVar.SHOAL,FinalVar.BEACH,FinalVar.TREE,FinalVar.GRASS,FinalVar.TREE,FinalVar.BEACH,FinalVar.SHOAL },
        {FinalVar.SEA,FinalVar.SEA,FinalVar.SEA,FinalVar.SHOAL,FinalVar.BEACH,FinalVar.TREE,FinalVar.VILLAGE,FinalVar.TREE,FinalVar.GRASS,FinalVar.BEACH },
        {FinalVar.SEA,FinalVar.SEA,FinalVar.SHOAL,FinalVar.SHOAL,FinalVar.BEACH,FinalVar.TREE,FinalVar.GRASS,FinalVar.GRASS,FinalVar.GRASS,FinalVar.GRASS },
        {FinalVar.SEA,FinalVar.SEA,FinalVar.SHOAL,FinalVar.BEACH,FinalVar.TREE,FinalVar.TREE,FinalVar.TREE,FinalVar.GRASS,FinalVar.GRASS,FinalVar.GRASS },
        {FinalVar.SEA,FinalVar.SHOAL,FinalVar.SHOAL,FinalVar.BEACH,FinalVar.GRASS,FinalVar.TOWER,FinalVar.GRASS,FinalVar.GRASS,FinalVar.TOWN,FinalVar.GRASS },
        {FinalVar.SHOAL,FinalVar.SHOAL,FinalVar.BEACH,FinalVar.TREE,FinalVar.TREE,FinalVar.TREE,FinalVar.TREE,FinalVar.GRASS,FinalVar.GRASS,FinalVar.GRASS }
    }; //地图信息
    public static string kingOfRed = "1,1";
    public static string kingOfBlue = "14,8";
    public static IEnumerable<string> strs = null;
    public static Dictionary<string, GameObject> buildings = new Dictionary<string, GameObject>();

    // Use this for initialization
    void Start()
    {
        GameObject go = Resources.Load("PreMap", typeof(GameObject)) as GameObject;
        for (int i = 0; i < sizeX; i++)
        {
            for (int j = 0; j < sizeY; j++)
            {
                GameObject preMap = Instantiate(go) as GameObject;
                preMap.transform.SetParent(GameObject.Find("Map").transform, false);
                preMap.transform.Translate(new Vector3(i, 0, j));
                preMap.transform.name = i.ToString() + "," + j.ToString();
                preMap.GetComponent<MapScript>().type = cose[i, j];
            }
        }//加载UI地图

        GameObject go2 = Resources.Load("KingOfRed", typeof(GameObject)) as GameObject;
        GameObject preMap2 = Instantiate(go2) as GameObject;
        preMap2.transform.position = GameObject.Find(kingOfRed).transform.position;
        preMap2.transform.name = "KingOfRed";
        preMap2.GetComponent<CharacterScript>().localPosition = kingOfRed;
        CS_ArmyManager.AddRedCharacter(kingOfRed, preMap2.GetComponent<CharacterScript>());
        //characters.Add(kingOfRed, preMap2.GetComponent<CharacterScript>());

        GameObject go3 = Resources.Load("KingOfBlue", typeof(GameObject)) as GameObject;
        GameObject preMap3 = Instantiate(go3) as GameObject;
        preMap3.transform.position = GameObject.Find(kingOfBlue).transform.position;
        preMap3.transform.name = "KingOfBlue";
        preMap3.GetComponent<CharacterScript>().localPosition = kingOfBlue;
        CS_ArmyManager.AddBlueCharacter(kingOfBlue, preMap3.GetComponent<CharacterScript>());
        //characters.Add(kingOfBlue, preMap3.GetComponent<CharacterScript>());

        buildings.Add("1,4", GameObject.Find("tower"));
        buildings.Add("14,5", GameObject.Find("tower (1)"));
        buildings.Add("4,3", GameObject.Find("village"));
        buildings.Add("11,6", GameObject.Find("village (1)"));
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonUp(0))
        {
            if (strs == null)
            {
                foreach (Transform child in transform)
                {
                    child.GetComponent<MapScript>().mouseOn = false;
                    child.GetComponent<MapScript>().MapOn(MapScript.mapOn);
                }
                MapScript.rageMap.Clear();
            }
            else
            {
                SetShin();
                strs = null;
            }
        }
    }

    public void SetMapOn(bool setOn)
    {
        MapScript.mapOn = setOn;
        foreach (Transform child in transform)
        {
            child.GetComponent<MapScript>().MapOn(setOn);
        }
    }

    public void SetShin()
    {
        List<string> list = new List<string>();
        foreach (string s in strs)
        {
            list.Add(s);
        }
        foreach (Transform child in transform)
        {
            if (list.Contains(child.name))
            {
                GameObject.Find(child.name).GetComponent<MapScript>().ShinTrue();
            }
            else
            {
                child.GetComponent<MapScript>().mouseOn = false;
                child.GetComponent<MapScript>().MapOn(MapScript.mapOn);
            }
        }
    }

    public static void HidBuilding(string localPosition)
    {
        if (buildings.ContainsKey(localPosition))
        {
            if (buildings[localPosition].activeSelf)
            {
                buildings[localPosition].transform.Translate(Vector3.down * 10);
            }
        }
    }

    public static void ShowBuilding(string localPosition)
    {
        if (!buildings.ContainsKey(localPosition))
        {
            return;
        }
        GameObject g = buildings[localPosition];
        foreach (KeyValuePair<string, GameObject> go in buildings)
        {
            if (go.Value == g && CS_ArmyManager.GetCharacter(go.Key) != null)
            {
                return;
            }
        }
        g.transform.Translate(Vector3.up * 10);
    }
}
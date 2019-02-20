using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CS_Canvas : MonoBehaviour
{

    GameObject go;

    // Use this for initialization
    void Start()
    {
        go = Resources.Load("CaracterState", typeof(GameObject)) as GameObject;
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void GetCaracterState()
    {
        GameObject preMap = Instantiate(go) as GameObject;
        preMap.transform.SetParent(transform, false);
        preMap.name = "CaracterState";
    }

    public void DestroyCaracterState()
    {
        if (GameObject.Find("CaracterState") != null)
        {
            Destroy(GameObject.Find("CaracterState"));
        }
    }
}

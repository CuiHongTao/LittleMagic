using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class CS_StatePanel : MonoBehaviour
{
    private string show = null;
    public float startY;
    public float endY;
    private bool showing = false;

    // Use this for initialization
    void Start()
    {
        foreach (Transform child in transform)
        {
            child.Translate(Vector3.right * Screen.width);
        }
        startY = transform.position.y;
        transform.Translate(Vector3.up * GetComponent<RectTransform>().rect.height);
        endY = transform.position.y;
    }

    // Update is called once per frame
    void Update()
    {
        if (StaticScript.lockMouse) return;
        if (showing)
        {
            if (Input.GetMouseButtonDown(0) && !EventSystem.current.IsPointerOverGameObject())
            {
                StartCoroutine(StartePanelOut());
            }
            if (Input.GetMouseButtonDown(1))
            {
                StartCoroutine(StartePanelOut());
            }
        }
    }

    public void ChangeShow(string onShow)
    {
        if (StaticScript.lockMouse)
        {
            return;
        }
        if (showing)
        {
            if (show == onShow)
            {
                StartCoroutine(StartePanelOut());
            }
            else
            {
                ChangeContent(onShow);
            }
        }
        else
        {
            StartCoroutine(StartePanelIn());
            ChangeContent(onShow);
        }
    }

    public void OverShow()
    {
        if (!showing) return;
        transform.position = new Vector3(transform.position.x, endY, transform.position.z);
        showing = false;
        GameObject.Find(show + "List").transform.Translate(Vector3.right * Screen.width);
        show = null;
    }

    IEnumerator StartePanelIn()
    {
        StaticScript.lockMouse = true;
        while (transform.position.y > startY + 1)
        {
            transform.Translate(Vector3.down * (endY - startY) / 30);
            yield return 0;
        }
        transform.position = new Vector3(transform.position.x, startY, transform.position.z);
        StaticScript.lockMouse = false;
        showing = true;
    }

    IEnumerator StartePanelOut()
    {
        StaticScript.lockMouse = true;
        while (transform.position.y < endY - 1)
        {
            transform.Translate(Vector3.up * (endY - startY) / 30);
            yield return 0;
        }
        transform.position = new Vector3(transform.position.x, endY, transform.position.z);
        StaticScript.lockMouse = false;
        showing = false;
        GameObject.Find(show + "List").transform.Translate(Vector3.right * Screen.width);
        show = null;
    }

    public void ChangeContent(string onShow)
    {
        if (show != null)
        {
            GameObject.Find(show + "List").transform.Translate(Vector3.right * Screen.width);
        }
        GameObject.Find(onShow + "List").transform.Translate(Vector3.left * Screen.width);
        show = onShow;
    }
}

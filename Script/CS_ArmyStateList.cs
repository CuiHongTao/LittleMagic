using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CS_ArmyStateList : MonoBehaviour
{
    private float parentHeight;
    private float localY;
    private float speed = 0;

    // Use this for initialization
    void Start()
    {
        localY = transform.localPosition.y;
        parentHeight = transform.parent.GetComponent<RectTransform>().rect.height;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetAxis("Mouse ScrollWheel") < 0)
        {
            transform.Translate(Vector3.up * transform.GetChild(0).GetComponent<RectTransform>().rect.height);
            if (speed == 0)
            {
                StartCoroutine(Move());
            }
        }
        if (Input.GetAxis("Mouse ScrollWheel") > 0)
        {
            transform.Translate(Vector3.down * transform.GetChild(0).GetComponent<RectTransform>().rect.height);
            if (speed == 0)
            {
                StartCoroutine(Move());
            }
        }
    }

    IEnumerator Move()
    {
        if (transform.childCount == 0) yield break;
        speed = 1;
        float childHeight = transform.childCount * transform.GetChild(0).GetComponent<RectTransform>().rect.height;

        if (childHeight < parentHeight)
        {
            float temp = Mathf.Lerp(transform.localPosition.y, localY, 0.05f) - transform.localPosition.y;
            do
            {
                transform.Translate(Vector3.up * temp);
                temp = Mathf.Lerp(transform.localPosition.y, localY, 0.05f) - transform.localPosition.y;
                yield return 0;
            } while (temp > 0.01f || temp < -0.01f);
            transform.localPosition = new Vector3(transform.localPosition.x, localY, transform.localPosition.z);
        }
        else if (transform.localPosition.y < localY)
        {
            float temp = Mathf.Lerp(transform.localPosition.y, localY, 0.05f) - transform.localPosition.y;
            do
            {
                transform.Translate(Vector3.up * temp);
                temp = Mathf.Lerp(transform.localPosition.y, localY, 0.05f) - transform.localPosition.y;
                yield return 0;
            } while (temp > 0.01f);
            if (temp > 0)
            {
                transform.localPosition = new Vector3(transform.localPosition.x, localY, transform.localPosition.z);
            }
        }
        else if (transform.localPosition.y - localY > childHeight - parentHeight)
        {
            float temp = Mathf.Lerp(transform.localPosition.y, localY + childHeight - parentHeight, 0.05f) - transform.localPosition.y;
            do
            {
                transform.Translate(Vector3.up * temp);
                temp = Mathf.Lerp(transform.localPosition.y, localY + childHeight - parentHeight, 0.05f) - transform.localPosition.y;
                yield return 0;
            } while (temp < -0.01f);
            if (temp < 0)
            {
                transform.localPosition = new Vector3(transform.localPosition.x, localY + childHeight - parentHeight, transform.localPosition.z);
            }
        }
        speed = 0;
    }
}
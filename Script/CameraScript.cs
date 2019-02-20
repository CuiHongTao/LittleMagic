using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class CameraScript : MonoBehaviour
{
    private bool cameraLock = false;
    public int moveSpeed = 15;
    private static int sizeW = 16;
    private static int sizeH = 10;

    // Use this for initialization
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        if (!cameraLock)
        {
            
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                Application.Quit();
            }
            if (Input.GetKeyDown(KeyCode.M))
            {
                if (MapScript.mapOn)
                {
                    GameObject.Find("Map").GetComponent<MapManagerScript>().SetMapOn(false);
                }
                else
                {
                    GameObject.Find("Map").GetComponent<MapManagerScript>().SetMapOn(true);
                }
            }
            if (Input.mousePosition.x > Screen.width - 5)
            {
                if (transform.position.x < (sizeW / 2))
                    transform.Translate(-Vector3.left * Time.deltaTime * moveSpeed, Space.World);
            }
            if (Input.mousePosition.x < 5)
            {
                if (transform.position.x > -(sizeW / 2))
                    transform.Translate(Vector3.left * Time.deltaTime * moveSpeed, Space.World);
            }
            if (Input.mousePosition.y > Screen.height - 5)
            {
                if (transform.position.z < (sizeH / 2))
                    transform.Translate(Vector3.forward * Time.deltaTime * moveSpeed, Space.World);
            }
            if (Input.mousePosition.y < 5)
            {
                if (transform.position.z > -(sizeH / 2))
                    transform.Translate(-Vector3.forward * Time.deltaTime * moveSpeed, Space.World);
            }
            if (EventSystem.current.IsPointerOverGameObject()) return;
            if (Input.GetAxis("Mouse ScrollWheel") > 0)
            {
                if (transform.position.y < 5)
                {
                    transform.Translate(Vector3.up * Time.deltaTime * 2 * moveSpeed, Space.World);
                    transform.Rotate(-Vector3.left * Time.deltaTime * 33.333f * moveSpeed, Space.World);
                }
                if (transform.position.y > 5)
                {
                    transform.rotation = Quaternion.Euler(90, 0, 0);
                    transform.position = new Vector3(transform.position.x, 5, transform.position.z);
                }
            }
            if (Input.GetAxis("Mouse ScrollWheel") < 0)
            {
                if (transform.position.y > 2)
                {
                    transform.Translate(-Vector3.up * Time.deltaTime * 2 * moveSpeed, Space.World);
                    transform.Rotate(Vector3.left * Time.deltaTime * 33.333f * moveSpeed, Space.World);
                }
                if (transform.position.y < 2)
                {
                    transform.rotation = Quaternion.Euler(40, 0, 0);
                    transform.position = new Vector3(transform.position.x, 2, transform.position.z);
                }
            }
        }
    }

    public void MoveTo(GameObject go)
    {
        float x = transform.position.x - go.transform.position.x;
        float z = transform.position.z - go.transform.position.z;
        StartCoroutine(Move(x / 20, z / 20));
    }

    IEnumerator Move(float preX, float preZ)
    {
        int i = 20;
        cameraLock = true;
        while (i > 0)
        {
            transform.position = new Vector3(transform.position.x - preX, transform.position.y, transform.position.z - preZ);
            i--;
            yield return 0;
        }
        cameraLock = false;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveScript : MonoBehaviour
{


    public float jumpVar;
    public float moveVar;
    public Transform another;
    private Rigidbody m_rigidbody;
    private bool jumping = true;
    private int jumptime = 0;
    private char beforJump = ' ';
    private bool beforJumpW = false;
    private bool beforJumpA = false;
    private bool beforJumpS = false;
    private bool beforJumpD = false;
    private bool fallendown = true;

    // Use this for initialization
    void Start()
    {
        m_rigidbody = gameObject.GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.W))
        {
            if (!jumping)
            {
                this.gameObject.transform.Translate(Vector3.forward * moveVar * Time.deltaTime, Space.World);
                transform.LookAt(new Vector3(another.position.x, another.position.y, another.position.z));
            }
            else
                this.gameObject.transform.Translate(Vector3.forward * moveVar * Time.deltaTime * 0.5f, Space.World);
        }
        if (Input.GetKey(KeyCode.S))
        {
            if (!jumping)
            {
                transform.LookAt(new Vector3(another.position.x, another.position.y, another.position.z));
                this.gameObject.transform.Translate(-Vector3.forward * moveVar * Time.deltaTime, Space.World);
            }
            else
                this.gameObject.transform.Translate(-Vector3.forward * moveVar * Time.deltaTime * 0.5f, Space.World);

        }
        if (Input.GetKey(KeyCode.A))
        {
            if (!jumping)
            {
                transform.LookAt(new Vector3(another.position.x, another.position.y, another.position.z));
                this.gameObject.transform.Translate(Vector3.left * moveVar * Time.deltaTime, Space.World);
            }
            else
                this.gameObject.transform.Translate(Vector3.left * moveVar * Time.deltaTime * 0.5f, Space.World);
        }
        if (Input.GetKey(KeyCode.D))
        {
            if (!jumping)
            {
                transform.LookAt(new Vector3(another.position.x, another.position.y, another.position.z));
                this.gameObject.transform.Translate(-Vector3.left * moveVar * Time.deltaTime, Space.World);
            }
            else
                this.gameObject.transform.Translate(-Vector3.left * moveVar * Time.deltaTime * 0.5f, Space.World);
        }
        if (!jumping)
        {
            if (Input.GetKey(KeyCode.Space) & !fallendown)
            {
                jumping = true;
                m_rigidbody.velocity = new Vector3(0, 3 * jumpVar, 0);
                beforJumpW = false;
                beforJumpA = false;
                beforJumpS = false;
                beforJumpD = false;
                if (Input.GetKey(KeyCode.W))
                {
                    beforJumpW = true;
                }
                if (Input.GetKey(KeyCode.A))
                {
                    beforJumpA = true;
                }
                if (Input.GetKey(KeyCode.S))
                {
                    beforJumpS = true;
                }
                if (Input.GetKey(KeyCode.D))
                {
                    beforJumpD = true;
                }
            }
        }
        if (jumping)
        {
            if (Input.GetKey(KeyCode.Space))
            {
                jumptime++;
                if (jumptime > 5 & !fallendown)
                {
                    fallendown = true;
                    m_rigidbody.velocity = new Vector3(0, 5 * jumpVar, 0);
                    jumptime = 0;
                }
            }
            else
            {
                fallendown = true;
            }
            if (beforJumpA) transform.Translate(Vector3.left * moveVar * Time.deltaTime * 0.5f, Space.World);
            if (beforJumpW) transform.Translate(Vector3.forward * moveVar * Time.deltaTime * 0.5f, Space.World);
            if (beforJumpS) transform.Translate(-Vector3.forward * moveVar * Time.deltaTime * 0.5f, Space.World);
            if (beforJumpD) transform.Translate(-Vector3.left * moveVar * Time.deltaTime * 0.5f, Space.World);
        }
        if (transform.position.y < 0.26 & jumping == true)
        {
            fallendown = false;
            jumping = false;
            jumptime = 0;
            transform.LookAt(another);
        }
        Debug.Log("fallendown=" + fallendown + "jumping=" + jumping);
        //if (fallendown)
        //{
        //    this.gameObject.transform.Translate(Vector3.down * moveVar * Time.deltaTime * (1/transform.position.y), Space.World);
        //}
    }

    public void FallenDownChanged()
    {
        fallendown = true;
    }
}

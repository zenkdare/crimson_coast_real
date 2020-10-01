using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ship_Movement : MonoBehaviour
{
    public GameObject manager;
    public float speed;
    public float maxSpeed;
    public float acceleration;
    public float deceleration;
    public float rotateSpeed;
    public GameObject dock_Button;
    public GameObject sail_On_Button;
    public GameObject port_Text;
    private bool makingport;
    private GameObject currentPort;
    // Start is called before the first frame update
    Rigidbody rb;
    void Start()
    {
        makingport = false;
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        /*
        if ((Input.GetKey("s")) && (speed < maxSpeed))
        {
            speed -= (acceleration * Time.deltaTime);
        }
        else if ((Input.GetKey("w")) && (speed > -maxSpeed))
        {
            speed += (acceleration * Time.deltaTime);
        }
        else
        {
            if (speed > (deceleration * Time.deltaTime))
            {
                speed -= (deceleration * Time.deltaTime);
            }
            else if (speed < (-deceleration * Time.deltaTime))
            {
                speed += (deceleration * Time.deltaTime);
            }
            else
            {
                speed = 0;
            }

        }
        transform.position +=  transform.forward*speed * Time.deltaTime;
        transform.Rotate((transform.up * Input.GetAxis("Horizontal")) * rotateSpeed * Time.fixedDeltaTime);
        */
    }
    private void FixedUpdate()
    {
        if (!makingport)
        {
            Vector3 velocity = (transform.right * Input.GetAxis("Vertical")*-1) * speed * Time.fixedDeltaTime;
            velocity.y = rb.velocity.y;
            rb.velocity = velocity;
            if (rb.velocity.x != 0 && rb.velocity.z != 0)
            {
                transform.Rotate((transform.up * Input.GetAxis("Horizontal")) * rotateSpeed * Time.fixedDeltaTime);
            }
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag=="town" || collision.gameObject.tag == "island")
        {
            port_Text.SetActive(true);
            dock_Button.SetActive(true);
            sail_On_Button.SetActive(true);
            makingport = true;
            currentPort = collision.gameObject;
        }
    }
    public void dock()
    {
        makingport = false;
        port_Text.SetActive(false);
        dock_Button.SetActive(false);
        sail_On_Button.SetActive(false);
        ManagerScript mScript = manager.GetComponent<ManagerScript>();
        mScript.In_To_Port(currentPort);
    }
    public void sailOn()
    {
        makingport = false;
        port_Text.SetActive(false);
        dock_Button.SetActive(false);
        sail_On_Button.SetActive(false);
    }
}

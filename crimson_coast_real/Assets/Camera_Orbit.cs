using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UIElements;

public class Camera_Orbit : MonoBehaviour
{
    public float zoomspeed;
    public float lookSpeed;
    public float turnSpeed = 4.0f;
    public GameObject ship;
    public GameObject manager;
    private Vector3 offset;
    bool inPosition;
    Vector3 relPos;
    Quaternion newRot;
    public NavMeshAgent b_agent;
    // Start is called before the first frame update
    void Start()
    {
        offset = ship.transform.position - ship.transform.GetChild(0).transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void LateUpdate()
    {
        if (!inPosition)
        {
            b_agent.isStopped = true;
            if (Vector3.Distance(transform.position, ship.transform.GetChild(0).transform.position)>5.0)
            {
                transform.position = Vector3.Lerp(transform.position, ship.transform.GetChild(0).transform.position, zoomspeed * Time.deltaTime);
            }
            if(Vector3.Distance(transform.position, ship.transform.GetChild(0).transform.position) <= 5.0)
            {
                transform.position = Vector3.MoveTowards(transform.position, ship.transform.GetChild(0).transform.position, lookSpeed * Time.deltaTime);
            }
            
            if (transform.position==ship.transform.GetChild(0).transform.position)
            {
                inPosition = true;
                Ship_Movement shipscript = ship.GetComponent<Ship_Movement>();
                shipscript.enabled = true;
                ManagerScript manage = manager.GetComponent<ManagerScript>();
                if (!manage.in_week_report)
                {
                    b_agent.isStopped = false;
                }
               
            }
        }
        if (inPosition)
        {
            offset = Quaternion.AngleAxis(Input.GetAxis("Mouse X") * turnSpeed, Vector3.up) * offset;
            transform.position = ship.transform.position - offset;
        }
        //relPos = ship.transform.position - transform.position;
        //newRot = Quaternion.LookRotation(relPos);
        //transform.rotation = Quaternion.RotateTowards(transform.rotation, newRot, Time.time * lookSpeed);
        Vector3 pos = ship.transform.GetChild(1).transform.position;
        transform.LookAt(pos);
    }

    private void OnEnable()
    {
        inPosition = false;
    }
}

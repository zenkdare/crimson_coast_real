using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Ship_Movement : MonoBehaviour
{
    public GameObject manager;
    public GameObject dock_Button;
    public GameObject sail_On_Button;
    public GameObject port_Text;
    public bool inport;
    private GameObject targetPort;
    private NavMeshAgent agent;
    // Start is called before the first frame update
    Rigidbody rb;
    void Start()
    {
        inport = true;
        rb = GetComponent<Rigidbody>();
        agent = GetComponent<NavMeshAgent>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!inport)
        {
            if (!(agent.pathPending))
            {
                if (agent.pathStatus == NavMeshPathStatus.PathComplete && agent.remainingDistance == 0)
                {
                    dock();
                }
            }
        }

    }
    public void set_Port(GameObject port)
    {
        targetPort = port;
    }
    public void dock()
    {
        ManagerScript mScript = manager.GetComponent<ManagerScript>();
        mScript.In_To_Port(targetPort);
    }
}

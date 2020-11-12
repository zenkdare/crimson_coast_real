using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

public class Ship_Movement : MonoBehaviour
{
    public GameObject manager;
    //public GameObject dock_Button;
    //public GameObject sail_On_Button;
    //public GameObject port_Text;
    public bool inport;
    private GameObject targetPort;
    private NavMeshAgent agent;
    // Start is called before the first frame update
    Rigidbody rb;
    public List<Crew> ship_crew = new List<Crew>();
    public List<Event> events = new List<Event>();
    private float halfway=0;
    public GameObject local_event;
    public Text event_name;
    public Text evnet_flavor;
    public Text decision1;
    public Text decision2;
    public string current_event;
    void Start()
    {
        inport = true;
        rb = GetComponent<Rigidbody>();
        agent = GetComponent<NavMeshAgent>();
        events.Add(new Event("Storm", "", "", "", ""));
        events.Add(new Event("Theft", "", "", "", ""));
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
                    trigger_event(1);
                    //dock();
                }
                else if(Vector3.Distance(gameObject.transform.position, agent.destination) == halfway)
                {
                    
                    print(agent.remainingDistance);
                    trigger_event(0);
                    agent.isStopped = true;
                }
            }
        }
        else
        {
            if (!(agent.pathPending))
            {
                halfway = Vector3.Distance(gameObject.transform.position, agent.destination)/2;
            }
        }

    }
    public void set_halfway()
    {
        if (halfway == 0)
        {
            
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
    public int get_crew_count()
    {
        return ship_crew.Count;
    }
    public void add_crew(Crew new_hire)
    {
        ship_crew.Add(new_hire);
    }
    public void trigger_event(int num)
    {
        local_event.SetActive(true);
        event_name.text = events[num].get_name();
        evnet_flavor.text = events[num].get_flavor();
        decision1.text = events[num].get_o1();
        decision2.text = events[num].get_o2();
        current_event = events[num].get_name();
    }
    public void makedecision(int num)
    {
        ManagerScript manage = manager.GetComponent<ManagerScript>();
        manage.handle_event(current_event, num);
        local_event.SetActive(false);
        agent.isStopped = false;
    }
}

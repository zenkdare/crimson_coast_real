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
    public GameObject current_port;
    private NavMeshAgent agent;
    // Start is called before the first frame update
    Rigidbody rb;
    public List<Crew> ship_crew = new List<Crew>();
    public List<Event> events = new List<Event>();
    private float halfway=0;
    //public GameObject local_event;
    //public Text event_name;
    //public Text evnet_flavor;
    //public Text decision1;
    //public Text decision2;
    public string current_event;
    public int weekdis;
    public Vector3 lastpos;
    public GameObject canvas;
    public UIManager uiScript;
    void Start()
    {
        inport = true;
        rb = GetComponent<Rigidbody>();
        agent = GetComponent<NavMeshAgent>();
        events.Add(new Event("Storm", "", "", "", ""));
        events.Add(new Event("Theft", "Stop the theft, for a watch dog is in your service", "accept the loss, whoever the theif is, they were too sneaky to catch", "Rumors abound of cargo going missing. You head into the lower decks to see if your crew tell the truth", ""));
    }

    // Update is called once per frame
    void Update()
    {
        if (!inport)
        {
            if (!(agent.pathPending))
            {
                //print(Vector3.Distance(gameObject.transform.position, lastpos));
                if (agent.pathStatus == NavMeshPathStatus.PathComplete && agent.remainingDistance == 0)
                {
                    dock();
                }
                else if(Vector3.Distance(gameObject.transform.position, lastpos) >= weekdis)
                {
                    //print(Vector3.Distance(gameObject.transform.position, lastpos));
                    trigger_event(0);
                    events.RemoveAt(0);
                    agent.isStopped = true;
                    lastpos = gameObject.transform.position;
                }
            }
        }
        else
        {
            if (!(agent.pathPending))
            {
                lastpos = gameObject.transform.position;
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
        current_port = targetPort;
    }
    public int get_crew_count()
    {
        return ship_crew.Count;
    }
    public void add_crew(Crew new_hire)
    {
        /*
        ship_crew.Add(new_hire);
        if(String.Equals(new_hire.get_t1(), "Silver Tongue"))
        {
            townscript = current_port.GetComponent<Town>();
            
        }
        */
    }
    public void trigger_event(int num)
    {
        // local_event.SetActive(true);
        // event_name.text = events[num].get_name();
        // evnet_flavor.text = events[num].get_flavor();
        // decision1.text = events[num].get_o1();
        // decision2.text = events[num].get_o2();
        // current_event = events[num].get_name();
        uiScript.EventUI(true);
        uiScript.updateEvent(events[num].get_name(), events[num].get_flavor(), events[num].get_o1(), events[num].get_o2(), "option1 description", "option 2 description");

    }
    public void makedecision(int num)
    {
        ManagerScript manage = manager.GetComponent<ManagerScript>();
        manage.handle_event(current_event, num);
        //local_event.SetActive(false);
        uiScript.EventUI(false);
        uiScript.eventResult("Description of what happened");
        uiScript.EventResultUI(true);

        //agent.isStopped = false;
    }
    public void continueJourney(){
        agent.isStopped = false;
    }
}

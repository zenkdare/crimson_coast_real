using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;
using System.IO;

public class Ship_Movement : MonoBehaviour
{
    public GameObject manager;
    public bool inport;
    private GameObject targetPort;
    public GameObject current_port;
    private NavMeshAgent agent;
    Rigidbody rb;
    public List<Crew> ship_crew = new List<Crew>();
    public List<Event> events = new List<Event>();
    public List<Event> possible_events = new List<Event>();
    private float halfway=0;
    public Event current_event;
    public int weekdis;
    public Vector3 lastpos;
    public GameObject canvas;
    public UIManager uiScript;
    private int has_wind_reader;
    private int has_watch_dog;
    private int has_peacemaker;
    private int num_lucky;
    private int has_doctor;
    private int has_bosun;
    private int has_coneccted;
    private int num_silver_tongue;
    public float event_rate;
    public float watchdog_success_chance;
    public float reader_success_chance;
    public float peace_success_chance;
    void Start()
    {
        has_wind_reader=0;
        has_watch_dog=0;
        has_peacemaker=0;
        num_lucky = 0; 
        has_doctor = 0;
        has_bosun = 0;
        has_coneccted = 0;
        num_silver_tongue=0;
        inport = true;
        rb = GetComponent<Rigidbody>();
        agent = GetComponent<NavMeshAgent>();
        StreamReader sr = new StreamReader("Assets/Event_info.txt");
        string all_event_info;
        all_event_info = sr.ReadToEnd();
        sr.Close();
        string[] text_events = all_event_info.Split(';');
        for(int i = 0; i < text_events.Length; i++)
        {
            events.Add(new Event(text_events[i]));
        }
        possible_events.Add(events[1]);
        possible_events.Add(events[2]);
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
                    if (Random.value <= event_rate)
                    {
                        int event_num = Random.Range(0, possible_events.Count);
                        current_event = possible_events[event_num];
                        trigger_event(possible_events[event_num]);
                    }
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
        ship_crew.Add(new_hire);
        if(new_hire.get_t1().Equals("Wind Reader"))
        {
            has_wind_reader++;
        }
        if (new_hire.get_t1().Equals("Watch Dog"))
        {
            has_watch_dog++;
        }
        if (new_hire.get_t1().Equals("Peacemaker"))
        {
            has_peacemaker ++;
        }
        if (new_hire.get_t1().Equals("Lucky"))
        {
            num_lucky++;
        }
        if (new_hire.get_t1().Equals("Doctor"))
        {
            has_doctor ++;
        }
        if (new_hire.get_t1().Equals("Bosun"))
        {
            has_bosun ++;
        }
        if (new_hire.get_t1().Equals("Silver Tongue"))
        {
            num_silver_tongue++; ;
        }
        if (new_hire.get_t1().Equals("Connected"))
        {
            has_coneccted ++;
        }
        print(has_wind_reader + " , "+ has_watch_dog + " , "+ has_peacemaker+" , "+ num_lucky+" , "+ num_lucky+","+ has_doctor+","+ has_bosun+","+ num_silver_tongue+","+has_coneccted);
    }
    public void fire_crew(int num)
    {
        Crew newly_fired = ship_crew[num];
        if (newly_fired.get_t1().Equals("Wind Reader"))
        {
            has_wind_reader--;
        }
        if (newly_fired.get_t1().Equals("Watch Dog"))
        {
            has_watch_dog--;
        }
        if (newly_fired.get_t1().Equals("Peacemaker"))
        {
            has_peacemaker--;
        }
        if (newly_fired.get_t1().Equals("Lucky"))
        {
            num_lucky--;
        }
        if (newly_fired.get_t1().Equals("Doctor"))
        {
            has_doctor--;
        }
        if (newly_fired.get_t1().Equals("Bosun"))
        {
            has_bosun--;
        }
        if (newly_fired.get_t1().Equals("Silver Tongue"))
        {
            num_silver_tongue--; ;
        }
        if (newly_fired.get_t1().Equals("Connected"))
        {
            has_coneccted--;
        }
        print(has_wind_reader + " , " + has_watch_dog + " , " + has_peacemaker + " , " + num_lucky + " , " + num_lucky + "," + has_doctor + "," + has_bosun + "," + num_silver_tongue + "," + has_coneccted);

    }
    public void trigger_event(Event e)
    {
        // local_event.SetActive(true);
        // event_name.text = events[num].get_name();
        // evnet_flavor.text = events[num].get_flavor();
        // decision1.text = events[num].get_o1();
        // decision2.text = events[num].get_o2();
        // current_event = events[num].get_name();
        uiScript.EventUI(true);
        uiScript.updateEvent(e.get_name(), e.get_flavor(), e.get_o1(), e.get_o2(),e.get_o1_descrip(), e.get_o2_descrip());

    }
    public void makedecision(int num)
    {
        //local_event.SetActive(false);
        string result="hmmmm, not suppossed to happen";
        int num_result = -1;
        if (current_event.is_active())
        {
            float odds = Random.value;
            if(current_event.get_name().Equals("A Theif in the Night"))
            {
                if (num == 0)
                {
                    if (has_watch_dog > 0 || odds <= watchdog_success_chance)
                    {
                        result = current_event.get_good_result();
                        num_result = 0;
                    }
                    else
                    {
                        result = current_event.get_failed_result();
                        num_result = 1;
                    }
                }
                else
                {
                    result = current_event.get_bad_result();
                    num_result = 2;
                }   
            }
            if (current_event.get_name().Equals("Brewing Storm"))
            {
                if (num == 0)
                {
                    if (has_wind_reader > 0 || odds <= reader_success_chance)
                    {
                        result = current_event.get_good_result();
                        num_result = 0;
                    }
                    else
                    {
                        result = current_event.get_failed_result();
                        num_result = 1;
                    }
                }
                else
                {
                    result = current_event.get_bad_result();
                    num_result = 2;
                }
            }
            if (current_event.get_name().Equals("On Deck Brawl"))
            {
                if (num == 0)
                {
                    if (has_peacemaker > 0 || odds <= peace_success_chance)
                    {
                        result = current_event.get_good_result();
                        num_result = 0;
                    }
                    else
                    {
                        result = current_event.get_failed_result();
                        num_result = 1;
                    }
                }
                else
                {
                    result = current_event.get_bad_result();
                    num_result = 2;
                }
            }

        }
        uiScript.EventUI(false);
        uiScript.eventResult(result);
        uiScript.EventResultUI(true);
        ManagerScript manage = manager.GetComponent<ManagerScript>();
        manage.handle_event(current_event, num_result);
        //agent.isStopped = false;
    }
    public void continueJourney(){
        agent.isStopped = false;
    }
}

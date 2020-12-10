using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;
using System.IO;

public class Ship_Movement : MonoBehaviour
{
    public GameObject cam;
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
    private int no_rations_num;
    private bool using_spice;
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
        no_rations_num = 0;
        inport = true;
        using_spice = false;
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
        possible_events.Add(events[8]);
        possible_events.Add(events[7]);
        possible_events.Add(events[1]);
        possible_events.Add(events[2]);
        possible_events.Add(events[3]);
        possible_events.Add(events[4]);
        possible_events.Add(events[5]);
    }

    // Update is called once per frame
    void Update()
    {
        if (!inport)
        {
            if (!(agent.pathPending))
            {
                ManagerScript mScript = manager.GetComponent<ManagerScript>();
                //print(Vector3.Distance(gameObject.transform.position, lastpos));
                if (agent.pathStatus == NavMeshPathStatus.PathComplete && agent.remainingDistance == 0)
                {
                    dock();
                    for(int i = 0; i < ship_crew.Count; i++)
                    {
                        if (ship_crew[i].getLoyalty() <= 1)
                        {
                            mScript.fire_crew(i);
                            i--;
                        }
                    }
                }
                else if(Vector3.Distance(gameObject.transform.position, lastpos) >= weekdis)
                {
                    //print(Vector3.Distance(gameObject.transform.position, lastpos));
                    if (Random.value <= event_rate)
                    {
                        List<Crew> poss_traitor = new List<Crew>();
                        List<Crew> poss_raise = new List<Crew>();
                        bool possible_plot=false;
                        bool possible_plot_catch=false;
                        for(int i = 0; i < ship_crew.Count; i++)
                        {
                            if (ship_crew[i].getLoyalty()==0)
                            {
                                possible_plot = true;
                                poss_traitor.Add(ship_crew[i]);
                            }
                            if (ship_crew[i].getLoyalty() >= 8)
                            {
                                possible_plot_catch = true;
                            }
                        }
                        if(possible_plot_catch && possible_plot)
                        {
                            int half_n_half=Random.Range(0, 2);
                            if (half_n_half == 0)
                            {
                                int get_traitor = Random.Range(0, poss_traitor.Count);
                                possible_events[0].change_trigger(ship_crew.IndexOf(poss_traitor[get_traitor]));
                                current_event = possible_events[0];
                                trigger_event(possible_events[0]);
                            }
                        }
                        else
                        {
                            int event_num;
                            event_num = Random.Range(1, possible_events.Count);
                            if (event_num == 1)
                            {
                                int get_raiser = Random.Range(0, ship_crew.Count);
                                possible_events[1].change_trigger(get_raiser);
                            }
                            current_event = possible_events[event_num];
                            trigger_event(possible_events[event_num]);
                        }
                    }
                    else{
                        mScript.weekReport();
                    }
                    agent.isStopped = true;
                    agent.velocity = Vector3.zero;
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
        ship_crew.Add(new_hire);
        if(new_hire.get_t1().Equals("Wind Reader"))
        {
            print("Wind work");
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
        if(new_hire.get_t1().Equals("Sharp Eyes"))
        {
            possible_events.Add(events[6]);
            possible_events.Add(events[6]);
        }
        if (new_hire.get_t2().Equals("Thief"))
        {
            possible_events.Add(events[2]);
        }
        if (new_hire.get_t2().Equals("Violent"))
        {
            possible_events.Add(events[4]);
        }
        if (new_hire.get_t2().Equals("Sickly"))
        {
            possible_events.Add(events[6]);
        }
        if (new_hire.get_t2().Equals("Clumsy"))
        {
            possible_events.Add(events[8]);
        }
        if (new_hire.get_t2().Equals("Unlucky"))
        {
            possible_events.Add(events[3]);
        }
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
        if (newly_fired.get_t1().Equals("Sharp Eyes"))
        {
           for(int i=0; i < possible_events.Count; i++)
           {
                if(possible_events[i].get_id()==3)
                {
                    possible_events.RemoveAt(i);
                }
           }
            for (int i = 0; i < possible_events.Count; i++)
            {
                if (possible_events[i].get_id() == 3)
                {
                    possible_events.RemoveAt(i);
                }
            }
        }
        print(possible_events.Count);
        if (newly_fired.get_t2().Equals("Thief"))
        {
            possible_events.Remove(events[0]);
        }
        
        if (newly_fired.get_t2().Equals("Violent"))
        {
            possible_events.Remove(events[2]);
        }
        if (newly_fired.get_t2().Equals("Sickly"))
        {
            possible_events.Remove(events[4]);
        }
        if (newly_fired.get_t2().Equals("Clumsy"))
        {
            possible_events.Remove(events[6]);
        }
        if (newly_fired.get_t2().Equals("Unlucky"))
        {
            possible_events.Remove(events[1]);
        }
        //print(possible_events.Count);
        ship_crew.RemoveAt(num);


    }
    public void trigger_event(Event e)
    {
        Camera_Orbit camorbit = cam.GetComponent<Camera_Orbit>();
        camorbit.enabled = false;
        print(e.get_name());
        // local_event.SetActive(true);
        // event_name.text = events[num].get_name();
        // evnet_flavor.text = events[num].get_flavor();
        // decision1.text = events[num].get_o1();
        // decision2.text = events[num].get_o2();
        // current_event = events[num].get_name();
        if (e.is_active())
        {
            //print("active");
            if(e.get_id()==7 || e.get_id() == 8)
            {
                uiScript.EventUI(true);
                uiScript.updateEvent(e.get_name(), ship_crew[e.get_trigger()].get_name()+" "+e.get_flavor(), e.get_o1(), e.get_o2(), e.get_o1_descrip(), e.get_o2_descrip());
            }
            else
            {
                uiScript.EventUI(true);
                uiScript.updateEvent(e.get_name(), e.get_flavor(), e.get_o1(), e.get_o2(), e.get_o1_descrip(), e.get_o2_descrip());
            }
        }
        else
        {
            //print("not active");
            string result = "something should be here";
            int num_result = -1;
            if (e.get_id() == 3)
            {
                //print("made it float");
                if (num_lucky > 0)
                {
                    result = e.get_flavor()+"\n"+e.get_good_result();
                }
                else
                {
                    result = e.get_flavor() + "\n" + e.get_failed_result();
                }
            }
            if (e.get_id() == 4)
            {
                //print("made it sick");
                if (has_doctor > 0)
                {
                    result = e.get_flavor() + "\n" + e.get_good_result();
                    num_result = 0;
                }
                else
                {
                    result = e.get_flavor() + "\n" + e.get_failed_result();
                    num_result = 1;
                }
            }
            if (e.get_id() == 5)
            {
                //print("made it rough");
                if (has_bosun > 0)
                {
                    result = e.get_flavor() + "\n" + e.get_good_result();
                    num_result = 0;
                }
                else
                {
                    result = e.get_flavor() + "\n" + e.get_failed_result();
                    num_result = 1;
                }
            }
            if (e.get_id() == 6)
            {
                //print("made it acc");
                result = e.get_flavor() + "\n" + e.get_good_result();
            }
            
            uiScript.updateEvent(e.get_name(), e.get_flavor(), e.get_o1(), e.get_o2(), e.get_o1_descrip(), e.get_o2_descrip());
            uiScript.EventResultUI(true);
            uiScript.eventResult(result, e.get_name());
            ManagerScript manage = manager.GetComponent<ManagerScript>();
            manage.handle_event(current_event, num_result);
        }

    }
    public void makedecision(int num)
    {
        //local_event.SetActive(false);
        string result="hmmmm, not suppossed to happen";
        int num_result = -1;
        ManagerScript manage = manager.GetComponent<ManagerScript>();
        if (current_event.is_active())
        {
            float odds = Random.value;
            if(current_event.get_id() == 0)
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
            if (current_event.get_id() == 1)
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
                    for(int i = 0; i < ship_crew.Count; i++)
                    {
                        ship_crew[i].change_loyalty(-1);
                    }
                    uiScript.update_ship_crew();
                }
            }
            if (current_event.get_id() == 2)
            {
                if (num == 0)
                {
                    if (has_peacemaker > 0 || odds <= peace_success_chance)
                    {
                        result = current_event.get_good_result();
                        num_result = 0;
                        for (int i = 0; i < ship_crew.Count; i++)
                        {
                            ship_crew[i].change_loyalty(+2);
                        }
                        uiScript.update_ship_crew();
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
            if (current_event.get_id() == 7)
            {
                print(num);
                if (num == 0)
                {
                    ship_crew[current_event.get_trigger()].change_wage(1);
                    ship_crew[current_event.get_trigger()].change_loyalty(2);
                    result = current_event.get_good_result();
                   
                }
                else
                {
                    ship_crew[current_event.get_trigger()].change_loyalty(-3);
                    result = current_event.get_bad_result();
                }
                uiScript.update_ship_crew();
            }
            if (current_event.get_id() == 8)
            {

                if (num == 0)
                {
                    for(int i = 0; i < ship_crew.Count; i++)
                    {
                        if (ship_crew[i].getLoyalty() <= 4)
                        {
                            ship_crew[i].change_loyalty(-1);
                        }
                    }
                    ship_crew[current_event.get_trigger()].set_loyalty(3);
                }
                else
                {
                    manage.fire_crew(current_event.get_trigger());
                    //ship_crew.RemoveAt(current_event.get_trigger());
                    for (int i = 0; i < ship_crew.Count; i++)
                    {
                        if (ship_crew[i].getLoyalty() <= 4)
                        {
                            ship_crew[i].change_loyalty(3);
                        }
                        if(ship_crew[i].getLoyalty() >= 7)
                        {
                            ship_crew[i].change_loyalty(-2);
                        }
                    }
                }
                uiScript.update_ship_crew();
            }
        }
        uiScript.EventUI(false);
        uiScript.eventResult(result, current_event.get_name());
        uiScript.EventResultUI(true);
        
        manage.handle_event(current_event, num_result);
        //agent.isStopped = false;
    }
    public void continueJourney(){
        ManagerScript manage = manager.GetComponent<ManagerScript>();
        manage.in_week_report = false;
        Camera_Orbit camorbit = cam.GetComponent<Camera_Orbit>();
        camorbit.enabled = true;
        agent.isStopped = false;
    }
    public void give_rum()
    {
        for(int i=0; i < ship_crew.Count; i++)
        {
            if (ship_crew[i].get_t1().Equals("Drunk"))
            {
                ship_crew[i].change_loyalty(4);
            }
            else if (ship_crew[i].get_t1().Equals("Teetotaler"))
            {
                //nothing happens
            }
            else
            {
                ship_crew[i].change_loyalty(2);
            }
            
        }
        uiScript.update_ship_crew();
    }
    public void extra_rations()
    {
        for (int i = 0; i < ship_crew.Count; i++)
        {
            if (ship_crew[i].get_t2() != "Glutton")
            {
                if (using_spice)
                {
                    ship_crew[i].change_loyalty(4);
                }
                ship_crew[i].change_loyalty(1);
            }
            else
            {
                if (using_spice)
                {
                    ship_crew[i].change_loyalty(3);
                }
            }

        }
        uiScript.update_ship_crew();
        if (using_spice)
        {
            ManagerScript man = manager.GetComponent<ManagerScript>();
            man.spend_spice();
            using_spice = false;
        }
        no_rations_num = 0;
    }
    public void normal_rations()
    {
        no_rations_num = 0;
        if (using_spice)
        {
            for (int i = 0; i < ship_crew.Count; i++)
            { 
                ship_crew[i].change_loyalty(3);
            }
            uiScript.update_ship_crew();
            ManagerScript man = manager.GetComponent<ManagerScript>();
            man.spend_spice();
            using_spice = false;
        }
        for (int i = 0; i < ship_crew.Count; i++)
        {
            if (ship_crew[i].get_t2().Equals("Glutton"))
            {
                ship_crew[i].change_loyalty(-1);
            }
        }
        uiScript.update_ship_crew();
    }
    public void no_rations()
    {
        for (int i = 0; i < ship_crew.Count; i++)
        {
            if(ship_crew[i].get_t2() != "Glutton")
            {
                ship_crew[i].change_loyalty(-1);
            }
            else
            {
                ship_crew[i].change_loyalty(-2);
            }
        }
        no_rations_num++;
        if (no_rations_num > 2)
        {
            //you should lose the game here
            //need to update the ui here
        }
        uiScript.update_ship_crew();
    }
    public void toggle_spice(bool boo)
    {
        using_spice = boo;
    }
    public int handle_mutiny_spread()
    {
        int mutany_count=0;
        for(int i = 0; i < ship_crew.Count; i++)
        {
            if (ship_crew[0].getLoyalty() == 0)
            {
                mutany_count++;
                int num = i;
                while (ship_crew[num].getLoyalty() == 0)
                {
                    num = Random.Range(0, ship_crew.Count);
                }
                ship_crew[i].change_loyalty(-2);
            }
        }
        uiScript.update_ship_crew();
        if (mutany_count >= ship_crew.Count / 2)
        {
            //you lose here
        }
        return mutany_count;
    }
    public int get_lucky_num()
    {
        return num_lucky;
    }
    public Crew get_crew_at_spot(int spot)
    {
        Crew temp = ship_crew[spot];
        return temp;
    }
    public void change_loyalty_all(int num)
    {
        for (int i = 0; i < ship_crew.Count; i++)
        {
            ship_crew[i].change_loyalty(num);
        }
        uiScript.update_ship_crew();
    }
    public int get_wages()
    {
        int num=0;
        for(int i = 0; i < ship_crew.Count; i++)
        {
            num += ship_crew[i].get_cost();
        }
        return num;
    }
    public bool is_connected()
    {
        if (has_coneccted > 0)
        {
            return true;
        }
        else
        {
            return false;
        }
    }
    public GameObject get_target_port()
    {
        return targetPort;
    }
}

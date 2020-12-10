using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Event
{
    private string id;
    private string name;
    private string option1;
    private string option2;
    private string option1_descrip;
    private string option2_descrip;
    private string flavor;
    private int trigger;
    private string good_result;
    private string failed_result;
    private string bad_result;
    // Start is called before the first frame update
    public Event(string info)
    {
        string[] parsed_info = info.Split('\n');
        if (parsed_info.Length == 11)
        {
            id = parsed_info[0];
            name = parsed_info[1];
            flavor = parsed_info[2];
            option1_descrip = parsed_info[3];
            option2_descrip = parsed_info[4];
            option1 = parsed_info[5];
            option2 = parsed_info[6];
            good_result = parsed_info[7];
            failed_result = parsed_info[8];
            bad_result = parsed_info[9];
            trigger = -1;
        }
        if (parsed_info.Length == 10)
        {
            id = parsed_info[0];
            name = parsed_info[1];
            flavor = parsed_info[2];
            option1_descrip = parsed_info[3];
            option2_descrip = parsed_info[4];
            option1 = parsed_info[5];
            option2 = parsed_info[6];
            good_result = parsed_info[7];
            failed_result = "";
            bad_result = parsed_info[8];
            trigger = -1;
        }
        if (parsed_info.Length <= 6)
        {
            id = parsed_info[0];
            name = parsed_info[1];
            flavor = parsed_info[2];
            good_result = parsed_info[4];
            failed_result = parsed_info[3];
            option1_descrip = "";
            option2_descrip = "";
            option1 = "";
            option2 = "";
            bad_result = "";
            trigger = -1;
        }
    }
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public int get_id()
    {
        return int.Parse(id);
    }
    public string get_name()
    {
        return name;
    }
    public string get_o1()
    {
        return option1;
    }
    public string get_o2()
    {
        return option2;
    }
    public string get_flavor()
    {
        return flavor;
    }
    public int get_trigger()
    {
        return trigger;
    }
    public string get_o1_descrip()
    {
        return option1_descrip;
    }
    public string get_o2_descrip()
    {
        return option2_descrip;
    }
    public string get_good_result()
    {
        return good_result;
    }
    public string get_failed_result()
    {
        return failed_result;
    }
    public string get_bad_result()
    {
        return bad_result;
    }
    public bool is_active()
    {
        if(int.Parse(id) == 0 || int.Parse(id) == 1 || int.Parse(id) == 2 || int.Parse(id) == 7 || int.Parse(id) == 8)
        {
            return true;
        }
        else
        {
            return false;
        }
    }
    public bool is_passive()
    {
        if (int.Parse(id) == 3 || int.Parse(id) == 4 || int.Parse(id) == 5 || int.Parse(id) == 6)
        {
            return true;
        }
        else
        {
            return false;
        }
    }
    public void change_trigger(int n)
    {
        trigger = n;
    }
    /*
    public override bool Equals(string s)
    {
        if (e.get_name().Equals(id))
        {
            return true;
        }
        else
        {
            return false;
        }
    }
    */
}

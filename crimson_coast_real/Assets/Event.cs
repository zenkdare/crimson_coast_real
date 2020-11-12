using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Event : MonoBehaviour
{
    private string id;
    private string option1;
    private string option2;
    private string flavor;
    private string trigger;
    // Start is called before the first frame update
    public Event(string n, string o1, string o2, string f, string t)
    {
        id = n;
        option1 = o1;
        option2 = o2;
        flavor = f;
        if (t != null)
        {
            trigger = t;
        }
    }
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public string get_name()
    {
        return id;
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
    public string get_trigger()
    {
        return trigger;
    }
}

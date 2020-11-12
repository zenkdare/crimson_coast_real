using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using UnityEngine;

public class Crew : MonoBehaviour
{
    public string crew_name;
    public int pay;
    public string trait1;
    public string trait2;
    // Start is called before the first frame update\
    public Crew(string n, int c, string t1, string t2)
    {
        crew_name = n;
        pay = c;
        trait1 = t1;
        trait2 = t2;
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
        return crew_name;
    }
    public int get_cost()
    {
        return pay;
    }
    public string get_t1()
    {
        return trait1;
    }
    public string get_t2()
    {
        return trait2;
    }
}

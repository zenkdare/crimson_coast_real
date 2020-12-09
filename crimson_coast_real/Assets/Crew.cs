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
    public string trait1Desc = "trait 1 description";
    public string trait2Desc = "trait 2 description";
    public int loyalty;
    // Start is called before the first frame update\
    public Crew(string n, int c, string t1, string t2, string t1d, string t2d, int loy)
    {
        crew_name = n;
        pay = c;
        trait1 = t1;
        trait2 = t2;
        trait1Desc = t1d;
        trait2Desc = t2d;
        loyalty = loy;
        if (t2.Equals("Greedy"))
        {
            pay += 3;
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
    public string get_t1Desc()
    {
        return trait1Desc;
    }
    public string get_t2Desc()
    {
        return trait2Desc;
    }
    public int getLoyalty()
    {
        return loyalty;
    }
    public void change_loyalty(int num)
    {
        
        if(trait2== "Guarded" && num>0)
        {
            loyalty += num / 2; 
        }
        else
        {
            loyalty += num;
        }
        if (loyalty < 0)
        {
            loyalty = 0;
        }
    }
    public void change_wage(int num)
    {
        pay += num;
    }
    public void set_loyalty(int num)
    {
        loyalty = num;
    }
}

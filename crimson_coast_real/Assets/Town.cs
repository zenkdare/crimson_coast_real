using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices.WindowsRuntime;
using UnityEngine;
using UnityEngine.UI;

public class Town : MonoBehaviour
{
    //public Text rum_stock_market;
    public int rum_price;
    public int rum_amount;
    public int spice_price;
    public int spice_amount;
    public int timber_price;
    public int timber_amount;
    public int med_price;
    public int med_amount;
    public int temp_amount;
    public List<Crew> local_crew = new List<Crew>();
    private int og_rum_amount;
    private int og_spice_amount;
    private int og_timber_amount;
    private int og_med_amount; 
    //public Text n;
    //public Text t1;
    //public Text t2;
    //public Text cost;
    public GameObject ship;
    //public GameObject crew1;
    public GameObject canvas;
    public UIManager uiScript;
    // Start is called before the first frame update
    void Start()
    {
        og_rum_amount = rum_amount;
        og_spice_amount = spice_amount;
        og_timber_amount = timber_amount;
        og_med_amount = med_amount;
        temp_amount = rum_amount;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public int get_buy_amount(string good)
    {
        if (good.Equals("rum"))
        {
            return rum_price;
        }
        if (good.Equals("spice"))
        {
            return spice_price;
        }
        if (good.Equals("timber"))
        {
            return timber_price;
        }
        if (good.Equals("med"))
        {
            return med_price;
        }
        else return -1;
    }
    public int get_sell_amount(string good)
    {
        if (good.Equals("rum"))
        {
            return (int)(rum_price - (rum_price * .20));
        }
        if (good.Equals("spice"))
        {
            return (int)(spice_price - (spice_price * .20));
        }
        if (good.Equals("timber"))
        {
            return (int)(timber_price - (timber_price * .20));
        }
        if (good.Equals("med"))
        {
            return (int)(med_price - (med_price * .20));
        }
        else
        {
            return -1;
        }
    }
    public int get_good_amount(string good)
    {
        if (good.Equals("rum"))
        {
            return rum_amount;
        }
        if (good.Equals("spice"))
        {
            return spice_amount;
        }
        if (good.Equals("timber"))
        {
            return timber_amount;
        }
        if (good.Equals("med"))
        {
            return med_amount;
        }
        else
        {
            return - 1;
        }
    }
    public void set_shop_stock()
    {
        //temp_amount = rum_amount;
        //rum_stock_market.text = (temp_amount.ToString());
        uiScript.updateMarket("Rum", "Stock", rum_amount);
        uiScript.updateMarket("Spice", "Stock", spice_amount);
        uiScript.updateMarket("Timber", "Stock", timber_amount);
        uiScript.updateMarket("Medicine", "Stock", med_amount);
    }
    public void alter_shop_stock(int diff, string good)
    {
        if (good.Equals("rum"))
        {
            rum_amount += diff;
            uiScript.updateMarket("Rum", "Stock", rum_amount);
        }
        if (good.Equals("spice"))
        {
            spice_amount += diff;
            uiScript.updateMarket("Spice", "Stock", spice_amount);
        }
        if (good.Equals("timber"))
        {
            timber_amount += diff;
            uiScript.updateMarket("Timber", "Stock", timber_amount);
        }
        if (good.Equals("med"))
        {
            med_amount += diff;
            uiScript.updateMarket("Medicine", "Stock", med_amount);
        }

        //rum_stock_market.text = (temp_amount.ToString());

    }
    public void set_up_tavern(List<string> names, List<string> t1_lis, List<string> t2_lis, List<string> t1_d, List<string> t2_d)
    {
        local_crew.Add(generate_crew(names, t1_lis, t2_lis, t1_d, t2_d));
        local_crew.Add(generate_crew(names, t1_lis, t2_lis, t1_d, t2_d));
        local_crew.Add(generate_crew(names, t1_lis, t2_lis, t1_d, t2_d));
        local_crew.Add(generate_crew(names, t1_lis, t2_lis, t1_d, t2_d));
        local_crew.Add(generate_crew(names, t1_lis, t2_lis, t1_d, t2_d));
        local_crew.Add(generate_crew(names, t1_lis, t2_lis, t1_d, t2_d));
        local_crew.Add(generate_crew(names, t1_lis, t2_lis, t1_d, t2_d));
        local_crew.Add(generate_crew(names, t1_lis, t2_lis, t1_d, t2_d));
        rum_amount = og_rum_amount;
        spice_amount = og_spice_amount;
        timber_amount = og_timber_amount;
        med_amount = og_med_amount;
        temp_amount = rum_amount;
        //n.text = local_crew[0].get_name();
        //t1.text = local_crew[0].get_t1();
        //t2.text = local_crew[0].get_t2();
        //cost.text = local_crew[0].get_cost().ToString();
        uiScript.updateTavern(local_crew);
    }
    public void hire_crew(int num)
    {
        //Debug.Log("hire_crew Town:"+num);
        Ship_Movement ship_code = ship.GetComponent<Ship_Movement>();
        ship_code.add_crew(local_crew[num]);
        uiScript.addCrew(local_crew[num]);
        local_crew.RemoveAt(num);
        uiScript.updateTavern(local_crew);
        //crew1.SetActive(false);
        //uiScript.destroyCrewTav(num);
        //uiScript.updateTavern(local_crew);
    }

    private Crew generate_crew(List<string> names, List<string> t1_lis, List<string> t2_lis, List<string> t1_d, List<string> t2_d)
    {
        int name_num = Random.Range(0, names.Count);
        int t1_num = Random.Range(0, t1_lis.Count);
        int t2_num = Random.Range(0, t2_lis.Count);
        return new Crew(names[name_num], 2, t1_lis[t1_num], t2_lis[t2_num], t1_d[t1_num], t2_d[t2_num], 5);
    }
    public void change_good_price(int num, string good)
    {
        if (good.Equals("rum"))
        {
            rum_price += num;
        }
        if (good.Equals("spice"))
        {
            spice_price += num;
        }
        if (good.Equals("timber"))
        {
            timber_price += num;
        }
        if (good.Equals("med"))
        {
            med_price += num;
        }
    }
    public void fired_crew(Crew addition)
    {
        if (local_crew.Count < 9)
        {
            local_crew.Add(addition);
            uiScript.updateTavern(local_crew);
        }
    }
}

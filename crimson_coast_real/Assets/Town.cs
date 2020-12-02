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
    public int temp_amount;
    public List<Crew> local_crew = new List<Crew>();
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
        
        temp_amount = rum_amount;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public int get_buy_amount_rum()
    {
        return rum_price;
    }
    public int get_sell_amount_rum()
    {
        return (int)(rum_price-(rum_price * .20));
    }
    public int get_rum_amount()
    {
        return rum_amount;
    }
    public void set_shop_stock()
    {
        temp_amount = rum_amount;
        //rum_stock_market.text = (temp_amount.ToString());
        uiScript.updateMarket("Rum", "Stock", temp_amount);
    }
    public void alter_shop_stock(int diff)
    {
        temp_amount+=diff;
        //rum_stock_market.text = (temp_amount.ToString());
        uiScript.updateMarket("Rum", "Stock", temp_amount);
    }
    public void set_up_tavern(string [] names, string [] t1_lis, string [] t2_lis)
    {
        if (name == "Town3")
        {
            local_crew.Add(new Crew("Skippy", 2, "sharp eyes", "theif"));
            local_crew.Add(new Crew("Adam", 5, "a", "1"));
            local_crew.Add(new Crew("Jerry", 23, "b", "2"));
            local_crew.Add(new Crew("Tom", 6, "c", "3"));
            local_crew.Add(new Crew("Tim", 7, "d", "4"));
            local_crew.Add(new Crew("Jones", 8, "e", "5"));
            local_crew.Add(new Crew("Jim", 3, "f", "6"));
            local_crew.Add(new Crew("Morgen", 4, "g", "7"));
        }
        temp_amount = rum_amount;
        if (name == "Town1")
        {
            local_crew.Add(new Crew("Fred", 2, "Drunk", "superstitious"));
        }
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
        //crew1.SetActive(false);
        //uiScript.destroyCrewTav(num);
        //uiScript.updateTavern(local_crew);
    }

    private Crew generate_crew(string[] names, string[] t1_lis, string [] t2_lis)
    {
        int name_num = Random.Range(0, names.Length);
        int t1_num = Random.Range(0, t1_lis.Length);
        int t2_num = Random.Range(0, t2_lis.Length);
        return new Crew(names[name_num], 2, t1_lis[t1_num], t2_lis[t2_num]);
    }
    public void change_rum_price(int num)
    {
        rum_price += num;
    }
}

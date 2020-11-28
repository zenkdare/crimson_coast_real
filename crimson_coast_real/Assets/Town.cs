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
    public GameObject crew1;
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
    public void set_up_tavern()
    {
        if (name == "Town3")
        {
            local_crew.Add(new Crew("Skippy", 2, "sharp eyes", "theif"));
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
        Ship_Movement ship_code = ship.GetComponent<Ship_Movement>();
        ship_code.add_crew(local_crew[num]);
        //crew1.SetActive(false);
        uiScript.updateTavern(local_crew);
    }
}

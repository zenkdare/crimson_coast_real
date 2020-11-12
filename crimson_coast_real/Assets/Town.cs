using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices.WindowsRuntime;
using UnityEngine;
using UnityEngine.UI;

public class Town : MonoBehaviour
{
    public Text rum_stock_market;
    public int rum_price;
    public int rum_amount;
    public int temp_amount;
    public List<Crew> local_crew = new List<Crew>();
    // Start is called before the first frame update
    void Start()
    {
        if (name == "Island4")
        {
            local_crew.Add(new Crew("Skippy", 2, "sharp eyes", "theif"));
        }
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
        rum_stock_market.text = (temp_amount.ToString());
    }
    public void alter_shop_stock(int diff)
    {
        temp_amount+=diff;
        rum_stock_market.text = (temp_amount.ToString());
    }

}

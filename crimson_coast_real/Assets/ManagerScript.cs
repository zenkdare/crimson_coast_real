using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

public class ManagerScript : MonoBehaviour
{
    public GameObject cam;
    public GameObject start_town;
    //public GameObject chart_course_button;
    //public GameObject confirm_course_button;
    //public GameObject reset_course_button;
    //public GameObject set_sail_button;
    //public GameObject enter_market_button;
    public GameObject map;
    public GameObject courseCharter;
    public GameObject current_location;
    public GameObject ship;
    public NavMeshAgent b_agent;
    public int gold;
    //public Text gold_text;
    //public Text crew_count_text;
    //public GameObject shop_screen;
    //public Text rum_diff;
    public int rum_dif_int;
    public int spice_dif_int;
    public int timber_dif_int;
    public int med_dif_int;
    //public Text rum_cost;
    public int rum_cargo_count;
    public int spice_cargo_count;
    public int timber_cargo_count;
    public int med_cargo_count;
    //public Text rum_cargo_amount_text;
    public int temp_diff;
    private int rum_temp_diff;
    private int spice_temp_diff;
    private int timber_temp_diff;
    private int med_temp_diff;
    string[] crew_names;
    string[] trait1_lis;
    string[] trait2_lis;
    //public GameObject tavern;
    //public GameObject enter_tavern_button;
    public GameObject canvas;
    public UIManager uiScript;
    // Start is called before the first frame update
    void Start()
    {
        crew_names = new string[5] { "John", "Stevens", "Baker", "Robert", "Larry" };
        trait1_lis = new string[2] { "Silver Tongue", "Keen Eyes" };
        trait2_lis = new string[2] { "Greedy", "Theif" };
        SetUpTown(start_town);
        current_location = start_town;
        rum_dif_int = 0;
        spice_dif_int = 0;
        timber_dif_int = 0;
        med_dif_int = 0; 

        change_gold(0);
        //uiScript = canvas.GetComponent<UIManager>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void SetUpTown(GameObject town)
    {
        CameraScript camscript = cam.GetComponent<CameraScript>();
        camscript.Look_at_Location(town);
        Town townscript = town.GetComponent<Town>();
        townscript.set_shop_stock();
        townscript.set_up_tavern(crew_names, trait1_lis, trait2_lis);
        //rum_cargo_amount_text.text = rum_cargo_count.ToString();
        uiScript.updateMarket("Rum", "Cargo", rum_cargo_count);
        //chart_course_button.SetActive(true);
        //set_sail_button.SetActive(true);
        //enter_market_button.SetActive(true);
        //enter_tavern_button.SetActive(true);
        uiScript.TownUI(true);

    }

    public void Chart_a_Course()
    {
        CameraScript camscript = cam.GetComponent<CameraScript>();
        camscript.Look_at_Location(map);
        //chart_course_button.SetActive(false);
        //set_sail_button.SetActive(false);
        //enter_market_button.SetActive(false);
        //enter_tavern_button.SetActive(false);
        //confirm_course_button.SetActive(true);
        //reset_course_button.SetActive(true);
        uiScript.TownUI(false);
        uiScript.ChartUI(true);
        courseCharter.GetComponent<Charting_a_Course>().enabled = true;
    }
    public void enter_market()
    {
        //chart_course_button.SetActive(false);
        //set_sail_button.SetActive(false);
        //enter_market_button.SetActive(false);
        //enter_tavern_button.SetActive(false);
        //shop_screen.SetActive(true);
        uiScript.TownUI(false);
        uiScript.MarketUI(true);
    }
    public void enter_tavern()
    {
        //chart_course_button.SetActive(false);
        //set_sail_button.SetActive(false);
        //enter_market_button.SetActive(false);
        //enter_tavern_button.SetActive(false);
        //tavern.SetActive(true);
        uiScript.TownUI(false);
        uiScript.TavernUI(true);
        uiScript.ShipCrewUI(true);
    }
    public void Confirm_course()
    {
        CameraScript camscript = cam.GetComponent<CameraScript>();
        camscript.Look_at_Location(current_location);
        //chart_course_button.SetActive(true);
        //set_sail_button.SetActive(true);
        //enter_market_button.SetActive(true);
        //enter_tavern_button.SetActive(true);
        //confirm_course_button.SetActive(false);
        //reset_course_button.SetActive(false);
        uiScript.ChartUI(false);
        uiScript.TownUI(true);
        courseCharter.GetComponent<Charting_a_Course>().enabled = false;
    }

    public void Clear_course()
    {
        //Charting_a_Course charter = courseCharter.GetComponent<Charting_a_Course>();
        
    }

    public void Set_Sail()
    {
        if (b_agent.hasPath)
        {
            CameraScript camscript = cam.GetComponent<CameraScript>();
            Camera_Orbit camorbit = cam.GetComponent<Camera_Orbit>();
            camscript.enabled = false;
            camorbit.enabled = true;
            Ship_Movement shipscript = ship.GetComponent<Ship_Movement>();
            shipscript.inport = false;
            //set_sail_button.SetActive(false);
            //enter_market_button.SetActive(false);
            //enter_tavern_button.SetActive(false);
            uiScript.TownUI(false);
            uiScript.DestroyCrewTav();
        }
        else
        {
            print("you must choose a destination before setting sail");
        }
        
    }

    public void In_To_Port(GameObject location)
    {
        Ship_Movement shipscript = ship.GetComponent<Ship_Movement>();
        shipscript.inport = true;
        current_location = location;
        if (location.tag == "town")
        {
            SetUpTown(location);
        }
        CameraScript camscript = cam.GetComponent<CameraScript>();
        Camera_Orbit camorbit = cam.GetComponent<Camera_Orbit>();
        camscript.enabled = true;
        camorbit.enabled = false;
        change_gold(-2);
    }
    public void add_item(string item)
    {
        Town townscript = current_location.GetComponent<Town>();
        if (item.Equals("rum") && townscript.get_good_amount("rum")>rum_dif_int)
        {
            rum_dif_int += 1;
            //rum_diff.text = rum_dif_int.ToString();
            uiScript.updateMarket("Rum", "Amount", rum_dif_int);
            if (rum_dif_int > 0)
            {
                temp_diff -= townscript.get_buy_amount("rum");
                rum_temp_diff -= townscript.get_buy_amount("rum");
            }
            else if (rum_dif_int <= 0)
            {
                temp_diff -= townscript.get_sell_amount("rum");
                rum_temp_diff -= townscript.get_sell_amount("rum");
            }
            //rum_cost.text = temp_diff.ToString();
            uiScript.updateMarket("Rum", "Cost", rum_temp_diff);
        }
        else if (item.Equals("spice") && townscript.get_good_amount("spice")>spice_dif_int)
        {
            spice_dif_int += 1;
            //rum_diff.text = rum_dif_int.ToString();
            uiScript.updateMarket("Spice", "Amount", spice_dif_int);
            if (spice_dif_int > 0)
            {
                temp_diff -= townscript.get_buy_amount("spice");
                spice_temp_diff -= townscript.get_buy_amount("spice");
            }
            else if (spice_dif_int <= 0)
            {
                temp_diff -= townscript.get_sell_amount("spice");
                spice_temp_diff -= townscript.get_sell_amount("spice");
            }
            //rum_cost.text = temp_diff.ToString();
            uiScript.updateMarket("Spice", "Cost", spice_temp_diff);
        }
        else if (item.Equals("timber") && townscript.get_good_amount("timber")>timber_dif_int)
        {
            timber_dif_int += 1;
            //rum_diff.text = rum_dif_int.ToString();
            uiScript.updateMarket("Timber", "Amount", timber_dif_int);
            if (timber_dif_int > 0)
            {
                temp_diff -= townscript.get_buy_amount("timber");
                timber_temp_diff -= townscript.get_buy_amount("timber");
            }
            else if (timber_dif_int <= 0)
            {
                temp_diff -= townscript.get_sell_amount("timber");
                timber_temp_diff -= townscript.get_sell_amount("timber");
            }
            //rum_cost.text = temp_diff.ToString();
            uiScript.updateMarket("Timber", "Cost", timber_temp_diff);
        }
        else if (item.Equals("medicine") && townscript.get_good_amount("med")>med_dif_int)
        {
            med_dif_int += 1;
            //rum_diff.text = rum_dif_int.ToString();
            uiScript.updateMarket("Medicine", "Amount", med_dif_int);
            if (med_dif_int > 0)
            {
                temp_diff -= townscript.get_buy_amount("med");
                med_temp_diff -= townscript.get_buy_amount("med");
            }
            else if (med_dif_int <= 0)
            {
                temp_diff -= townscript.get_sell_amount("med");
                med_temp_diff -= townscript.get_sell_amount("med");
            }
            //rum_cost.text = temp_diff.ToString();
            uiScript.updateMarket("Medicine", "Cost", med_temp_diff);
        }
        else
        {
            print("can't buy more than stock allows");
        }
    }
    public void sub_item(string item)
    {
        Town townscript = current_location.GetComponent<Town>();
        if (item.Equals("rum") && rum_cargo_count > -rum_dif_int)
        {
            rum_dif_int -= 1;
            //rum_diff.text = rum_dif_int.ToString();
            uiScript.updateMarket("Rum", "Amount", rum_dif_int);
            if (rum_dif_int >= 0)
            {
                temp_diff += townscript.get_buy_amount("rum");
                rum_temp_diff += townscript.get_buy_amount("rum");
            }
            else if (rum_dif_int < 0)
            {
                temp_diff += townscript.get_sell_amount("rum");
                rum_temp_diff += townscript.get_sell_amount("rum");
            }
            //rum_cost.text = temp_diff.ToString();
            uiScript.updateMarket("Rum", "Cost", rum_temp_diff);
        }
        if (item.Equals("spice") && spice_cargo_count > -spice_dif_int)
        {
            spice_dif_int -= 1;
            //rum_diff.text = rum_dif_int.ToString();
            uiScript.updateMarket("Spice", "Amount", spice_dif_int);
            if (spice_dif_int >= 0)
            {
                temp_diff += townscript.get_buy_amount("spice");
                spice_temp_diff += townscript.get_buy_amount("spice");
            }
            else if (spice_dif_int < 0)
            {
                temp_diff += townscript.get_sell_amount("spice");
                spice_temp_diff += townscript.get_sell_amount("spice");
            }
            //rum_cost.text = temp_diff.ToString();
            uiScript.updateMarket("Spice", "Cost", spice_temp_diff);
        }
        if (item.Equals("timber") && timber_cargo_count > -timber_dif_int)
        {
            timber_dif_int -= 1;
            //rum_diff.text = rum_dif_int.ToString();
            uiScript.updateMarket("Timber", "Amount", timber_dif_int);
            if (timber_dif_int >= 0)
            {
                temp_diff += townscript.get_buy_amount("timber");
                timber_temp_diff += townscript.get_buy_amount("timber");
            }
            else if (timber_dif_int < 0)
            {
                temp_diff += townscript.get_sell_amount("timber");
                timber_temp_diff += townscript.get_sell_amount("timber");
            }
            //rum_cost.text = temp_diff.ToString();
            uiScript.updateMarket("Timber", "Cost", timber_temp_diff);
        }
        if (item.Equals("medicine") && med_cargo_count > -med_dif_int)
        {
            med_dif_int -= 1;
            //rum_diff.text = rum_dif_int.ToString();
            uiScript.updateMarket("Medicine", "Amount", med_dif_int);
            if (med_dif_int >= 0)
            {
                temp_diff += townscript.get_buy_amount("med");
                med_temp_diff += townscript.get_buy_amount("med");
            }
            else if (med_dif_int < 0)
            {
                temp_diff += townscript.get_sell_amount("med");
                med_temp_diff += townscript.get_sell_amount("med");
            }
            //rum_cost.text = temp_diff.ToString();
            uiScript.updateMarket("Medicine", "Cost", med_temp_diff);
        }
    }
    public void confirm_purchase()
    {
        if (temp_diff<0)
        {
            if (-temp_diff <= gold)
            {
                Town townscript = current_location.GetComponent<Town>();
                townscript.alter_shop_stock(-rum_dif_int, "rum");
                townscript.alter_shop_stock(-spice_dif_int, "spice");
                townscript.alter_shop_stock(-timber_dif_int, "timber");
                townscript.alter_shop_stock(-med_dif_int, "med");
                //rum_diff.text = ("0");
                uiScript.updateMarket("Rum", "Amount", 0);
                uiScript.updateMarket("Spice", "Amount", 0);
                uiScript.updateMarket("Timber", "Amount", 0);
                uiScript.updateMarket("Medicine", "Amount", 0);
                //rum_cost.text = ("0");
                uiScript.updateMarket("Rum", "Cost", 0);
                uiScript.updateMarket("Spice", "Cost", 0);
                uiScript.updateMarket("Timber", "Cost", 0);
                uiScript.updateMarket("Medicine", "Cost", 0);
                rum_cargo_count += rum_dif_int;
                spice_cargo_count += spice_dif_int;
                timber_cargo_count += timber_dif_int;
                med_cargo_count += med_dif_int;
                //rum_cargo_amount_text.text = rum_cargo_count.ToString();
                uiScript.updateMarket("Rum", "Cargo", rum_cargo_count);
                uiScript.updateMarket("Spice", "Cargo", spice_cargo_count);
                uiScript.updateMarket("Timber", "Cargo", timber_cargo_count);
                uiScript.updateMarket("Medicine", "Cargo", med_cargo_count);
                rum_dif_int = 0;
                spice_dif_int = 0;
                timber_dif_int = 0;
                med_dif_int = 0;
                change_gold(temp_diff);
                temp_diff = 0;
                rum_temp_diff = 0;
                spice_temp_diff = 0;
                timber_temp_diff = 0;
                med_temp_diff = 0;
            }
            else
            {
                print("can't spend more than you have");
            }
        }
        else
        {
            Town townscript = current_location.GetComponent<Town>();
            townscript.alter_shop_stock(-rum_dif_int, "rum");
            townscript.alter_shop_stock(-spice_dif_int, "spice");
            townscript.alter_shop_stock(-timber_dif_int, "timber");
            townscript.alter_shop_stock(-med_dif_int, "med");
            //rum_diff.text = ("0");
            uiScript.updateMarket("Rum", "Amount", 0);
            uiScript.updateMarket("Spice", "Amount", 0);
            uiScript.updateMarket("Timber", "Amount", 0);
            uiScript.updateMarket("Medicine", "Amount", 0);
            //rum_cost.text = ("0");
            uiScript.updateMarket("Rum", "Cost", 0);
            uiScript.updateMarket("Spice", "Cost", 0);
            uiScript.updateMarket("Timber", "Cost", 0);
            uiScript.updateMarket("Medicine", "Cost", 0);
            rum_cargo_count += rum_dif_int;
            spice_cargo_count += spice_dif_int;
            timber_cargo_count += timber_dif_int;
            med_cargo_count += med_dif_int;
            //rum_cargo_amount_text.text = rum_cargo_count.ToString();
            uiScript.updateMarket("Rum", "Cargo", rum_cargo_count);
            uiScript.updateMarket("Spice", "Cargo", spice_cargo_count);
            uiScript.updateMarket("Timber", "Cargo", timber_cargo_count);
            uiScript.updateMarket("Medicine", "Cargo", med_cargo_count);
            rum_dif_int = 0;
            spice_dif_int = 0;
            timber_dif_int = 0;
            med_dif_int = 0;
            change_gold(temp_diff);
            temp_diff = 0;
            rum_temp_diff = 0;
            spice_temp_diff = 0;
            timber_temp_diff = 0;
            med_temp_diff = 0;
        }
    }
    public void exit_market()
    {
        //chart_course_button.SetActive(true);
        //set_sail_button.SetActive(true);
        //enter_market_button.SetActive(true);
        //enter_tavern_button.SetActive(true);
        //shop_screen.SetActive(false);
        uiScript.MarketUI(false);
        uiScript.TownUI(true);
    }
    public void exit_tavern()
    {
        //chart_course_button.SetActive(true);
        //set_sail_button.SetActive(true);
        //enter_market_button.SetActive(true);
        //enter_tavern_button.SetActive(true);
        //tavern.SetActive(false);
        uiScript.TavernUI(false);
        uiScript.ShipCrewUI(false);
        uiScript.TownUI(true);
    }
    public void change_gold(int change)
    {
        gold += change;
        //gold_text.text = ("Gold: " + gold);
        uiScript.updateGold(gold);
    }
    public void hire_crew(int num)
    {
        //Debug.Log("hire_crew manager:"+num);
        Town townscript = current_location.GetComponent<Town>();
        townscript.hire_crew(num);
        Ship_Movement ship_script = ship.GetComponent<Ship_Movement>();
        //crew_count_text.text = ("Crew Count: "+ship_script.get_crew_count().ToString());
        uiScript.updateCrewCount(ship_script.get_crew_count());
    }
    public void fire_crew(int num)
    {//Removes a crewmate from crew listing
        //Debug.Log("fire_crew manager:"+num);
        Ship_Movement ship_script = ship.GetComponent<Ship_Movement>();

        //crew_count_text.text = ("Crew Count: "+ship_script.get_crew_count().ToString());
        uiScript.updateCrewCount(ship_script.get_crew_count());
    }
    public void handle_event(string n, int option)
    {
        if (n.Equals("storm"))
        {
            if (option == 0)
            {

            }
        }
        if (n.Equals("Theft"))
        {
            if (option == 1)
            {
                rum_cargo_count -= 2;
                Ship_Movement ship_script = ship.GetComponent<Ship_Movement>();
                ship_script.dock();
            }
        }
    }
    public void weekReport(){
        string report = "generate a string\n this should have info about everything that happened in the week\n Like you spent 5 gold on crew\n you lost 7 rum to theft\n you used 20 rations on crew\n etc";
        uiScript.weekInfoDisp(report);
        uiScript.EventResultUI(false);
        uiScript.WeekInfoUI(true);
    }

}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;
using System.IO;

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
    public int rations_cost;
    public int rum_dif_int;
    public int spice_dif_int;
    public int timber_dif_int;
    public int med_dif_int;
    public int rations_dif_int;
    //public Text rum_cost;
    public int rum_cargo_count;
    public int spice_cargo_count;
    public int timber_cargo_count;
    public int med_cargo_count;
    public int rations_cargo_count;
    //public Text rum_cargo_amount_text;
    public int temp_diff;
    private int rum_temp_diff;
    private int spice_temp_diff;
    private int timber_temp_diff;
    private int med_temp_diff;
    private int rations_temp_diff;
    List<string> crew_names;
    List<string> trait1_lis;
    List<string> trait2_lis;
    List<string> trait1d;
    List<string> trait2d;
    public int current_ration_state;
    //public GameObject tavern;
    //public GameObject enter_tavern_button;
    public GameObject canvas;
    public UIManager uiScript;
    public AudioManager audioScript;
    public int required_crew_count;
    public int max_cargo;
    private string event_outcome;
    public bool first_upgrade;
    public bool second_upgrade;
    public bool in_week_report;
    // Start is called before the first frame update
    void Start()
    {
        crew_names = new List<string>();
        trait1_lis = new List<string>();
        trait2_lis = new List<string>();
        trait1d = new List<string>();
        trait2d = new List<string>();
        current_location = start_town;
        rum_dif_int = 0;
        spice_dif_int = 0;
        timber_dif_int = 0;
        med_dif_int = 0;
        rations_dif_int = 0;
        rations_dif_int = 0;
        change_gold(0);
        current_ration_state = 2;
        Ship_Movement ship_script = ship.GetComponent<Ship_Movement>();
        uiScript.updateCrewCount(ship_script.get_crew_count(), required_crew_count);
        uiScript.updateCargoCount(0, max_cargo);
        //where you load the strings from a text file for crew generation
        StreamReader sr = new StreamReader("Assets/CrewNames.txt");
        string line;
        while ((line = sr.ReadLine()) != null)
        {
            crew_names.Add(line);
        }
        sr.Close();
        sr = new StreamReader("Assets/Traits_1.txt");
        while ((line = sr.ReadLine()) != null)
        {
            trait1_lis.Add(line);
        }
        sr.Close();
        sr = new StreamReader("Assets/Traits_2.txt");
        while ((line = sr.ReadLine()) != null)
        {
            trait2_lis.Add(line);
        }
        sr.Close();
        sr = new StreamReader("Assets/T1_D.txt");
        while ((line = sr.ReadLine()) != null)
        {
            trait1d.Add(line);
        }
        sr.Close();
        sr = new StreamReader("Assets/T2_D.txt");
        while ((line = sr.ReadLine()) != null)
        {
            trait2d.Add(line);
        }
        sr.Close();
        SetUpTown(start_town);
        //uiScript = canvas.GetComponent<UIManager>();
        first_upgrade = false;
        second_upgrade = false;
        in_week_report = false;
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
        townscript.set_up_tavern(crew_names, trait1_lis, trait2_lis, trait1d, trait2d);
        //rum_cargo_amount_text.text = rum_cargo_count.ToString();
        uiScript.updateMarket("Rum", "Cargo", rum_cargo_count);
        uiScript.updateMarket("Spice", "Cargo", spice_cargo_count);
        uiScript.updateMarket("Timber", "Cargo", timber_cargo_count);
        uiScript.updateMarket("Medicine", "Cargo", med_cargo_count);
        uiScript.updateMarket("Rations", "Cargo", med_cargo_count);
        //chart_course_button.SetActive(true);
        //set_sail_button.SetActive(true);
        //enter_market_button.SetActive(true);
        //enter_tavern_button.SetActive(true);
        uiScript.TownUI(true);
        audioScript.portSound(true);

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
    public void Chart_a_Course_Report()
    {
        CameraScript camscript = cam.GetComponent<CameraScript>();
        Camera_Orbit camorbit = cam.GetComponent<Camera_Orbit>();
        camscript.enabled = true;
        camorbit.enabled = false;
        camscript.Look_at_Location(map);
        //chart_course_button.SetActive(false);
        //set_sail_button.SetActive(false);
        //enter_market_button.SetActive(false);
        //enter_tavern_button.SetActive(false);
        //confirm_course_button.SetActive(true);
        //reset_course_button.SetActive(true);
        //uiScript.TownUI(false);
        uiScript.ChartReportUI(true);
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
        //uiScript.ShipCrewUI(true);
    }
    public void Confirm_course()
    {
        Ship_Movement shipscript = ship.GetComponent<Ship_Movement>();
        if (shipscript.get_target_port()!=current_location)
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
        else
        {
            uiScript.ErrorDisp("You're already there you fool!");
        }
        
    }
    public void Confirm_course_Report()
    {
        Ship_Movement shipscript = ship.GetComponent<Ship_Movement>();
        if (shipscript.get_target_port() != current_location)
        {
            CameraScript camscript = cam.GetComponent<CameraScript>();
            Camera_Orbit camorbit = cam.GetComponent<Camera_Orbit>();
            camscript.enabled = false;
            camorbit.enabled = true;
            //chart_course_button.SetActive(true);
            //set_sail_button.SetActive(true);
            //enter_market_button.SetActive(true);
            //enter_tavern_button.SetActive(true);
            //confirm_course_button.SetActive(false);
            //reset_course_button.SetActive(false);
            uiScript.ChartReportUI(false);
            uiScript.WeekInfoUI(true);
            courseCharter.GetComponent<Charting_a_Course>().enabled = false;
        }
        else
        {
            uiScript.ErrorDisp("You're either in town already, or need to let stocks replenish");
        }
    }

    public void Clear_course()
    {
        //Charting_a_Course charter = courseCharter.GetComponent<Charting_a_Course>();
        
    }

    public void Set_Sail()
    {
        Ship_Movement shipscript = ship.GetComponent<Ship_Movement>();
        if (b_agent.hasPath && shipscript.get_crew_count()>=required_crew_count)
        {
            CameraScript camscript = cam.GetComponent<CameraScript>();
            Camera_Orbit camorbit = cam.GetComponent<Camera_Orbit>();
            camscript.enabled = false;
            camorbit.enabled = true;
            
            shipscript.inport = false;
            //set_sail_button.SetActive(false);
            //enter_market_button.SetActive(false);
            //enter_tavern_button.SetActive(false);
            uiScript.TownUI(false);
            uiScript.DestroyCrewTav();
            audioScript.oceanSound(true);
        }
        else
        {
            uiScript.ErrorDisp("you must choose a destination before setting sail and have the required number of crew for your ship size");
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
        else if (item.Equals("rations"))
        {
            rations_dif_int += 3;
            uiScript.updateMarket("Rations", "Amount", rations_dif_int);
            if (rations_dif_int > 0)
            {
                temp_diff -= rations_cost;
                rations_temp_diff -= rations_cost;
            }
            else if (rations_dif_int <= 0)
            {
                temp_diff -= rations_cost;
                rations_temp_diff -= rations_cost;
            }
            uiScript.updateMarket("Rations", "Cost", rations_temp_diff);
        }
        else
        {
            uiScript.ErrorDisp("can't buy more than stock allows");
            print("can't buy more than stock allows");
        }

        uiScript.updateMarket("Footer", "Cost", temp_diff);//total cost of all items yet to be purchased
        uiScript.updateMarket("Footer", "Amount", (rations_dif_int/3) + med_dif_int + timber_dif_int + spice_dif_int + rum_dif_int);//total number of all items yet to be purchased
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
        if (item.Equals("rations") && rations_cargo_count > -rations_dif_int)
        {
            rations_dif_int -= 3;
            //rum_diff.text = rum_dif_int.ToString();
            uiScript.updateMarket("Rations", "Amount", rations_dif_int);
            if (rations_dif_int >= 0)
            {
                temp_diff += rations_cost;
                rations_temp_diff += rations_cost;
            }
            else if (rations_dif_int < 0)
            {
                temp_diff += rations_cost;
                rations_temp_diff += rations_cost;
            }
            //rum_cost.text = temp_diff.ToString();
            uiScript.updateMarket("Rations", "Cost", rations_temp_diff);
        }
        uiScript.updateMarket("Footer", "Cost", temp_diff);//total cost of all items yet to be purchased
        uiScript.updateMarket("Footer", "Amount", (rations_dif_int/3)+med_dif_int+timber_dif_int+spice_dif_int+rum_dif_int);//total number of all items yet to be purchased
    }
    public void confirm_purchase()
    {
        if (temp_diff<0)
        {
            if (-temp_diff <= gold )
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
                uiScript.updateMarket("Rations", "Amount", 0);
                //rum_cost.text = ("0");
                uiScript.updateMarket("Rum", "Cost", 0);
                uiScript.updateMarket("Spice", "Cost", 0);
                uiScript.updateMarket("Timber", "Cost", 0);
                uiScript.updateMarket("Medicine", "Cost", 0);
                uiScript.updateMarket("Rations", "Cost", 0);
                rum_cargo_count += rum_dif_int;
                spice_cargo_count += spice_dif_int;
                timber_cargo_count += timber_dif_int;
                med_cargo_count += med_dif_int;
                rations_cargo_count += rations_dif_int;
                //rum_cargo_amount_text.text = rum_cargo_count.ToString();
                uiScript.updateMarket("Rum", "Cargo", rum_cargo_count);
                uiScript.updateMarket("Spice", "Cargo", spice_cargo_count);
                uiScript.updateMarket("Timber", "Cargo", timber_cargo_count);
                uiScript.updateMarket("Medicine", "Cargo", med_cargo_count);
                uiScript.updateMarket("Rations", "Cargo", rations_cargo_count);
                uiScript.updateCargoCount(((rations_cargo_count / 3) + med_cargo_count + timber_cargo_count + spice_cargo_count + rum_cargo_count), max_cargo);
                rum_dif_int = 0;
                spice_dif_int = 0;
                timber_dif_int = 0;
                med_dif_int = 0;
                rations_dif_int = 0;
                change_gold(temp_diff);
                temp_diff = 0;
                rum_temp_diff = 0;
                spice_temp_diff = 0;
                timber_temp_diff = 0;
                med_temp_diff = 0;
                rations_temp_diff = 0;
                uiScript.updateMarket("Footer", "Cost", temp_diff);//total cost of all items yet to be purchased
                uiScript.updateMarket("Footer", "Amount", 0);//total number of all items yet to be purchased
            }
            else
            {
                print("can't spend more than ye have, and can't buy more than ye can hold");    
            }
        }
        else if (rations_dif_int + med_dif_int + timber_dif_int + spice_dif_int + rum_dif_int > max_cargo)
        {
            uiScript.ErrorDisp("You can't exceed your max cargo space of "+max_cargo);
        }
        else
        {
            Town townscript = current_location.GetComponent<Town>();
            townscript.alter_shop_stock(-rum_dif_int, "rum");
            townscript.alter_shop_stock(-spice_dif_int, "spice");
            townscript.alter_shop_stock(-timber_dif_int, "timber");
            townscript.alter_shop_stock(-med_dif_int, "med");

            uiScript.updateMarket("Rum", "Amount", 0);
            uiScript.updateMarket("Spice", "Amount", 0);
            uiScript.updateMarket("Timber", "Amount", 0);
            uiScript.updateMarket("Medicine", "Amount", 0);
            uiScript.updateMarket("Rations", "Amount", 0);
            
            uiScript.updateMarket("Rum", "Cost", 0);
            uiScript.updateMarket("Spice", "Cost", 0);
            uiScript.updateMarket("Timber", "Cost", 0);
            uiScript.updateMarket("Medicine", "Cost", 0);
            uiScript.updateMarket("Rations", "Amount", 0);
            rum_cargo_count += rum_dif_int;
            spice_cargo_count += spice_dif_int;
            timber_cargo_count += timber_dif_int;
            med_cargo_count += med_dif_int;
            rations_cargo_count += rations_dif_int;
            
            uiScript.updateMarket("Rum", "Cargo", rum_cargo_count);
            uiScript.updateMarket("Spice", "Cargo", spice_cargo_count);
            uiScript.updateMarket("Timber", "Cargo", timber_cargo_count);
            uiScript.updateMarket("Medicine", "Cargo", med_cargo_count);
            uiScript.updateMarket("Rations", "Cargo", rations_cargo_count);
            uiScript.updateCargoCount(((rations_cargo_count / 3) + med_cargo_count + timber_cargo_count + spice_cargo_count + rum_cargo_count), max_cargo);
            uiScript.cargoUpdate();
            rum_dif_int = 0;
            spice_dif_int = 0;
            timber_dif_int = 0;
            med_dif_int = 0;
            rations_dif_int = 0;
            change_gold(temp_diff);
            temp_diff = 0;
            rum_temp_diff = 0;
            spice_temp_diff = 0;
            timber_temp_diff = 0;
            med_temp_diff = 0;
            rations_temp_diff = 0;
            uiScript.updateMarket("Footer", "Cost", temp_diff);//total cost of all items yet to be purchased
            uiScript.updateMarket("Footer", "Amount", 0);//total number of all items yet to be purchased
        }
    }
    public void exit_market()
    {
        //chart_course_button.SetActive(true);
        //set_sail_button.SetActive(true);
        //enter_market_button.SetActive(true);
        //enter_tavern_button.SetActive(true);
        //shop_screen.SetActive(false);
        uiScript.updateMarket("Rum", "Amount", 0);
        uiScript.updateMarket("Spice", "Amount", 0);
        uiScript.updateMarket("Timber", "Amount", 0);
        uiScript.updateMarket("Medicine", "Amount", 0);
        uiScript.updateMarket("Rations", "Amount", 0);

        uiScript.updateMarket("Rum", "Cost", 0);
        uiScript.updateMarket("Spice", "Cost", 0);
        uiScript.updateMarket("Timber", "Cost", 0);
        uiScript.updateMarket("Medicine", "Cost", 0);
        uiScript.updateMarket("Rations", "Amount", 0);
        uiScript.updateMarket("Rum", "Cargo", rum_cargo_count);
        uiScript.updateMarket("Spice", "Cargo", spice_cargo_count);
        uiScript.updateMarket("Timber", "Cargo", timber_cargo_count);
        uiScript.updateMarket("Medicine", "Cargo", med_cargo_count);
        uiScript.updateMarket("Rations", "Cargo", rations_cargo_count);
        uiScript.updateCargoCount(((rations_cargo_count / 3) + med_cargo_count + timber_cargo_count + spice_cargo_count + rum_cargo_count), max_cargo);
        uiScript.cargoUpdate();
        rum_dif_int = 0;
        spice_dif_int = 0;
        timber_dif_int = 0;
        med_dif_int = 0;
        rations_dif_int = 0;
        temp_diff = 0;
        rum_temp_diff = 0;
        spice_temp_diff = 0;
        timber_temp_diff = 0;
        med_temp_diff = 0;
        rations_temp_diff = 0;
        uiScript.updateMarket("Footer", "Cost", temp_diff);//total cost of all items yet to be purchased
        uiScript.updateMarket("Footer", "Amount", 0);//total number of all items yet to be purchased
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
        //uiScript.ShipCrewUI(false);
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
        Ship_Movement ship_script = ship.GetComponent<Ship_Movement>();
        townscript.hire_crew(num);
        
        //crew_count_text.text = ("Crew Count: "+ship_script.get_crew_count().ToString());
        uiScript.updateCrewCount(ship_script.get_crew_count(), required_crew_count);
    }
    public void fire_crew(int num)
    {//Removes a crewmate from crew listing
        //Debug.Log("fire_crew manager:"+num);
        print("num: " + num);
        Ship_Movement ship_script = ship.GetComponent<Ship_Movement>();
        print(1);
        Crew r_fired = ship_script.get_crew_at_spot(num);
        print(2);
        Town townscript = current_location.GetComponent<Town>();
        print(3);
        townscript.fired_crew(r_fired);
        print(4);
        ship_script.fire_crew(num);
        print(5);
        //crew_count_text.text = ("Crew Count: "+ship_script.get_crew_count().ToString());
        uiScript.updateCrewCount(ship_script.get_crew_count(), required_crew_count);
    }
    public void handle_event(Event e, int result)
    {
        Camera_Orbit camorbit = cam.GetComponent<Camera_Orbit>();
        camorbit.enabled = false;
        if (e.get_id()==0)
        {
            if (result == 1)
            {
                int stolen_good = Random.Range(0, 5);
                if (stolen_good == 0)
                {
                    rum_cargo_count -= 3;
                    event_outcome = "three cases of rum were stolen";
                }
                if (stolen_good == 1)
                {
                    spice_cargo_count -= 3;
                    event_outcome = "three cases of spice were stolen";
                }
                if (stolen_good == 2)
                {
                    timber_cargo_count -= 3;
                    event_outcome = "three crates of timber were stolen";
                }
                if (stolen_good == 2)
                {
                    med_cargo_count -= 3;
                    event_outcome = "three crates of medicine were stolen";
                }
            }
            if (result == 2)
            {
                int stolen_good = Random.Range(0, 5);
                if (stolen_good == 0)
                {
                    rum_cargo_count -= 1;
                    event_outcome = "three cases of rum were stolen";
                }
                if (stolen_good == 1)
                {
                    spice_cargo_count -= 1;
                    event_outcome = "three cases of spice were stolen";
                }
                if (stolen_good == 2)
                {
                    timber_cargo_count -= 1;
                    event_outcome = "three crates of timber were stolen";
                }
                if (stolen_good == 2)
                {
                    med_cargo_count -= 1;
                    event_outcome = "three crates of rum were stolen";
                }
            }
        }
        if (e.get_id() == 1)
        {
            if (result == 1)
            {
                timber_cargo_count -= 3;
                rations_cargo_count -= 3;
                event_outcome = "Three crates of timber and 6 rations were lost";
            }
            if (result == 2)
            {
                Ship_Movement ship_script = ship.GetComponent<Ship_Movement>();
                ship_script.change_loyalty_all(-1);
                event_outcome = "The loyalty of all crew was lowered by one";
            }
        }
        if (e.get_id() == 2)
        {
            if (result == 0)
            {
                Ship_Movement ship_script = ship.GetComponent<Ship_Movement>();
                ship_script.change_loyalty_all(2);
                event_outcome = "The loyalty of all crew was raised by two";
            }
            if (result == 1)
            {
                med_cargo_count -= 3;
                timber_cargo_count -= 3;
                event_outcome = "three crates of medicine and three crates of timber were lost";
            }
            if (result == 2)
            {
                med_cargo_count--;
                timber_cargo_count--;
                event_outcome = "one crate of medicine and one crate of timber were lost";
            }
        }
        if(e.get_id() == 3)
        {
            int r = Random.Range(0, 4);
            Ship_Movement ship_script = ship.GetComponent<Ship_Movement>();
            int add = ship_script.get_lucky_num() * 2;
            if (r == 0)
            {
                rum_cargo_count += 1 + add;
                event_outcome = (1+add)+" crates of rum were found as floatsam";
            }
            if (r == 1)
            {
                spice_cargo_count += 1 + add;
                event_outcome = (1 + add) + " crates of spice were found as floatsam";
            }
            if (r == 2)
            {
                timber_cargo_count += 1 + add;
                event_outcome = (1 + add) + " crates of timber were found as floatsam";
            }
            if (r == 3)
            {
                med_cargo_count += 1 + add;
                event_outcome = (1 + add) + " crates of medicine were found as floatsam";
            }
        }
        if(e.get_id() == 4)
        {
            if (result == 1)
            {
                med_cargo_count--;
                event_outcome =  "one crate of medicine was lost to cure a sickness";
            }
        }
        if (e.get_id() == 5)
        {
            if (result == 1)
            {
                timber_cargo_count--;
                event_outcome = "one crate of timber was lost to fix up your ship from rough seas";
            }
        }
        if (e.get_id() == 6)
        {  
            timber_cargo_count--;
            event_outcome = "one crate of timber was lost to fix up your ship from an accident";
        }
        uiScript.updateCargoCount(((rations_cargo_count / 3) + med_cargo_count + timber_cargo_count + spice_cargo_count + rum_cargo_count), max_cargo);
        uiScript.cargoUpdate();
    }
    //code for the stuff that comes after events
    public void weekReport() {
        in_week_report = true;
        Camera_Orbit camorbit = cam.GetComponent<Camera_Orbit>();
        camorbit.enabled = false;
        Ship_Movement ship_script = ship.GetComponent<Ship_Movement>();
        if (current_ration_state == 2)
        {
            rations_cargo_count -= (ship_script.get_crew_count() * 2);
            ship_script.extra_rations();
        }
        if (current_ration_state == 1)
        {
            rations_cargo_count -= ship_script.get_crew_count();
            ship_script.normal_rations();
        }
        if (current_ration_state == 0)
        {
            ship_script.no_rations();
        }
        if (rations_cargo_count < 0)
        {
            rations_cargo_count = 0;
        }
        ship_script.handle_mutiny_spread();
        int gold_change = ship_script.get_wages();
        change_gold(-gold_change);
        rations_cargo_count -= ship_script.get_crew_count();
        //print(event_outcome);
        string report = gold_change + " gold was spent to pay your crew\n" + ship_script.get_crew_count() + " rations were used to fed your crew\n"+event_outcome;
        uiScript.weekInfoDisp(report);
        uiScript.EventResultUI(false);
        uiScript.WeekInfoUI(true);
        uiScript.updateCargoCount(((rations_cargo_count / 3) + med_cargo_count + timber_cargo_count + spice_cargo_count + rum_cargo_count), max_cargo);
        uiScript.cargoUpdate();
        if (current_ration_state > rations_cargo_count)
        {
            current_ration_state--;
            uiScript.ErrorDisp("ration plan has been lowered due to lack of food");
        }
        if (current_ration_state > rations_cargo_count)
        {
            current_ration_state--;
            uiScript.ErrorDisp("ration plan has been lowered due to lack of food");
        }
    }

    public void choose_ration_type(int num)
    {
        //generous should be 2, normal is 1, none is 0
        if (num > rations_cargo_count)
        {
            uiScript.ErrorDisp("you can't more food than you have");
        }
        else
        {
            current_ration_state = num;
        }
    }

    //methods for getting the cargo and stuff
    public int get_cargo_rum()
    {
        return rum_cargo_count;
    }
    public int get_cargo_spice()
    {
        return spice_cargo_count;
    }
    public int get_cargo_timber()
    {
        return timber_cargo_count;
    }
    public int get_cargo_med()
    {
        return med_cargo_count;
    }
    public int get_cargo_rations()
    {
        return rations_cargo_count;
    }

    public void give_rum()
    {
        if (rum_cargo_count > 0)
        {
            Ship_Movement ship_script = ship.GetComponent<Ship_Movement>();
            ship_script.give_rum();
            rum_cargo_count--;
        }
        else
        {
            uiScript.ErrorDisp("can't give what aint there");
            print("can't give what aint there");
        }
    }
    public void give_spice()
    {
        Ship_Movement ship_script = ship.GetComponent<Ship_Movement>();
        ship_script.toggle_spice(true);
    }
    public void spend_spice()
    {
        spice_cargo_count--;
    }
    public void QuitGame(){
        Application.Quit();
    }
    public void upgrade_ship()
    {
        if (!first_upgrade && gold>=250)
        {
            max_cargo = 15;
            required_crew_count = 6;
            gold -= 250;
            first_upgrade = true;
        }
        if (first_upgrade && gold >= 500)
        {
            max_cargo = 20;
            required_crew_count = 9;
            gold -= 500;
            second_upgrade = true;
        }
        if (second_upgrade)
        {
            uiScript.ErrorDisp("no furthur upgrade available");
        }
    }
}

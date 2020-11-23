﻿using System.Collections;
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
    //public Text rum_cost;
    public int rum_cargo_count;
    //public Text rum_cargo_amount_text;
    public int temp_diff;
    //public GameObject tavern;
    //public GameObject enter_tavern_button;
    public GameObject canvas;
    public UIManager uiScript;
    // Start is called before the first frame update
    void Start()
    {
        SetUpTown(start_town);
        current_location = start_town;
        rum_dif_int = 0;
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
        townscript.set_up_tavern();
        //rum_cargo_amount_text.text = rum_cargo_count.ToString();
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
        uiScript.MarketUI(true);
    }
    public void enter_tavern()
    {
        //chart_course_button.SetActive(false);
        //set_sail_button.SetActive(false);
        //enter_market_button.SetActive(false);
        //enter_tavern_button.SetActive(false);
        //tavern.SetActive(true);
        uiScript.TavernUI(true);
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
        if (item.Equals("rum"))
        {
            rum_dif_int += 1;
            //rum_diff.text = rum_dif_int.ToString();
            if (rum_dif_int > 0)
            {
                Town townscript = current_location.GetComponent<Town>();
                temp_diff -= townscript.get_rum_amount();
            }
            else if (rum_dif_int <= 0)
            {
                Town townscript = current_location.GetComponent<Town>();
                temp_diff -= townscript.get_sell_amount_rum();
            }
            //rum_cost.text = temp_diff.ToString();
        }
    }
    public void sub_item(string item)
    {
        if (item.Equals("rum"))
        {
            rum_dif_int -= 1;
            //rum_diff.text = rum_dif_int.ToString();
            if (rum_dif_int >= 0)
            {
                Town townscript = current_location.GetComponent<Town>();
                temp_diff += townscript.get_rum_amount();
            }
            else if (rum_dif_int < 0)
            {
                Town townscript = current_location.GetComponent<Town>();
                temp_diff += townscript.get_sell_amount_rum();
            }
            //rum_cost.text = temp_diff.ToString();
        }
    }
    public void confirm_purchase()
    {
        Town townscript = current_location.GetComponent<Town>();
        townscript.alter_shop_stock(-rum_dif_int);
        //rum_diff.text = ("0");
        //rum_cost.text = ("0");
        rum_cargo_count += rum_dif_int;
        //rum_cargo_amount_text.text = rum_cargo_count.ToString();
        rum_dif_int = 0;
        change_gold(temp_diff);
        temp_diff = 0;
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
        Town townscript = current_location.GetComponent<Town>();
        townscript.hire_crew(num);
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
}

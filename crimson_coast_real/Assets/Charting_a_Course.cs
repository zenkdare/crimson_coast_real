using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.EventSystems;

public class Charting_a_Course : MonoBehaviour
{

    public GameObject boat;
    public NavMeshAgent b_agent;
    public int weekdis;
    public float dis_in_a_week;
    public GameObject canvas;
    public UIManager uiScript;
    private GameObject current_hit;
    // Start is called before the first frame update
    void Start()
    {
        current_hit = null;
        uiScript.updateTownInfo("Journey length: " + "unknown" + "\nRum price: " + "unknown" + "\nSpice price: " + "unknown" + "\nTimber price: " + "unknown" + "\nMedicine price: " + "unknown" + "\nRations price: " + "unknown");
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Create_point_and_line();
            //StartCoroutine("Create_point_and_line");
        }
        Draw_Chart_path.path = b_agent.path.corners;
        if (!(b_agent.pathPending))
        {
            weekdis = (int)(Mathf.Floor(RemainingDistance(b_agent.path.corners)/dis_in_a_week));
            Ship_Movement shipscript = boat.GetComponent<Ship_Movement>();
            if (current_hit != null)
            {
                Town townscript = current_hit.GetComponent<Town>();               
                uiScript.updateTownInfo("Journey length: " + weekdis + " weeks\nRum price: " + townscript.get_buy_amount("rum") + "\nSpice price: " + townscript.get_buy_amount("spice") + "\nTimber price: " + townscript.get_buy_amount("timber") + "\nMedicine price: " + townscript.get_buy_amount("med") + "\nRations price: " +1);
                
            } 
        }
    }

    void Create_point_and_line()
    {
        var ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        RaycastHit hit = new RaycastHit();
        if (EventSystem.current.IsPointerOverGameObject()==false)    // is the touch on the GUI
        {
           
            if (Physics.Raycast(ray, out hit, Mathf.Infinity))
            {
                
                if (hit.transform.tag == "town" || hit.transform.tag == "island")
                {
                    Ship_Movement shipscript = boat.GetComponent<Ship_Movement>();
                    current_hit = hit.transform.gameObject;
                    b_agent.SetDestination(hit.transform.GetChild(1).transform.position);
                    shipscript.set_Port(hit.transform.gameObject);
                    Draw_Chart_path.path = b_agent.path.corners;
                    b_agent.isStopped=true;
                }
            }
        }
    }
    public float RemainingDistance(Vector3[] points)
    {
        if (points.Length < 2) return 0;
        float distance = 0;
        for (int i = 0; i < points.Length - 1; i++)
            distance += Vector3.Distance(points[i], points[i + 1]);
        return distance;
    }
    //method to call for the ui to get the amount of weeks it will take to make a journey
    public int Get_Distance_in_Weeks()
    {
        return weekdis;
    }
    /*
    public void RemoveanItem(Point point)
    {
        int index = points.IndexOf(point);
        if (index == points.Count-1 && index!=0)
        {
            points[index - 1].Added_point(points[index-1]);
        }
        else if (index > 0)
        {
            points[index - 1].Added_point(points[index + 1]);
        }
        points.Remove(point);
        print(points.Count);
    }

    public void Clear_course()
    {
        print("clear is called");
        while(points.Count>0)
        {
            points[0].Clear_point();
        }
    }
    */
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.EventSystems;

public class Charting_a_Course : MonoBehaviour
{

    public GameObject boat;
    public NavMeshAgent b_agent;
    // Start is called before the first frame update
    void Start()
    {
        
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
    }

    void Create_point_and_line()
    {
        print(Input.mousePosition);
        var ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        RaycastHit hit = new RaycastHit();
        if (EventSystem.current.IsPointerOverGameObject()==false)    // is the touch on the GUI
        {
           
            if (Physics.Raycast(ray, out hit, Mathf.Infinity))
            {
                
                if (hit.transform.tag == "town" || hit.transform.tag == "island")
                {
                    
                    b_agent.SetDestination(hit.transform.GetChild(0).transform.position);
                    Ship_Movement shipscript = boat.GetComponent<Ship_Movement>();
                    shipscript.set_Port(hit.transform.gameObject);
                    Draw_Chart_path.path = b_agent.path.corners;
                    b_agent.isStopped=true;
                }
            }
        }
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

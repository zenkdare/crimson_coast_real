using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Charting_a_Course : MonoBehaviour
{
    public GameObject pointPrefab;
    public GameObject linePrefab;
    public List<Point> points = new List<Point>();
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
        }
    }

    void Create_point_and_line()
    {
        print(Input.mousePosition);
        var ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        RaycastHit hit = new RaycastHit();
        print(EventSystem.current.IsPointerOverGameObject());
        if (EventSystem.current.IsPointerOverGameObject()==false)    // is the touch on the GUI
        {
            if (Physics.Raycast(ray, out hit, Mathf.Infinity))
            {
                if (hit.transform.tag == "map")
                {
                    GameObject instance = Instantiate(pointPrefab, hit.point, Quaternion.identity);
                    print(instance.transform);
                    Point marker = instance.GetComponent<Point>();
                    marker.Setchart(this);
                    points.Add(marker);
                    marker.Creation_setup();
                    if (points.Count > 1)
                    {
                        points[points.Count - 2].Added_point(points[points.Count - 1]);
                    }
                }
            }
        }
    }

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
}

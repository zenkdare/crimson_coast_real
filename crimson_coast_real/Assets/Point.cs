using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Point : MonoBehaviour
{
    private LineRenderer line;
    public Charting_a_Course chart;
    // Start is called before the first frame update
    void Start()
    {
        LineRenderer line = GetComponent<LineRenderer>();
        line.positionCount = 2;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Setchart(Charting_a_Course input)
    {
        chart = input;
    }

    private void OnMouseDown()
    {
        Clear_point();
    }
    public void Creation_setup()
    {
        LineRenderer line = GetComponent<LineRenderer>();
        line.SetPosition(0, this.transform.position);
        line.SetPosition(1, this.transform.position);
    }
    public void Added_point(Point second_point)
    {
        LineRenderer line = GetComponent<LineRenderer>();
        line.SetPosition(1, second_point.transform.position);
    }

    public void Clear_point()
    {
        //chart.RemoveanItem(this);
        Destroy(gameObject);
    }

}

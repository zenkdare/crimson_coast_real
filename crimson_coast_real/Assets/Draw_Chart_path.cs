using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Draw_Chart_path : MonoBehaviour
{
    public static Vector3[] path = new Vector3[0];

    private LineRenderer lr;
    public int width;
    public Color chart_color;
    void Start()
    {
        lr = GetComponent<LineRenderer>();
        lr.startWidth = width;
        lr.endWidth = width;
        lr.material.color = chart_color;
    }

    void Update()
    {
        if (path != null && path.Length > 1)
        {
            lr.positionCount = path.Length;
            for (int i = 0; i < path.Length; i++)
            {
                lr.SetPosition(i, path[i]);
            }
        }
    }
}

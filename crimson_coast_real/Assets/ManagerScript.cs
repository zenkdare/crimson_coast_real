using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ManagerScript : MonoBehaviour
{
    public GameObject cam;
    public GameObject start_town;
    public GameObject chart_course_button;
    public GameObject confirm_course_button;
    public GameObject reset_course_button;
    public GameObject set_sail_button;
    public GameObject map;
    public GameObject courseCharter;
    public GameObject current_location;
    public GameObject ship;
    // Start is called before the first frame update
    void Start()
    {
        //SetUpTown(start_town);
        //current_location = start_town;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void SetUpTown(GameObject town)
    {
        CameraScript camscript = cam.GetComponent<CameraScript>();
        camscript.Look_at_Location(town);
        chart_course_button.SetActive(true);
        set_sail_button.SetActive(true);
    }

    public void Chart_a_Course()
    {
        CameraScript camscript = cam.GetComponent<CameraScript>();
        camscript.Look_at_Location(map);
        chart_course_button.SetActive(false);
        set_sail_button.SetActive(false);
        confirm_course_button.SetActive(true);
        reset_course_button.SetActive(true);
        courseCharter.SetActive(true);
    }

    public void Confirm_course()
    {
        CameraScript camscript = cam.GetComponent<CameraScript>();
        camscript.Look_at_Location(current_location);
        chart_course_button.SetActive(true);
        set_sail_button.SetActive(true);
        confirm_course_button.SetActive(false);
        reset_course_button.SetActive(false);
        courseCharter.SetActive(false);
    }

    public void Clear_course()
    {
        Charting_a_Course charter = courseCharter.GetComponent<Charting_a_Course>();
        charter.Clear_course();
    }

    public void Set_Sail()
    {
        CameraScript camscript = cam.GetComponent<CameraScript>();
        Camera_Orbit camorbit = cam.GetComponent<Camera_Orbit>();
        camscript.enabled = false;
        camorbit.enabled = true;
    }

    public void In_To_Port(GameObject location)
    {
        Ship_Movement shipscript = ship.GetComponent<Ship_Movement>();
        shipscript.enabled = false;
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
}

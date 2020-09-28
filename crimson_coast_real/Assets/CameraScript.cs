using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraScript : MonoBehaviour
{
    public float speed;
    Vector3 camspot;
    GameObject lookpoint;
    // Start is called before the first frame update
    void Start()
    {
        speed = 1f;
        //camspot = new Vector3(0, 0, 0);
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = Vector3.Lerp(transform.position, camspot, speed * Time.deltaTime);
        transform.LookAt(lookpoint.transform);
    }

    public void Look_at_Location(GameObject location)
    {
        lookpoint = location;
        camspot=location.transform.GetChild(0).transform.position;
    }

}

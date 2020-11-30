using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class hireButtonScript : MonoBehaviour
{
	public ManagerScript mangScript;
	public crewUIScript crewUI;
    // Start is called before the first frame update
    void Start()
    {
        mangScript = GameObject.FindWithTag("manager").GetComponent<ManagerScript>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void hireCrew(){
    	//Debug.Log("hireIndex:"+crewUI.getIndex());
    	mangScript.hire_crew(crewUI.getIndex());
    	Destroy(crewUI.gameObject);
    }
}

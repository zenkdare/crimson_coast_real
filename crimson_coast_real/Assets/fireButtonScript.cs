using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class fireButtonScript : MonoBehaviour
{
	public ManagerScript mangScript;
    public UIManager uiScript;
	public crewUIScript crewUI;
    // Start is called before the first frame update
    void Start()
    {
        mangScript = GameObject.FindWithTag("manager").GetComponent<ManagerScript>();
        uiScript = GameObject.FindWithTag("canvas").GetComponent<UIManager>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void fireCrew(){
    	//Debug.Log("hireIndex:"+crewUI.getIndex());
    	mangScript.fire_crew(crewUI.getIndex());
    }
}

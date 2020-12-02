using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class crewUIScript : MonoBehaviour
{
	public int crewIndex;
	//public GameObject crewUIGameObject;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public GameObject getGameObject(){
    	return gameObject;
    }

    public int getIndex(){
    	return crewIndex;
    }

    public void setIndex(int i){
    	crewIndex = i;
    }
}

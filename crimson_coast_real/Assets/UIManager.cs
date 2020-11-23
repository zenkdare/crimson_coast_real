using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
	public Text goldText;
	public Text crewCountText;
	//public list townCrew;
	//public list playerCrew;
	public Text eventTitle;
	public Text eventText;
	public Text eventOption1;
	public Text eventOption2;
	public GameObject townUI;
	public GameObject confirmCourseUI;
	public GameObject marketUI;
	public GameObject tavernUI;
	public GameObject eventUI;
    public GameObject Manager;
    public ManagerScript managScript;

    // Start is called before the first frame update
    void Start()
    {
        //managScript = Manager.GetComponent<ManagerScript>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void TownUI(bool active){
    	townUI.SetActive(active);
    	if (active){
    		//set cargo
    	}
    }

    public void ChartUI(bool active){
    	confirmCourseUI.SetActive(active);
    }

    public void MarketUI(bool active){
    	marketUI.SetActive(active);
    }

    public void TavernUI(bool active){
    	tavernUI.SetActive(active);
    }

    public void EventUI(bool active){
    	eventUI.SetActive(active);
    }

    public void updateGold(int gold){
    	goldText.text = ("Gold: " + gold);
    }

    public void updateCrewCount(int crew){
    	crewCountText.text = ("Crew Count: " + crew);
    }

}

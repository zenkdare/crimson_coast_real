using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{

	public Text goldText;
	public Text crewCountText;
	private Text eventTitle;
	private Text eventText;
	private Text eventOption1;
	private Text eventOption2;
	private Text eventDesc1;
	private Text eventDesc2;
	public GameObject townUI;
	public GameObject confirmCourseUI;
	public GameObject marketUI;
	public GameObject tavernUI;
	public GameObject eventUI;
	public GameObject Crew1;

	// public float widthSmooth = 4.6f;
	// public float heightSmooth = 4.6f;
	// public float textSmooth = 0.1f;
	// public bool widthOpen = false;
	// public bool heightOpen = false;

    // Start is called before the first frame update
    void Start()
    {
    	eventTitle = eventUI.transform.Find("Title").GetComponent<Text>();
    	eventText = eventUI.transform.Find("Main").GetComponent<Text>();
    	eventOption1 = eventUI.transform.Find("Buttons/Button1/Text").GetComponent<Text>();
    	eventOption2 = eventUI.transform.Find("Buttons/Button2/Text").GetComponent<Text>();
    	eventDesc1 = eventUI.transform.Find("Buttons/Button1/Description/Text").GetComponent<Text>();
    	eventDesc2 = eventUI.transform.Find("Buttons/Button2/Description/Text").GetComponent<Text>();
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

    public void updateMarket(string item, string index, int value){
    	Text textbox = marketUI.transform.Find(item+"/"+index).GetComponent<Text>();
    	textbox.text = value.ToString();
    }

    public void updateTavern(List<Crew> local_crew){
    	if (local_crew.Count == 0){
    		Crew1.SetActive(false);
    	}
    	else {
	    	Crew crew = local_crew[0];
			Text textbox = tavernUI.transform.Find("Crew1/Name").GetComponent<Text>();
			textbox.text = crew.get_name();
			textbox = tavernUI.transform.Find("Crew1/trait1").GetComponent<Text>();
			textbox.text = crew.get_t1();
			textbox = tavernUI.transform.Find("Crew1/trait2").GetComponent<Text>();
			textbox.text = crew.get_t2();
			textbox = tavernUI.transform.Find("Crew1/cost").GetComponent<Text>();
			textbox.text = crew.get_cost().ToString();
		}
    }

    public void updateEvent(string title, string info, string option1, string option2, string option1Desc, string option2Desc){
    	eventTitle.text = title;
    	eventText.text = info;
    	eventOption1.text = option1;
    	eventOption2.text = option2;
    	eventDesc1.text = option1Desc;
    	eventDesc2.text = option2Desc;
    }

    // public void resetPopup(){
    // 	widthOpen = false;
    // 	heightOpen = false;
    // }

}

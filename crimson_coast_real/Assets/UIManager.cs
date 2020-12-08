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
	private Text eventResultText;
	private Text weekInfoText;
    private Text townInfoText;
    private Text winText;
    private Text loseText;
    private Text errorText;
    private float errorTime;
    private bool error = false;
	public GameObject tavExitButton;
	public GameObject tavList;
	public GameObject crewExitButton;
	public GameObject shipCrewList;
	public GameObject townUI;
	public GameObject confirmCourseUI;
    public GameObject confirmCourseReportUI;
	public GameObject marketUI;
	public GameObject tavernUI;
	public GameObject shipCrewUI;
    public GameObject crewListUI;
	public GameObject eventUI;
	public GameObject eventResultUI;
	public GameObject weekInfoUI;
    public GameObject cargoUI;
    public GameObject townInfoUI;
    public GameObject errorUI;
    public GameObject winUI;
    public GameObject loseUI;
	public GameObject CrewUITav;
	public GameObject CrewUIShip;
    public GameObject CrewUIList;
    public GameObject Manager;
    public ManagerScript managerScript;
    private List<GameObject> townCrew = new List<GameObject>();
    private List<GameObject> shipCrew = new List<GameObject>();
    private List<GameObject> crewListCrew = new List<GameObject>();

    // Start is called before the first frame update
    void Start()
    {
    	eventTitle = eventUI.transform.Find("Title").GetComponent<Text>();
    	eventText = eventUI.transform.Find("Main").GetComponent<Text>();
    	eventOption1 = eventUI.transform.Find("Buttons/Button1/Text").GetComponent<Text>();
    	eventOption2 = eventUI.transform.Find("Buttons/Button2/Text").GetComponent<Text>();
    	eventDesc1 = eventUI.transform.Find("Buttons/Button1/Description/Text").GetComponent<Text>();
    	eventDesc2 = eventUI.transform.Find("Buttons/Button2/Description/Text").GetComponent<Text>();
    	eventResultText = eventResultUI.transform.Find("Main").GetComponent<Text>();
    	weekInfoText = weekInfoUI.transform.Find("Main").GetComponent<Text>();
        townInfoText = townInfoUI.transform.Find("Text").GetComponent<Text>();
    	//tavExitButton = tavernUI.transform.GetChild(1).gameObject;
    	//tavList = tavernUI.transform.GetChild(0).gameObject;
    	//crewExitButton = shipCrewUI.transform.GetChild(1).gameObject;
    	//shipCrewList = shipCrewUI.transform.GetChild(0).gameObject;
        winText = winUI.transform.Find("Main").GetComponent<Text>();
        loseText = loseUI.transform.Find("Main").GetComponent<Text>();
        errorText = errorUI.transform.Find("Text").GetComponent<Text>();
    }

    // Update is called once per frame
    void Update()
    {
        if (error && (Time.time - errorTime > 5)){
            error = false;
            errorUI.SetActive(false);
        }
    }

    public void TownUI(bool active){
    	townUI.SetActive(active);
    }

    public void ChartUI(bool active){
    	confirmCourseUI.SetActive(active);
    }

    public void ChartReportUI(bool active){
        confirmCourseReportUI.SetActive(active);
    }

    public void MarketUI(bool active){
    	marketUI.SetActive(active);
    }

    public void TavernUI(bool active){
    	tavernUI.SetActive(active);
    }

    public void ShipCrewUI(bool active){
    	shipCrewUI.SetActive(active);
    }

    public void CrewListUI(bool active){
       crewListUI.SetActive(active);
    }

    public void EventUI(bool active){
    	eventUI.SetActive(active);
    }

    public void EventResultUI(bool active){
    	eventResultUI.SetActive(active);
    }

    public void WeekInfoUI(bool active){
    	weekInfoUI.SetActive(active);
    }

    public void CargoUI(bool active){
        cargoUI.SetActive(active);
    }

    public void TownInfoUI(bool active){
        townInfoUI.SetActive(active);
    }

    public void WinUI(bool active){
        winUI.SetActive(active);
    }

    public void LoseUI(bool active){
        loseUI.SetActive(active);
    }

    public void winDisp(string winString){
        winText.text = winString;
        WinUI(true);
    }

    public void loseDisp(string loseString){
        loseText.text = loseString;
        LoseUI(true);
    }

    public void ErrorDisp(string errorString){
        errorText.text = errorString;
        errorTime = Time.time;
        error = true;
        errorUI.SetActive(true);
    }

    public void updateGold(int gold){
    	goldText.text = ("Gold: " + gold);
    }

    public void updateCrewCount(int crew, int maxCrew){
    	crewCountText.text = ("Crew Count: " + crew + " / " + maxCrew);
    }

    public void updateMarket(string item, string index, int value){
    	Text textbox = marketUI.transform.Find(item+"/"+index).GetComponent<Text>();
    	textbox.text = value.ToString();
    }

    public void updateTavern(List<Crew> local_crew){//spawns in crewmate listings for tavern
    	//make a loop here for each crew member in the list
        DestroyCrewTav();
    	crewExitButton.SetActive(false);
    	tavExitButton.SetActive(true);
    	for (int i = 0; i < local_crew.Count; i++){
    		Crew crewmate = local_crew[i];
    		GameObject crewUI = GameObject.Instantiate(CrewUITav, tavList.transform) as GameObject;
    		townCrew.Add(crewUI);
    		//set index for hire button
    		crewUI.GetComponent<crewUIScript>().setIndex(i);
    		//update text to reflect the stats of a given crewmate
			Text textbox = crewUI.transform.Find("Name").GetComponent<Text>();
			textbox.text = crewmate.get_name();
			textbox = crewUI.transform.Find("trait1").GetComponent<Text>();
			textbox.text = crewmate.get_t1();
			textbox = textbox.transform.Find("description/Text").GetComponent<Text>();
			textbox.text = crewmate.get_t1Desc();
			textbox = crewUI.transform.Find("trait2").GetComponent<Text>();
			textbox.text = crewmate.get_t2();
			textbox = textbox.transform.Find("description/Text").GetComponent<Text>();
			textbox.text = crewmate.get_t2Desc();
			textbox = crewUI.transform.Find("cost").GetComponent<Text>();
			textbox.text = crewmate.get_cost().ToString();
		}
    }

    public void DestroyCrewTav(){
    	tavExitButton.SetActive(false);
    	crewExitButton.SetActive(true);
 		GameObject curCrew;
	    for(int i = 0; i < townCrew.Count; i++)
	    {
	    	//Debug.Log("townCrew: "+i);
	    	curCrew = townCrew[i];
	    	//townCrew.Remove(curCrew);
	        Destroy(curCrew);
	    }
	    townCrew.Clear();
 	}

 	public void DestroyCrewTav(GameObject curCrew){
    	townCrew.Remove(curCrew);
        Destroy(curCrew);
 	}

    public void addCrew(Crew crewmate){//adds crew member to ship UI
		GameObject crewUI = GameObject.Instantiate(CrewUIShip, shipCrewList.transform) as GameObject;
		shipCrew.Add(crewUI);
		//set index for fire button
		crewUI.GetComponent<crewUIScript>().setIndex(shipCrew.IndexOf(crewUI));
		//update text to reflect the stats of a given crewmate
		Text textbox = crewUI.transform.Find("Name").GetComponent<Text>();
		textbox.text = crewmate.get_name();
		textbox = crewUI.transform.Find("trait1").GetComponent<Text>();
		textbox.text = crewmate.get_t1();
		textbox = textbox.transform.Find("description/Text").GetComponent<Text>();
		textbox.text = crewmate.get_t1Desc();
		textbox = crewUI.transform.Find("trait2").GetComponent<Text>();
		textbox.text = crewmate.get_t2();
		textbox = textbox.transform.Find("description/Text").GetComponent<Text>();
		textbox.text = crewmate.get_t2Desc();
		textbox = crewUI.transform.Find("cost").GetComponent<Text>();
		textbox.text = crewmate.get_cost().ToString();


        //adds crew to crewList UI        
        crewUI = GameObject.Instantiate(CrewUIList, crewListUI.transform) as GameObject;
        crewListCrew.Add(crewUI);
        //set index for confirm button
        crewUI.GetComponent<crewUIScript>().setIndex(crewListCrew.IndexOf(crewUI));
        //update text to reflect the stats of a given crewmate
        textbox = crewUI.transform.Find("Name").GetComponent<Text>();
        textbox.text = crewmate.get_name();
        textbox = crewUI.transform.Find("trait1").GetComponent<Text>();
        textbox.text = crewmate.get_t1();
        textbox = textbox.transform.Find("description/Text").GetComponent<Text>();
        textbox.text = crewmate.get_t1Desc();
        textbox = crewUI.transform.Find("trait2").GetComponent<Text>();
        textbox.text = crewmate.get_t2();
        textbox = textbox.transform.Find("description/Text").GetComponent<Text>();
        textbox.text = crewmate.get_t2Desc();
        textbox = crewUI.transform.Find("cost").GetComponent<Text>();
        textbox.text = crewmate.get_cost().ToString();
    }

 	public void DestroyCrewShip(GameObject curCrew){
        int index= crewListCrew.IndexOf(curCrew);
        crewListCrew.Remove(curCrew);
        Destroy(curCrew);
        curCrew = shipCrew[index];
        shipCrew.Remove(curCrew);
        Destroy(curCrew);
 	}

    public void updateEvent(string title, string info, string option1, string option2, string option1Desc, string option2Desc){
    	eventTitle.text = title;
    	eventText.text = info;
    	eventOption1.text = option1;
    	eventOption2.text = option2;
    	eventDesc1.text = option1Desc;
    	eventDesc2.text = option2Desc;
    }

    public void eventResult(string info){
    	eventResultText.text = info;
    }

    public void weekInfoDisp(string info){
    	weekInfoText.text = info;
    }

    public void updateCargo(string item, string index, int value){
        Text textbox = cargoUI.transform.Find(item+"/"+index).GetComponent<Text>();
        textbox.text = value.ToString();
    }

    public void cargoUpdate(){
        int stock = managerScript.get_cargo_rum() + managerScript.get_cargo_spice() + managerScript.get_cargo_timber() + managerScript.get_cargo_med() + managerScript.get_cargo_rations();
        int cargo = 0;
        //set cargo UI to how many of each item is owned
        updateCargo("Rum", "Stock", managerScript.get_cargo_rum());
        updateCargo("Spice", "Stock", managerScript.get_cargo_spice());
        updateCargo("Timber", "Stock", managerScript.get_cargo_timber());
        updateCargo("Medicine", "Stock", managerScript.get_cargo_med());
        updateCargo("Rations", "Stock", managerScript.get_cargo_rations());
        //set cargo UI to how much space each item uses
        updateCargo("Rum", "Cargo", 0);
        updateCargo("Spice", "Cargo", 0);
        updateCargo("Timber", "Cargo", 0);
        updateCargo("Medicine", "Cargo", 0);
        updateCargo("Rations", "Cargo", 0);
        //set cargo UI totals
        updateCargo("Footer", "Stock", stock);
        updateCargo("Footer", "Cargo", cargo);
    }

    public void updateTownInfo(string info){
        townInfoText.text = info;
    }

}

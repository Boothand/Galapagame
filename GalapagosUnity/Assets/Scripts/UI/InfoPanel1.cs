using UnityEngine;
using UnityEngine.UI;

public class InfoPanel1 : MonoBehaviour
{
	public Text selectedObjectName;
	public Text infoField1;
	public Text infoField2;
	public Text factionText;
	public CanvasGroup workstationButtons;
	public CanvasGroup fishboatButtons;
	public CanvasGroup missionButtons;
	public CanvasGroup bazaarButtons;

	public GameObject panelGameobject;
	public GameObject missionPanel;
	public Button exitMission;


	void Start ()
	{
		panelGameobject.SetActive(false);
		exitMission.enabled = false;
		exitMission.gameObject.SetActive(false);
		missionPanel.SetActive(false);
	}

	public void FishBoatBuyWorker()
	{
		GameManager.selectedObject.GetComponent<Fisherboat>().BuyWorker();
	}

	public void SellFish()
	{
		GameManager.selectedObject.GetComponent<Bazaar>().SellAllFish();
	}

	public void CheckMissions()
	{
		panelGameobject.SetActive(true);
		exitMission.enabled = true;
		exitMission.gameObject.SetActive(true);
	}

	public void ExitMissionPanel()
	{
		panelGameobject.SetActive(false);
		exitMission.enabled = false;
		exitMission.gameObject.SetActive(false);
	}

	public void exitMisionLogPanel()
	{
		missionPanel.SetActive(false);
		missionPanel.gameObject.transform.FindChild("SurvivalMission").gameObject.transform.FindChild("Mission Name").GetComponent<Text>().enabled = false;
		missionPanel.gameObject.transform.FindChild("FishingProblems").gameObject.transform.FindChild("Mission Name").GetComponent<Text>().enabled = false;
		missionPanel.gameObject.transform.FindChild("Timeline").gameObject.transform.FindChild("Mission Name").GetComponent<Text>().enabled = false;

		missionPanel.gameObject.transform.FindChild("SurvivalMission").gameObject.transform.FindChild("Survival of factions").gameObject.transform.FindChild("Text").GetComponent<Text>().enabled = false;
		missionPanel.gameObject.transform.FindChild("FishingProblems").gameObject.transform.FindChild("Fishermens impact").gameObject.transform.FindChild("Text").GetComponent<Text>().enabled = false;
		missionPanel.gameObject.transform.FindChild("Timeline").gameObject.transform.FindChild("Time waits for no one").gameObject.transform.FindChild("Text").GetComponent<Text>().enabled = false;
	}

	public void MissionLog1()
	{
		missionPanel.SetActive(true);
		missionPanel.gameObject.transform.FindChild("SurvivalMission").gameObject.transform.FindChild("Mission Name").GetComponent<Text>().text = missionPanel.gameObject.transform.FindChild("SurvivalMission").gameObject.transform.FindChild("Survival of factions").name;
		missionPanel.gameObject.transform.FindChild("SurvivalMission").gameObject.transform.FindChild("Mission Name").GetComponent<Text>().enabled = true;
		missionPanel.gameObject.transform.FindChild("SurvivalMission").gameObject.transform.FindChild("Survival of factions").gameObject.transform.FindChild("Text").GetComponent<Text>().enabled = true;
	}

	public void MissionLog2()
	{
		missionPanel.SetActive(true);
		missionPanel.gameObject.transform.FindChild("FishingProblems").gameObject.transform.FindChild("Mission Name").GetComponent<Text>().text = missionPanel.gameObject.transform.FindChild("FishingProblems").gameObject.transform.FindChild("Fishermens impact").name;
		missionPanel.gameObject.transform.FindChild("FishingProblems").gameObject.transform.FindChild("Mission Name").GetComponent<Text>().enabled = true;
		missionPanel.gameObject.transform.FindChild("FishingProblems").gameObject.transform.FindChild("Fishermens impact").gameObject.transform.FindChild("Text").GetComponent<Text>().enabled = true;
	}

	public void MissionLog3()
	{
		missionPanel.SetActive(true);
		missionPanel.gameObject.transform.FindChild("Timeline").gameObject.transform.FindChild("Mission Name").GetComponent<Text>().text = missionPanel.gameObject.transform.FindChild("Timeline").gameObject.transform.FindChild("Time waits for no one").name;
		missionPanel.gameObject.transform.FindChild("Timeline").gameObject.transform.FindChild("Mission Name").GetComponent<Text>().enabled = true;
		missionPanel.gameObject.transform.FindChild("Timeline").gameObject.transform.FindChild("Time waits for no one").gameObject.transform.FindChild("Text").GetComponent<Text>().enabled = true;
	}

	void Update ()
	{
		//Init
		workstationButtons.alpha = 0f;
		workstationButtons.interactable = false;
		workstationButtons.blocksRaycasts = false;

		bazaarButtons.alpha = 0f;
		bazaarButtons.interactable = false;
		bazaarButtons.blocksRaycasts = false;

		fishboatButtons.alpha = 0f;
		fishboatButtons.interactable = false;
		fishboatButtons.blocksRaycasts = false;

		selectedObjectName.enabled = false;
		infoField1.enabled = false;
		infoField2.enabled = false;
		factionText.enabled = false;

		if (GameManager.selectedObject)
		{
			//Title of the object
			selectedObjectName.enabled = true;
			selectedObjectName.text = GameManager.selectedObject.typeName;

			//Faction text
			if (GameManager.selectedObject.faction != Stats.Faction.None)
			{
				factionText.enabled = true;
				factionText.text = GameManager.selectedObject.faction.ToString() + " faction";
			}
			else
			{
				factionText.enabled = false;
			}

			//Special cases
			if (GameManager.selectedObject.GetComponent<Fisherboat>())
			{
				Fisherboat boat = GameManager.selectedObject.GetComponent<Fisherboat>();

				infoField1.enabled = true;
				infoField1.text = "Workers: " + boat.workers + "/" + boat.workerCapacity;
				infoField2.enabled = true;
				infoField2.text = "Fish: " + boat.fish + "/" + boat.fishCapacity;

				fishboatButtons.alpha = 1f;
				fishboatButtons.interactable = true;
				fishboatButtons.blocksRaycasts = true;
			}
			else if (GameManager.selectedObject.GetComponent<FishZone>())
			{
				FishZone zone = GameManager.selectedObject.GetComponent<FishZone>();

				infoField1.enabled = true;
				infoField1.text = "Fish remaining: " + zone.fishAmount.ToString() + "/" + zone.maxFishAmount.ToString();
			}
			else if (GameManager.selectedObject.GetComponent<Workstation>())
			{
				Workstation station = GameManager.selectedObject.GetComponent<Workstation>();

				workstationButtons.alpha = 1f;
				workstationButtons.interactable = true;
				workstationButtons.blocksRaycasts = true;
				infoField1.enabled = true;
				infoField1.text = "Fish in storage: " + station.fishAmount;
			}
			else if (GameManager.selectedObject.GetComponent<Bazaar>())
			{
				Bazaar bazaar = GameManager.selectedObject.GetComponent<Bazaar>();

				bazaarButtons.alpha = 1f;
				bazaarButtons.interactable = true;
				bazaarButtons.blocksRaycasts = true;

				bazaar.SellAllFish();
				
			}
		}
	}
}
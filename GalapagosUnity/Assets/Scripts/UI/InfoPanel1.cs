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


	void Start ()
	{
		
	}

	public void FishBoatBuyWorker()
	{
		GameManager.selectedObject.GetComponent<Fisherboat>().BuyWorker();
	}

	void Update ()
	{
		//Init
		workstationButtons.alpha = 0f;
		workstationButtons.interactable = false;
		workstationButtons.blocksRaycasts = false;

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

				if (boat.faction == Stats.Faction.Player)
				{
					fishboatButtons.alpha = 1f;
					fishboatButtons.interactable = true;
					fishboatButtons.blocksRaycasts = true;
				}
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
		}
	}
}
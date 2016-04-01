using UnityEngine;
using UnityEngine.UI;

public class InfoPanel1 : MonoBehaviour
{
	public Text selectedObjectName;
	public Text infoField1;
	public Text infoField2;
	public Text factionText;


	void Start ()
	{
		
	}
	
	void Update ()
	{
		//Init
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

				infoField1.enabled = true;
				infoField1.text = "Fish in storage: " + station.fishAmount;
			}
		}
	}
}
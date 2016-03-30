using UnityEngine;
using UnityEngine.UI;

public class InfoPanel1 : MonoBehaviour
{
	public Text selectedObjectName;
	public Text infoField1;
	public Text infoField2;


	void Start ()
	{
		
	}
	
	void Update ()
	{
		if (GameManager.selectedObject)
		{
			selectedObjectName.enabled = true;
			selectedObjectName.text = GameManager.selectedObject.typeName;

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
				infoField2.enabled = false;
			}
			else
			{
				infoField1.enabled = false;
				infoField2.enabled = false;
			}
		}
		else
		{
			selectedObjectName.enabled = false;
			infoField1.enabled = false;
			infoField2.enabled = false;
		}
	}
}
using UnityEngine;
using UnityEngine.UI;

public class InfoPanel1 : MonoBehaviour
{
	public Text selectedObjectName;
	public Text infoField1;
	public Text cargoSpace;


	void Start ()
	{
		
	}
	
	void Update ()
	{
		if (GameManager.selectedObject)
		{
			selectedObjectName.enabled = true;

			if (GameManager.selectedObject.GetComponent<Fisherboat>())
			{
				selectedObjectName.text = GameManager.selectedObject.typeNameFishingBoat;
				infoField1.enabled = true;
				infoField1.text = "Workers: " + GameManager.selectedObject.GetComponent<Fisherboat>().workers + "/" + GameManager.selectedObject.GetComponent<Fisherboat>().workerCapacity;
				cargoSpace.text = "Cargo Space: " + GameManager.selectedObject.GetComponent<Fisherboat>().fish + "/" + GameManager.selectedObject.GetComponent<Fisherboat>().fishCapacity;
			}
			else if (GameManager.selectedObject.GetComponent<GovermentVessels>())
			{
				selectedObjectName.text = GameManager.selectedObject.typeNameGovernmentVessel;
			}
			else if (GameManager.selectedObject.GetComponent<PlayerBuilding>())
			{
				selectedObjectName.text = GameManager.selectedObject.typeNameWorkBuilding;
			}
		}
		else
		{
			selectedObjectName.enabled = false;
			infoField1.enabled = false;
		}
	}
}
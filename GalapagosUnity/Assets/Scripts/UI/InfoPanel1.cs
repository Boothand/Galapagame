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
			selectedObjectName.text = GameManager.selectedObject.typeName;

			if (GameManager.selectedObject.GetComponent<Fisherboat>())
			{
				Fisherboat boat = GameManager.selectedObject.GetComponent<Fisherboat>();
				infoField1.enabled = true;
				infoField1.text = "Workers: " + boat.workers + "/" + boat.workerCapacity;
				cargoSpace.enabled = true;
				cargoSpace.text = "Fish: " + boat.fish + "/" + boat.fishCapacity;
			}
		}
		else
		{
			selectedObjectName.enabled = false;
			infoField1.enabled = false;
			cargoSpace.enabled = false;
		}
	}
}
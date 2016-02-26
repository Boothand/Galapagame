using UnityEngine;
using UnityEngine.UI;

public class InfoPanel1 : MonoBehaviour
{
	public Text selectedObjectName;
	public Text infoField1;

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
				infoField1.enabled = true;
				infoField1.text = GameManager.selectedObject.GetComponent<Fisherboat>().workers + "/" + GameManager.selectedObject.GetComponent<Fisherboat>().workerCapacity;
			}
		}
		else
		{
			selectedObjectName.enabled = false;
			infoField1.enabled = false;
		}
	}
}
using UnityEngine;

public class GameManager : MonoBehaviour
{
	public static Stats selectedObject;
	public TimeScript eventTimer;

	
	void Start ()
	{
	
	}
	
	void Update ()
	{
		if (eventTimer.currentDay == eventTimer.daysinmonth)
		{
			//utfør event her.
		}
	}
}
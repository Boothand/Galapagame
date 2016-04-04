using UnityEngine;

public class GameManager : MonoBehaviour
{
	public static Stats selectedObject;
	public TimeScript eventTimer;

	int eventDay;

	void Start ()
	{
		eventDay = eventTimer.daysinmonth;
	}
	
	void Update ()
	{

	}


	


}
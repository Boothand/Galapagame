using UnityEngine;

public class GameManager : MonoBehaviour
{
	public static Stats selectedObject;
	public TimeScript eventTimer;

	public FactionScript player;
	public FactionScript blue;
	public FactionScript red;
	public FactionScript green;
	public GameObject escapeMenu;

	bool monthlyEventHasHappened;

	void Start ()
	{
		escapeMenu.SetActive(false);
	}

	public void GameOver()
	{
		print("You lose.");
		Camera.main.enabled = false;
		//Ting
	}
	
	void Update ()
	{
		if (eventTimer.currentDay == eventTimer.daysinmonth && !monthlyEventHasHappened)
		{
			player.MonthlyPay();
			blue.MonthlyPay();
			red.MonthlyPay();
			green.MonthlyPay();
			monthlyEventHasHappened = true;
		}
		if (eventTimer.currentDay != eventTimer.daysinmonth)
		{
			monthlyEventHasHappened = false;
		}

		if (Input.GetKeyDown(KeyCode.Escape))
		{
			escapeMenu.SetActive(!escapeMenu.activeSelf);
		}
	}
}
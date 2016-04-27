﻿using UnityEngine;

public class GameManager : MonoBehaviour
{
	public static Stats selectedObject;
	public TimeScript eventTimer;

	public FactionScript player;
	public FactionScript blue;
	public FactionScript red;
	public FactionScript green;

	bool monthlyEventHasHappened;

	void Start ()
	{
	
	}
	
	void Update ()
	{
		if (eventTimer.currentDay == eventTimer.daysinmonth && !monthlyEventHasHappened)
		{
			player.RemoveMoney(player.monthlyDebt);
			monthlyEventHasHappened = true;
		}
	}
}
using UnityEngine;
using System.Collections;

public class FactionScript : MonoBehaviour
{

	public Stats.Faction faction;

	public Boat[] boats;
	//public Workstation[] workstations;
	
	public int totalMoney;
	public int totalFish;
	public int totalWorkers;

	public int totalBoats;
	public int totalWorkstations;

	public int monthlyDebt;


	void Start () 
	{
		totalWorkstations = 1;
		totalBoats = 1;
		totalWorkers = 5;
		totalMoney = 1000;
	}

	public void AddDebt(int amount)
	{
		monthlyDebt += amount;
	}

	public void MonthlyPay()
	{
		if (totalMoney >= monthlyDebt)
		{
			totalMoney -= monthlyDebt;
			monthlyDebt = 0;
		}
		else
		{
			monthlyDebt -= totalMoney;
			totalMoney = 0;
			print("Workers are so mad at " + faction.ToString());
		}
	}

	// Update is called once per frame
	void Update () 
	{
	
	}
}

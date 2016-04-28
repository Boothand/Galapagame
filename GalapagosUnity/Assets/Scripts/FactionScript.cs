using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class FactionScript : MonoBehaviour
{

	public Stats.Faction faction;

	public List<Fisherboat> boats;
	public List<Workstation> workstations;
	
	public int totalMoney;
	public int totalFish;
	public int totalWorkers;

	public int totalBoats;
	public int totalWorkstations;

	public int monthlyDebt;

	int strikes;


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
		if (totalMoney < monthlyDebt)
		{
			strikes++;
			print("Workers are so mad at " + faction.ToString());
		}

		totalMoney -= monthlyDebt;

		if (strikes > 2)
		{
			GameObject.Find("GameManager").GetComponent<GameManager>().GameOver();
		}
	}

	// Update is called once per frame
	void Update () 
	{
	
	}
}

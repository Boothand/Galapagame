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
	public int workerMadLevel;
	public int firedWorkers;
	[HideInInspector] public int workerSalary = 250;

	public GameObject noteText;

	void Start () 
	{
		workerSalary = 250;
		totalWorkstations = 1;
		totalBoats = 1;
		totalWorkers = 5;
		totalMoney = 5000;

		Fisherboat[] allBoats = GameObject.FindObjectsOfType<Fisherboat>();

		foreach (Fisherboat boat in allBoats)
		{
			if (boat.myFaction == this)
			{
				boats.Add(boat);
			}
		}

	}

	public void AddDebt(int amount)
	{
		monthlyDebt += amount;
	}

	public void MonthlyPay()
	{
		if (totalMoney < monthlyDebt)
		{
			//strikes++;
			workerMadLevel++;
			print("Workers are so mad at " + faction.ToString());

			if (workerMadLevel > 0)
			{
				WorkersQuit();
				//GameObject.Find("GameManager").GetComponent<GameManager>().GameOver();
			}
		}

		totalMoney -= monthlyDebt;

		
	}

	public void WorkersQuit()
	{
		int totalWorkersToLeave = 0;

		foreach (Fisherboat boat in boats)
		{
			int workersToLeave = boat.workers - Mathf.FloorToInt(boat.workers / workerMadLevel);
			totalWorkersToLeave += workersToLeave;

			boat.workers -= workersToLeave;
			AddDebt(workersToLeave * -workerSalary);
		}
		if (totalWorkersToLeave > 0)
		{
			if (faction == Stats.Faction.Player)
			{
				GameObject noteInstance = Instantiate(noteText, noteText.transform.position, Quaternion.identity) as GameObject;
				noteInstance.transform.parent = GameObject.Find("UI").transform;
				noteInstance.transform.localPosition = noteText.transform.position;
				noteInstance.GetComponent<NotificationText>().text.text = totalWorkersToLeave + " workers left " + faction.ToString() + " due to anger level: " + workerMadLevel;
			}
		}
	}


	void Update () 
	{
	
	}
}

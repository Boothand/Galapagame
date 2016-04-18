using UnityEngine;
using System.Collections;

public class FactionScript : MonoBehaviour {

	public Stats.Faction faction;

	int totalMoney;
	int totalFish;
	int totalWorkers;

	int totalBoats;
	int totalWorkstations;

	// Use this for initialization
	void Start () 
	{
		totalWorkstations = 1;
		totalBoats = 1;
		totalWorkers = 5;
		totalMoney = 1000;
	}


	// Update is called once per frame
	void Update () 
	{
	
	}
}

using UnityEngine;

public class Workstation : OwnableStructure
{
	public int fishAmount;
	public int companyMoney;
	public GameObject boatPrefab;
	public Transform boatSpawnPos;

	void Start()
	{
		base.BaseStart();

		companyMoney = 1000;

	}
	
	void Update()
	{
		base.BaseUpdate();
	}

	public void SellFish()
	{
		int moneyGain = fishAmount * 4;

		stats.ownerFaction.totalMoney += moneyGain;
		fishAmount = 0;
	}

	public void BuyBoat()
	{
		int boatPrice = 2000;
		if (stats.ownerFaction.totalMoney >= boatPrice)
		{
			stats.ownerFaction.totalMoney -= boatPrice;
		}

		GameObject boatInstance = Instantiate(boatPrefab, boatSpawnPos.position, Quaternion.identity) as GameObject;

		Fisherboat boat = boatInstance.GetComponent<Fisherboat>();

		boat.stats.faction = stats.faction;
		boat.fish = 0;
		boat.workers = 0;
	}

	void OnCollisionEnter(Collision col)
	{
		if (col.transform.GetComponent<Fisherboat>() &&
			col.transform.GetComponent<Fisherboat>().stats.faction == stats.faction)
		{
			fishAmount += col.transform.GetComponent<Fisherboat>().fish;
			col.transform.GetComponent<Fisherboat>().fish = 0;
		}
	}
}
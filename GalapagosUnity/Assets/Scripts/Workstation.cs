using UnityEngine;

public class Workstation : OwnableStructure
{
	public int fishAmount;
	public int companyMoney;

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
using UnityEngine;

public class Workstation : OwnableStructure
{
	public float fishAmount;
	public float companyMoney;

	void Start()
	{
		base.BaseStart();

		companyMoney = 1000;

	}
	
	void Update()
	{
		base.BaseUpdate();
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
using UnityEngine;

public class Workstation : OwnableStructure
{
	public float fishAmount;
	Stats stats2;

	void Start()
	{
		base.BaseStart();
		stats2 = GetComponent<Stats>();
	}
	
	void Update()
	{
		base.BaseUpdate();
	}

	void OnCollisionEnter(Collision col)
	{
		if (col.transform.GetComponent<Fisherboat>().stats.faction == stats2.faction)
		{
			fishAmount += col.transform.GetComponent<Fisherboat>().fish;
			col.transform.GetComponent<Fisherboat>().fish = 0;
		}
	}
}
using UnityEngine;
using System.Collections;


public class Fisherboat : Boat
{
	public int workerCapacity = 10;
	public int fishCapacity = 1000;
	public int fish = 0;
	public int workers;

	int fishGain = 0;
	int fishSpaceLeft = 0;

	bool stopFishing;

	void Start ()
	{
	}
	
	void Update ()
	{
		fishSpaceLeft = fishCapacity - fish;
	}

	void OnCollisionStay(Collision col)
	{
		if (col.gameObject.tag == "FishingZoneSmall" && this.gameObject.tag == "FishingBoat")
		{
			StartCoroutine(fishing());
		}
		if (col.gameObject.tag == "FishingZoneBig" && this.gameObject.tag == "FishingBoat")
		{
			StartCoroutine(fishingBig());
		}
	}

	void OnCollisionExit(Collision col)
	{
		if (col.gameObject.tag == "FishingZoneSmall" && this.gameObject.tag == "FishingBoat" || col.gameObject.tag == "FishingZoneBig" && this.gameObject.tag == "FishingBoat")
		{
			stopFishing = true;
		}
	}

	IEnumerator fishing ()
	{
		fishGain = 5;

		while (fishSpaceLeft > 0)
		{
			fish += fishGain;

			if (stopFishing == true)
				yield break;

			yield return new WaitForSeconds(1);
		}
	}

	IEnumerator fishingBig()
	{
		fishGain = 12;

		while (fishSpaceLeft > 0)
		{
			fish += fishGain;
			if (fishSpaceLeft < fishGain)
			{
				fish += fishSpaceLeft;
				yield break;
			}

			if (stopFishing == true)
				yield break;

			yield return new WaitForSeconds(2);
		}
	}
}
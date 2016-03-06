using UnityEngine;
using System.Collections;


public class Fisherboat : Boat
{
	public int workerCapacity = 10;
	public int fishCapacity = 1000;
	public int fish = 0;
	public int workers;

	public int fishGainDelay = 1;
	public int fishGainSmall = 5;
	public int fishGainBig = 12;
	int fishSpaceLeft = 0;

	void Start ()
	{

	}

	void OnCollisionEnter(Collision col)
	{
		if (col.transform.GetComponent<FishZone>())
		{
			StartCoroutine(GainFish(fishGainSmall, fishGainDelay, col.transform.GetComponent<FishZone>()));
		}
	}

	void OnCollisionExit(Collision col)
	{
		if (col.transform.GetComponent<FishZone>())
		{
			StopCoroutine("GainFish");
		}
	}

    IEnumerator GainFish(int amount, float delay, FishZone zone)
    {
		while (fishSpaceLeft > 0)
		{
			fish += zone.GetFished(workers);

			yield return new WaitForSeconds(delay);
		}
	}

	void Update()
	{
		fishSpaceLeft = fishCapacity - fish;

		if (fishSpaceLeft < 0 ||
			fish > fishCapacity)
		{
			fishSpaceLeft = 0;
			fish = fishCapacity;
		}
	}
}
using UnityEngine;
using System.Collections;


public class Fisherboat : Boat
{
	[SerializeField]
	Transform net;
	public bool isFishing;

	public Stats stats;

	public int workerCapacity = 10;
	public int fishCapacity = 1000;
	public int fish = 0;
	public int workers = 5;

	public int fishGainDelay = 1;
	int fishSpaceLeft = 0;

	IEnumerator fishRoutine;

	void Start ()
	{
		stats = GetComponent<Stats>();
		base.BaseStart();
		if (!net)
		{
			net = transform.FindChild("Net");
		}

		net.gameObject.SetActive(false);
	}


	void OnCollisionStay(Collision col)
	{
		if (col.transform.GetComponent<FishZone>())
		{
			if (fishSpaceLeft > 0 && !GetComponent<Pathfinder>().hasTarget)
			{
				if (!isFishing)
				{
					fishRoutine = GainFish(fishGainDelay, col.transform.GetComponent<FishZone>());
					StartCoroutine(fishRoutine);
				}
			}
			else
			{
				if (fishRoutine != null)
				{
					StopCoroutine(fishRoutine);
					isFishing = false;
					net.gameObject.SetActive(false);
				}
			}
		}
	}

	void OnCollisionExit(Collision col)
	{
		if (col.transform.GetComponent<FishZone>())
		{
			net.gameObject.SetActive(false);
			StopCoroutine(fishRoutine);
		}
	}

    IEnumerator GainFish(float delay, FishZone zone)
    {
		while (fishSpaceLeft > 0)
		{
			net.gameObject.SetActive(true);
			isFishing = true;
			fish += zone.GetFished(workers);

			yield return new WaitForSeconds(delay);
		}
	}

	public void BuyWorker()
	{
		if (workers < workerCapacity)
		{
			workers++;
			//Tap penga, referanse til factionskript.
		}
	}

	void Update()
	{
		base.BaseUpdate();

		fishSpaceLeft = fishCapacity - fish;

		if (fishSpaceLeft < 0 ||
			fish > fishCapacity)
		{
			fishSpaceLeft = 0;
			fish = fishCapacity;
		}
	}
}
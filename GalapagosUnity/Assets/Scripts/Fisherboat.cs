using UnityEngine;
using System.Collections;


public class Fisherboat : Boat
{
	[SerializeField]
	Transform net;
	public bool isFishing;
	public int workerCapacity = 10;
	public int fishCapacity = 25;
	public int fish = 0;
	public int workers = 5;

	public int fishGainDelay = 1;
	int fishSpaceLeft = 0;

	IEnumerator fishRoutine;

	//boat AI variables

	[SerializeField]
	FishZone[] fiskesoner = new FishZone[7];

	Pathfinder path;

	float ZoneDistanceOld;
	float ZoneDistanceNew;
	float workshopDistanceOld;
	float workshopDistanceNew;
	
	Transform oldZone;
	Transform NewZone;
	Transform useZone;
	Transform oldWorkshop;
	Transform newWorkshop;
	Transform useWorkshop;


	int currentZone = 0;
	int currentWorkshop = 0;
	bool currentlyFishing;

	void Awake()
	{
		base.BaseStart();

	}

	void Start ()
	{
		path = GetComponent<Pathfinder>();
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
			myFaction.AddDebt(531);
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

		if (myFaction != gameManager.player)
		{
			if (!currentlyFishing)
			{
				isBoatEmpty();
			}

			if (fish >= fishCapacity)
			{
				currentlyFishing = false;
				findBase();
			}
		}

	}


	void goToZone()
	{
		//looping through all fishzones
		foreach (FishZone soner in fiskesoner)
		{
			if (!oldZone)
			{
				oldZone = soner.transform;
				currentZone++;
			}
			else
			{
				NewZone = soner.transform;

				ZoneDistanceOld = Mathf.Sqrt(Mathf.Pow(oldZone.position.x - this.transform.position.x, 2) + Mathf.Pow(oldZone.position.y - this.transform.position.y, 2));
				ZoneDistanceNew = Mathf.Sqrt(Mathf.Pow(NewZone.position.x - this.transform.position.x, 2) + Mathf.Pow(NewZone.position.y - this.transform.position.y, 2));

				if (ZoneDistanceOld < ZoneDistanceNew)
				{
					useZone = oldZone;
					currentZone++;
				}
				else if (ZoneDistanceNew < ZoneDistanceOld)
				{
					oldZone = NewZone;
					useZone = NewZone;
					currentZone++;
				}
			}

			if (currentZone == fiskesoner.Length)
			{
				Vector3 fishZoneTarget = new Vector3(useZone.position.x, useZone.position.y, useZone.position.z);
				//print(fishZoneTarget);
				path.GoToPos(fishZoneTarget);
				currentZone = 0;
				currentlyFishing = true;
			}
		}
	}

	void isBoatEmpty()
	{
		// checks for boats that can be sent to a zone.
		//this one must be done before goToZone()

		if (fish < fishCapacity)
		{
			goToZone();
		}
		else
			return;
	}

	//void fullBoat()
	//{
	//	if (fish == fishCapacity)
	//	{
	//		findBase();
	//		currentlyFishing = true;
	//	}
	//}

	void findBase()
	{
		foreach(Workstation workshop in myFaction.workstations)
		{
			if (!oldWorkshop)
			{
				oldWorkshop = workshop.transform;
				currentWorkshop++;
				useWorkshop = oldWorkshop;
				workshopDistanceOld = Mathf.Sqrt(Mathf.Pow(oldWorkshop.position.x - transform.position.x, 2) + Mathf.Pow(oldWorkshop.position.y - transform.position.y, 2));
			}
			else
			{
				newWorkshop = workshop.transform;

				workshopDistanceOld = Mathf.Sqrt(Mathf.Pow(oldWorkshop.position.x - transform.position.x, 2) + Mathf.Pow(oldWorkshop.position.y - transform.position.y, 2));
				workshopDistanceNew = Mathf.Sqrt(Mathf.Pow(newWorkshop.position.x - transform.position.x, 2) + Mathf.Pow(newWorkshop.position.y - transform.position.y, 2));

				if (workshopDistanceOld < workshopDistanceNew)
				{
					useWorkshop = oldWorkshop;
					currentWorkshop++;
					print(useWorkshop);
				}
				else if (workshopDistanceNew < workshopDistanceOld)
				{
					useWorkshop = newWorkshop;
					oldWorkshop = newWorkshop;
					currentWorkshop++;
					print(useWorkshop);
				}
			}

			if (currentWorkshop == myFaction.workstations.Count)
			{
				Vector3 workstationCoords = new Vector3(useWorkshop.position.x, useWorkshop.position.y, useWorkshop.position.z);
				//print(workstationCoords);
				workstationCoords.z = 0;
				path.GoToPos(workstationCoords);
				oldWorkshop = null;
				newWorkshop = null;
				oldZone = null;
				NewZone = null;
				currentWorkshop = 0;
			}
		}
	}
}
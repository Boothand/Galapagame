using UnityEngine;
using System.Collections;
public class Boat : Mover
{

	public Stats.Faction faction;

	[SerializeField]
	FishZone[] fiskesoner = new FishZone[7];
	[SerializeField]
	Fisherboat[] boats = new Fisherboat[10];
	public FactionScript myFaction;

	internal Stats stats;
	Pathfinder path;

	float ZoneDistanceOld;
	float ZoneDistanceNew;
	Transform oldZone;
	Transform NewZone;
	Transform useZone;

	int currentZone = 0;


	void Start ()
	{
		stats = GetComponent<Stats>();
	}

	internal override void BaseUpdate()
	{
		base.BaseUpdate();

		if (faction == Stats.Faction.Player)
		{
			myFaction = gameManager.player;
		}
		else if (faction == Stats.Faction.Blue)
		{
			myFaction = gameManager.blue;
		}
		else if (faction == Stats.Faction.Red)
		{
			myFaction = gameManager.red;
		}
		else if (faction == Stats.Faction.Green)
		{
			myFaction = gameManager.green;
		}
	}
	
	void Update ()
	{
		
	}

	IEnumerable mainBoatAI()
	{
		yield return null;
	}

	IEnumerable findZone()
	{
		//finds a sone
		goToZone();
		yield return null;
	}

	IEnumerable returnToBase()
	{
		//starts a routine for returning to base with fish
		yield return null;
	}

	void goToZone()
	{
		//looping through all fishzones
		foreach(FishZone soner in fiskesoner)
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

				if (ZoneDistanceOld > ZoneDistanceNew)
				{
					useZone = oldZone;
					currentZone++;
				}
				else if (ZoneDistanceNew > ZoneDistanceOld)
				{
					oldZone = NewZone;
					useZone = NewZone;
					currentZone++;
				}
			}

			if (currentZone == fiskesoner.Length)
			{
				Vector3 fishZoneTarget = new Vector3(useZone.position.x, useZone.position.y, useZone.position.z);
				path.GoToPos(fishZoneTarget);
			}
		}
	}

	void checkForEmptyBoats()
	{
		// checks for boats that can be sent to a zone.
		//this one must be done before goToZone()
	}

	void findBase(Transform basePosition)
	{
		//finding the position of your base, in order to return when full of fish.
	}



}
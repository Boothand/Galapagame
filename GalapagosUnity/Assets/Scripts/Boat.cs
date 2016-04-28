using UnityEngine;
using System.Collections;
public class Boat : Mover
{
	[SerializeField]
	public FactionScript myFaction;


	void Start ()
	{
		BaseStart();
	}

	internal override void BaseUpdate()
	{
		base.BaseUpdate();

		if (stats.faction == Stats.Faction.Player)
		{
			myFaction = gameManager.player;
		}
		else if (stats.faction == Stats.Faction.Blue)
		{
			myFaction = gameManager.blue;
		}
		else if (stats.faction == Stats.Faction.Red)
		{
			myFaction = gameManager.red;
		}
		else if (stats.faction == Stats.Faction.Green)
		{
			myFaction = gameManager.green;
		}
	}
	
	void Update ()
	{
		
	}

	



}
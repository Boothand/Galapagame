using UnityEngine;
using System.Collections;
public class Boat : Mover
{

	public Stats.Faction faction;

	[SerializeField]
	public FactionScript myFaction;


	void Start ()
	{
		BaseStart();
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

	



}
﻿using UnityEngine;
using System.Collections;
public class Boat : Mover
{

	void Start ()
	{
		
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
		yield return null;
	}

	IEnumerable returnToBase()
	{
		//starts a routine for returning to base with fish
		yield return null;
	}

	void goToZone(Transform zonePosition)
	{
		//used for going to zone when closest found
		
		//finds a zone, and gets a random position within to go to.
		
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
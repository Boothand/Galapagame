using UnityEngine;

public class Mover : MonoBehaviour
{
	[HideInInspector]
	//Stats stats;
	public float speed = 1f;
	public float turnSpeed = 3f;

	internal void BaseStart ()
	{
		//stats = GetComponent<Stats>();
	}
	
	internal void BaseUpdate ()
	{
		
	}
}
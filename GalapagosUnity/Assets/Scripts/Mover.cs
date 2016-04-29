using UnityEngine;

public class Mover : MonoBehaviour
{
	[HideInInspector]
	internal Stats stats;
	public GameManager gameManager;
	public float speed = 1f;
	public float turnSpeed = 3f;

	virtual internal void BaseStart()
	{
		stats = GetComponent<Stats>();
		if (!gameManager)
		{
			gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
		}
	}
	
	virtual internal void BaseUpdate ()
	{
		
	}
}
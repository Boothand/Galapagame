using UnityEngine;

public class FishZone : MonoBehaviour
{
	public int fishAmount = 20000;
	public int maxFishAmount = 30000;
	public float respawnTime = 2f;
	public int respawnAmount = 15;
	float timer;
	int zoneTypeFactor;
	public ZoneType zoneType = ZoneType.Medium;

	public enum ZoneType
	{
		Small,
		Medium,
		Big
	}

	void Start ()
	{
		
	}

	public int GetFished(int workersOnBoard, int shipType = 1)
	{
		int loss = Mathf.FloorToInt((workersOnBoard * shipType * zoneTypeFactor) / 1.75f);
		fishAmount -= loss;
		
		return loss;
	}
	
	void Update ()
	{
		zoneTypeFactor = (int)zoneType + 1;

		//Kan også løysast med korutine, om man vil
		if (fishAmount < maxFishAmount)
		{
			timer += Time.deltaTime;

			if (timer > respawnTime)
			{
				fishAmount += respawnAmount;
				timer = 0;
			}

			if (fishAmount <= 0)
			{
				Destroy(gameObject);
			}
		}
	}
}
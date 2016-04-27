using UnityEngine;

public class Stats : MonoBehaviour
{
	GameManager gameManager;

    public enum NavType
    {
        Land,
        Water,
        None
    }

	public enum Faction
	{
		Player,
		Red,
		Green,
		Blue,
		Government,
		None
	}
	[HideInInspector] public FactionScript ownerFaction;
	public Faction faction = Faction.None;

	public string typeName = "Unnamed Object";


	[Header("Navigation")]
	[Tooltip("Makes movers navigate around this object's collider")]
	public bool navObstacle;
    public NavType navtype = NavType.None;

	[Header("Selection")]
	public bool selected;

	void Start()
	{
		gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
	}

	void Update()
	{
		if (faction == Faction.Player)
		{
			ownerFaction = gameManager.player;
		}
		else if (faction == Faction.Blue)
		{
			ownerFaction = gameManager.blue;
		}
		else if (faction == Faction.Red)
		{
			ownerFaction = gameManager.red;
		}
		else if (faction == Faction.Green)
		{
			ownerFaction = gameManager.green;
		}
	}
}

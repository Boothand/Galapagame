using UnityEngine;

public class Stats : MonoBehaviour
{
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

	public Faction faction = Faction.None;

	public string typeName = "Unnamed Object";


	[Header("Navigation")]
	[Tooltip("Makes movers navigate around this object's collider")]
	public bool navObstacle;
    public NavType navtype = NavType.None;

	[Header("Selection")]
	public bool selected;
}

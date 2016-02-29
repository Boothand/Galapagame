using UnityEngine;

public class Stats : MonoBehaviour
{
    public enum NavType
    {
        Land,
        Water,
        None
    }

	public string typeName = "Fishing boat Z47";
	[Header("Navigation")]
	[Tooltip("Makes movers navigate around this object's collider")]
	public bool navObstacle;
    public NavType navtype = NavType.None;

	[Header("Selection")]
	public bool selected;
}

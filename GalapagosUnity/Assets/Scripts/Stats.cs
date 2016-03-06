using UnityEngine;

public class Stats : MonoBehaviour
{
    public enum NavType
    {
        Land,
        Water,
        None
    }

	public string typeNameFishingBoat = "Fishing boat Z47";
	public string typeNameGovernmentVessel = "Goverment Vessel";
	public string typeNameWorkBuilding = "Your Fishery";
	public string TypenameNotYourBuilding = "Fishery of player X";

	[Header("Navigation")]
	[Tooltip("Makes movers navigate around this object's collider")]
	public bool navObstacle;
    public NavType navtype = NavType.None;

	[Header("Selection")]
	public bool selected;
}

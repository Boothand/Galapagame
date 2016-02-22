using UnityEngine;

public class Stats : MonoBehaviour
{
    public enum NavType
    {
        Land,
        Water
    }

    public bool navObstacle;
    public NavType navtype = NavType.Land;
}

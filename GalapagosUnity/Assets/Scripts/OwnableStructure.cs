using UnityEngine;

public class OwnableStructure : MonoBehaviour
{
	[SerializeField]
	Transform flag;
	internal Stats stats;

	internal void BaseStart ()
	{
		stats = GetComponent<Stats>();

		if (!flag)
		{
			flag = transform.FindChild("Flagpole/Flag");
		}
	}
	
	internal void BaseUpdate ()
	{

		if (stats.faction == Stats.Faction.Player)
		{
			flag.GetComponent<Renderer>().material = Resources.Load<Material>("Materials/Player");
		}
		else if (stats.faction == Stats.Faction.Blue)
		{
			flag.GetComponent<Renderer>().material = Resources.Load<Material>("Materials/Blue");
		}
		else if (stats.faction == Stats.Faction.Red)
		{
			flag.GetComponent<Renderer>().material = Resources.Load<Material>("Materials/Red");
		}
		else if (stats.faction == Stats.Faction.Green)
		{
			flag.GetComponent<Renderer>().material = Resources.Load<Material>("Materials/Green");
		}
	}
}
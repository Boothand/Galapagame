using UnityEngine;

[RequireComponent(typeof(Stats))]
[RequireComponent(typeof(Collider))]
public class Selectable : MonoBehaviour
{
	Stats stats;
	bool mouseDowned;
	LineRenderer[] lines = new LineRenderer[4];
	public Material lineMaterial;
	public float lineWidth = 0.04f;
	public Color lineColor = Color.white;
	public float lineOffset = 0.4f;
	public float lineZ = -0.1f;

	void Start ()
	{
		stats = GetComponent<Stats>();
		if (!lineMaterial)
		{
			lineMaterial = Resources.Load<Material>("Materials/SelectionLine");
		}

		for (int i = 0; i < lines.Length; i++)
		{
			Transform child = new GameObject("Line").transform;
			child.parent = transform;
			child.position = transform.position;
			child.gameObject.AddComponent<LineRenderer>();

			lines[i] = child.GetComponent<LineRenderer>();
		}
	}
	
	void Update ()
	{
		//Click selection
		if (Input.GetMouseButtonDown(0) || Input.GetMouseButtonUp(0))
		{
			RaycastHit hit;

			Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

			if (Physics.Raycast(ray, out hit))
			{
				//If you click on ME <3
				if (hit.transform == transform)
				{
					if (Input.GetMouseButtonDown(0))
					{
						mouseDowned = true;
					}

					if (mouseDowned && Input.GetMouseButtonUp(0))
					{
						stats.selected = true;
						mouseDowned = false;
					}
				}
				else
				{
					if (Input.GetMouseButtonUp(0))
					{
						stats.selected = false;
					}
				}
			}
		}

		//Draw lines around selected object
		if (stats.selected)
		{
			foreach (LineRenderer lr in lines)
			{
				lr.enabled = true;
				lr.SetWidth(lineWidth, lineWidth);
				lr.SetColors(lineColor, lineColor);
				lr.material = lineMaterial;
			}

			Collider collider = GetComponent<Collider>();

			lines[0].SetPosition(0, new Vector3(collider.bounds.min.x - lineOffset, collider.bounds.min.y - lineOffset, lineZ));
			lines[0].SetPosition(1, new Vector3(collider.bounds.min.x - lineOffset, collider.bounds.max.y + lineOffset, lineZ));

			lines[1].SetPosition(0, new Vector3(collider.bounds.min.x - lineOffset, collider.bounds.min.y - lineOffset, lineZ));
			lines[1].SetPosition(1, new Vector3(collider.bounds.max.x + lineOffset, collider.bounds.min.y - lineOffset, lineZ));

			lines[2].SetPosition(0, new Vector3(collider.bounds.max.x + lineOffset, collider.bounds.min.y - lineOffset, lineZ));
			lines[2].SetPosition(1, new Vector3(collider.bounds.max.x + lineOffset, collider.bounds.max.y + lineOffset, lineZ));

			lines[3].SetPosition(0, new Vector3(collider.bounds.max.x + lineOffset, collider.bounds.max.y + lineOffset, lineZ));
			lines[3].SetPosition(1, new Vector3(collider.bounds.min.x - lineOffset, collider.bounds.max.y + lineOffset, lineZ));

		}
		else
		{
			foreach (LineRenderer lr in lines)
			{
				lr.enabled = false;
			}
		}
	}
}
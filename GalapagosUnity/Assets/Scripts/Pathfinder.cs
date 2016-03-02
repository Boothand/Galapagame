using UnityEngine;
using System.Collections.Generic;

[RequireComponent(typeof(Mover))]
public class Pathfinder : MonoBehaviour
{
	Stats stats;
    Vector3 finalTargetPos;
    Vector3 subTargetPos;
	Mover mover;
	public float distanceFromObstacle = 0.75f;
    [SerializeField]bool hasTarget;
    Queue<Vector3> waypoints = new Queue<Vector3>();
    [SerializeField]int maxIterations = 1500;
	List<Vector3> debugList = new List<Vector3>();
	List<Vector3> debugList2 = new List<Vector3>();

	[SerializeField]bool debug;

	void Start ()
    {
		stats = GetComponent<Stats>();
		mover = GetComponent<Mover>();
	}

    Queue<Vector3> GetWaypoints(float angle, out float distance, int secondRoundLimit = -1)
    {
        Queue<Vector3> waypointQueue = new Queue<Vector3>();
        bool foundFreeWaypoint = false;
        Vector3 tempPoint = transform.position;
        distance = 0f;

        Vector3 tempDirection = (finalTargetPos - transform.position).normalized;

		int iterations = 0;
        while (!foundFreeWaypoint)
        {
            iterations++;

			//if (secondRoundLimit > 0 && iterations > secondRoundLimit)
			//{
			//	if (debug)
			//	{
			//		print("Second round has more waypoints, give up.");
			//	}
			//	break;
			//}

            if (iterations > maxIterations)
            {
                print("Attempts > " + maxIterations + " :(\nPut breakpoint at this error plzz!");
                break;
            }

            RaycastHit[] hits = Physics.RaycastAll(tempPoint, tempDirection, Vector3.Distance(tempPoint, finalTargetPos));
            foreach (RaycastHit hit in hits)
            {
                //Ignore collision with your own stuff
                if (hit.transform.root.GetInstanceID() == transform.root.GetInstanceID())
                {
                    continue;
                }

                if (hit.transform.GetComponent<Stats>() &&
                    hit.transform.GetComponent<Stats>().navObstacle)
                {
                    //Found something we can't pass through now.
                    Vector3 lastPoint = tempPoint;

                    tempPoint = hit.point;

                    if (debug)
                    {
                        Debug.DrawLine(lastPoint, tempPoint);
                    }

					tempPoint.z = 0f;

					
					tempPoint += hit.normal * distanceFromObstacle;
                    tempPoint += Quaternion.Euler(0, 0, angle) * hit.normal;					

                    tempDirection = (finalTargetPos - tempPoint).normalized;
                    distance += Vector3.Distance(lastPoint, tempPoint);

                    waypointQueue.Enqueue(tempPoint);

					if (debug)
                    {
                        Debug.DrawLine(transform.position, tempPoint, Color.red);
                    }

                    break;
                }

                //When there is no longer any obstacle in the way of the raycast
                distance += Vector3.Distance(tempPoint, finalTargetPos);

                if (debug)
                {
                    print("Found free path to target.");
                    print("Travel distance will be: " + distance);
                }

                waypointQueue.Enqueue(finalTargetPos);
                foundFreeWaypoint = true;
				break;
            }

			if (hits.Length == 0)
			{
				if (debug)
				{
					print("No ray at all.");
				}

				waypointQueue.Enqueue(finalTargetPos);
				foundFreeWaypoint = true;
				break;
			}

            if (waypointQueue.Count == 0)
            {
                if (debug)
                {
                    print("Made 0 waypoints.");
                }
                break;
            }
        }

        return waypointQueue;
    }

    Queue<Vector3> GetQuickestWaypoint()
    {
        float traveledDistanceA = 0;
        float traveledDistanceB = 0;
        Queue<Vector3> pathA = GetWaypoints(90, out traveledDistanceA);
		Queue<Vector3> pathAcopy = new Queue<Vector3>(pathA);
		Queue<Vector3> pathB = GetWaypoints(-90, out traveledDistanceB, pathA.Count);
		Queue<Vector3> pathBcopy = new Queue<Vector3>(pathB);

		if (debug)
		{
			debugList.Clear();
			while (pathAcopy.Count > 0)
			{
				debugList.Add(pathAcopy.Dequeue());
			}

			debugList2.Clear();
			while (pathBcopy.Count > 0)
			{
				debugList2.Add(pathBcopy.Dequeue());
			}
		}

		if (pathA.Count > maxIterations - 2)
		{
			return pathB;
		}

		if (pathB.Count > maxIterations - 2)
		{
			return pathA;
		}

        if (traveledDistanceA < traveledDistanceB)
        {
            return pathA;
        }

        return pathB;
    }

    public bool HasReached(Vector3 target)
    {
        if (Vector3.Distance(transform.position, target) < 0.01f)
        {
            return true;
        }

        return false;
    }

    //Can be called from AI script, so compatible with all 'movers'.
    public void GoToPos(Vector3 pos)
    {		
        //If AI etc.
        //Additional check for AI since they don't use a mouse to do raycasting.
        RaycastHit hit;
        Ray AIray = new Ray(pos - Vector3.forward * 0.5f, Vector3.forward);

		if (Physics.Raycast(AIray, out hit))
		{
			if (hit.transform.root.GetInstanceID() != transform.root.GetInstanceID() &&
				hit.transform.GetComponent<Stats>() &&
				hit.transform.GetComponent<Stats>().navtype == GetComponent<Stats>().navtype)
			{
				finalTargetPos = pos;
				hasTarget = true;

				waypoints.Clear();
				waypoints = GetQuickestWaypoint();

				if (waypoints.Count > 0)
				{
					subTargetPos = waypoints.Dequeue();
				}
			}
		}
    }
	
	void Update ()
    {
		if (stats.selected)
		{
			//if GetComponent< AI something here > (), to check if to use player input.

			//Input test
			if (Input.GetMouseButtonDown(1))    //Right click to set a new point
			{
				RaycastHit hit;
				Ray clickRay = Camera.main.ScreenPointToRay(Input.mousePosition);

				if (Physics.Raycast(clickRay, out hit))
				{
					if (hit.transform.root.GetInstanceID() != transform.root.GetInstanceID() &&
						hit.transform.GetComponent<Stats>() &&
						hit.transform.GetComponent<Stats>().navtype == GetComponent<Stats>().navtype)
					{

						GoToPos(hit.point);
					}
				}
				else
				{
					if (debug)
					{
						print("I can't go there.");
					}
				}
			}
		}

        //Move it towards final target, go around obstacles.
        if (hasTarget && !HasReached(finalTargetPos))
        {
			if (debug)
			{
				for (int i = 0; i < debugList.Count - 1; i++)
				{
					Debug.DrawLine(debugList[i], debugList[i + 1], Color.red);
				}

				for (int i = 0; i < debugList2.Count - 1; i++)
				{
					Debug.DrawLine(debugList2[i], debugList2[i + 1], Color.green);
				}
			}

			if (HasReached(subTargetPos))
            {
                if (waypoints.Count > 0)
                {
                    subTargetPos = waypoints.Dequeue();
                }
            }

            if (waypoints.Count == 0)
            {
                subTargetPos = finalTargetPos;
            }

            if (debug)
            {
                Debug.DrawLine(transform.position, subTargetPos, Color.red);
                Debug.DrawLine(transform.position, finalTargetPos, Color.blue);
            }

            Vector3 movementVector = (subTargetPos - transform.position).normalized;

            transform.position += movementVector * Time.deltaTime * mover.speed;

            //Rotate it to face the target.
            float rotX = subTargetPos.x - transform.position.x;
            float rotY = subTargetPos.y - transform.position.y;

            float angle = Mathf.Atan2(rotY, rotX) * Mathf.Rad2Deg;
            Quaternion targetRotation = Quaternion.Euler(new Vector3(0f, 0f, angle));

            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, Time.deltaTime * mover.turnSpeed);
        }
        else
        {
            hasTarget = false;
        }
	}
}
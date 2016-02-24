using UnityEngine;
using System.Collections.Generic;

public class Pathfinder : MonoBehaviour
{
    Vector3 finalTargetPos;
    Vector3 subTargetPos;
    public float moveSpeed = 1f;
    public float turnSpeed = 3f;
    [SerializeField]bool hasTarget;
    Queue<Vector3> waypoints = new Queue<Vector3>();
    [SerializeField]int maxIterations = 1500;

    [SerializeField]bool debug;

	void Start ()
    {
        
	}

    Queue<Vector3> GetWaypoints(float angle, out float distance)
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

            if (debug && iterations > maxIterations)
            {
                print("Attempts > " + maxIterations + " :(");
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

                    tempPoint += Quaternion.Euler(0, 0, angle) * hit.normal;
                    tempDirection = (finalTargetPos - tempPoint).normalized;
                    distance += Vector3.Distance(lastPoint, tempPoint);

                    waypointQueue.Enqueue(tempPoint);
                    //print("Enqueued point.");

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
        Queue<Vector3> pathA = GetWaypoints(70, out traveledDistanceA);
        Queue<Vector3> pathB = GetWaypoints(-70, out traveledDistanceB);

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
                print("Hit " + hit.transform.name);
            }

        }                

        finalTargetPos = pos;
        hasTarget = true;

        waypoints.Clear();
        waypoints = GetQuickestWaypoint();

        if (waypoints.Count > 0)
        {
            subTargetPos = waypoints.Dequeue();
        }
    }
	
	void Update ()
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

        //Move it towards final target, go around obstacles.
        if (hasTarget && !HasReached(finalTargetPos))
        {
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

            transform.position += movementVector * Time.deltaTime * moveSpeed;

            //Rotate it to face the target.
            float rotX = subTargetPos.x - transform.position.x;
            float rotY = subTargetPos.y - transform.position.y;

            float angle = Mathf.Atan2(rotY, rotX) * Mathf.Rad2Deg;
            Quaternion targetRotation = Quaternion.Euler(new Vector3(0f, 0f, angle));

            transform.rotation = Quaternion.Lerp(transform.rotation, targetRotation, Time.deltaTime * turnSpeed);
        }
        else
        {
            hasTarget = false;
        }
	}
}
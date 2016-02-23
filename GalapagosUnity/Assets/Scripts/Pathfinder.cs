using UnityEngine;
using System.Collections.Generic;

public class Pathfinder : MonoBehaviour
{
    Vector3 finalTargetPos;
    Vector3 subTargetPos;
    public float moveSpeed = 1f;
    public float turnSpeed = 3f;
    bool hasTarget;
    Queue<Vector3> waypoints = new Queue<Vector3>();

    [SerializeField]bool drawDebugLines;

	void Start ()
    {
        
	}

    void ConstructWaypoints()
    {
        bool foundFreeWaypoint = false;
        Vector3 tempPoint = transform.position;
        float totalDistanceA = 0;

        Vector3 tempDirection = (finalTargetPos - transform.position).normalized;

        int iterations = 0;
        while (!foundFreeWaypoint)
        {
            iterations++;

            if (iterations > 1200)
            {
                print("Attempts > 1200 :(");
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

                    if (drawDebugLines)
                    {
                        Debug.DrawLine(lastPoint, tempPoint);
                    }

                    tempPoint += Quaternion.Euler(0, 0, 70) * hit.normal;
                    tempDirection = (finalTargetPos - tempPoint).normalized;
                    totalDistanceA += Vector3.Distance(lastPoint, tempPoint);

                    waypoints.Enqueue(tempPoint);
                    print("Enqueued point.");

                    if (drawDebugLines)
                    {
                        Debug.DrawLine(transform.position, tempPoint, Color.red);
                    }

                    break;
                }

                totalDistanceA += Vector3.Distance(tempPoint, finalTargetPos);
                print("Found free path to target.");
                print("Travel distance will be: " + totalDistanceA);
                waypoints.Enqueue(finalTargetPos);
                foundFreeWaypoint = true;
            }
        }
    }

    public bool HasReached(Vector3 target)
    {
        if (Vector3.Distance(transform.position, target) < 0.01f)
        {
            return true;
        }

        return false;
    }
	
	void Update ()
    {
        //Input test
        if (Input.GetMouseButtonDown(1))    //Right click to set a new point
        {
            RaycastHit hit;
            Ray clickRay = Camera.main.ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(clickRay, out hit))
            {
                if (hit.transform.GetComponent<Stats>() &&
                    hit.transform.GetComponent<Stats>().navtype == GetComponent<Stats>().navtype)
                {
                    waypoints.Clear();
                    finalTargetPos = hit.point;
                    hasTarget = true;

                    ConstructWaypoints();

                    if (waypoints.Count > 0)
                    {
                        subTargetPos = waypoints.Dequeue();
                    }
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

            if (drawDebugLines)
            {
                Debug.DrawLine(transform.position, subTargetPos, Color.red);
            }

            Vector3 movementVector = (subTargetPos - transform.position).normalized;

            transform.position += movementVector * Time.deltaTime * moveSpeed;

            //Rotate it to face the target.
            float rotX = subTargetPos.x - transform.position.x;
            float rotY = subTargetPos.y - transform.position.y;

            float angle = Mathf.Atan2(rotY, rotX) * Mathf.Rad2Deg;

            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.Euler(new Vector3(0f, 0f, angle)), Time.deltaTime * turnSpeed);
        }
        else
        {
            hasTarget = false;
        }
	}
}
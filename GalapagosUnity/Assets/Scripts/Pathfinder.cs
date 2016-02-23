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

    [SerializeField]bool drawDebugLines;

	void Start ()
    {
        
	}

    float PathCalc(float angle)
    {
        bool foundFreeWaypoint = false;
        Vector3 tempPoint = transform.position;

        Vector3 tempDirection = (finalTargetPos - transform.position).normalized;
        float travelDistance = 0;

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

                    tempPoint += Quaternion.Euler(0, 0, angle) * hit.normal;
                    tempDirection = (finalTargetPos - tempPoint).normalized;
                    travelDistance += Vector3.Distance(lastPoint, tempPoint);

                    waypoints.Enqueue(tempPoint);
                    //print("Enqueued point.");

                    if (drawDebugLines)
                    {
                        Debug.DrawLine(transform.position, tempPoint, Color.red);
                    }

                    break;
                }

                //When there is no longer any obstacle in the way of the raycast
                travelDistance += Vector3.Distance(tempPoint, finalTargetPos);
                print("Found free path to target.");
                print("Travel distance will be: " + travelDistance);
                waypoints.Enqueue(finalTargetPos);
                foundFreeWaypoint = true;
            }

            if (waypoints.Count == 0)
            {
                print("Made 0 waypoints.");
                break;
            }
        }

        return travelDistance;
    }

    void ConstructWaypoints()
    {
        if (PathCalc(70) < PathCalc(-70))
        {
            waypoints.Clear();
            PathCalc(70);
        }
        else
        {
            waypoints.Clear();
            PathCalc(-70);
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
                if (hit.transform.root.GetInstanceID() != transform.root.GetInstanceID() &&
                    hit.transform.GetComponent<Stats>() &&
                    hit.transform.GetComponent<Stats>().navtype == GetComponent<Stats>().navtype)
                {
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

            if (waypoints.Count == 0)
            {
                subTargetPos = finalTargetPos;
            }

            if (drawDebugLines)
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

            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, Time.deltaTime * turnSpeed);
        }
        else
        {
            hasTarget = false;
        }
	}
}
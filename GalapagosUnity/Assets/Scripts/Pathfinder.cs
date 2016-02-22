using UnityEngine;
using System.Collections.Generic;

public class Pathfinder : MonoBehaviour
{
    Vector3 finalTargetPos;
    Vector3 subTargetPos;
    public float moveSpeed = 1f;
    public float turnSpeed = 3f;
    bool hasTarget;

    public enum Orientation
    {
        Up,
        Right,
        Left,
        Down
    }
    public Orientation modelOrientation = Orientation.Right;

    [SerializeField]bool drawDebugLines;

	void Start ()
    {
        
	}

    Vector3 GetNextWaypoint()
    {
        Vector3 goalDirection = (finalTargetPos - transform.position).normalized;

        bool foundFreeWaypoint = false;
        Vector3 endPoint;
        endPoint = finalTargetPos;

        Vector3 direction = (endPoint - transform.position).normalized;

        RaycastHit[] hits = Physics.RaycastAll(transform.position, direction, Vector3.Distance(endPoint, transform.position));
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
                endPoint = hit.point;

                if (drawDebugLines)
                {
                    Debug.DrawLine(transform.position, endPoint);
                }

                endPoint += Quaternion.Euler(0, 0, 80) * hit.normal;

                if (drawDebugLines)
                {
                    Debug.DrawLine(transform.position, endPoint, Color.red);
                }

                break;
            }

            foundFreeWaypoint = true;
        }

        return endPoint;
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
                    hit.transform.GetComponent<Stats>().validNavTarget)
                {
                    finalTargetPos = hit.point;
                    hasTarget = true;
                }
            }
        }

        //Move it towards final target, go around obstacles.
        if (hasTarget && !HasReached(finalTargetPos))
        {
            if (!HasReached(subTargetPos))
            {
                subTargetPos = GetNextWaypoint();
            }

            if (drawDebugLines)
            {
                Debug.DrawLine(transform.position, subTargetPos, Color.red);
            }

            Vector3 movementVector = (subTargetPos - transform.position).normalized;

            transform.position += movementVector * Time.deltaTime * moveSpeed;

            //Rotate it to face the target.
            if (modelOrientation == Orientation.Right)
            {
                float rotX = subTargetPos.x - transform.position.x;
                float rotY = subTargetPos.y - transform.position.y;

                float angle = Mathf.Atan2(rotY, rotX) * Mathf.Rad2Deg;

                transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.Euler(new Vector3(0f, 0f, angle)), Time.deltaTime * turnSpeed);
            }
        }
        else
        {
            hasTarget = false;
        }
	}
}
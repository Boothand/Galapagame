using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    public float speed = 1f;
    Vector3 targetPos;

	void Start ()
    {
        targetPos = transform.position;
	}
	
	void Update ()
    {
        targetPos += new Vector3(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"), 0f) * speed * Time.deltaTime;
//        transform.position = Vector3.Lerp(transform.position, targetPos, Time.deltaTime * speed);
        transform.position = targetPos;
	}
}

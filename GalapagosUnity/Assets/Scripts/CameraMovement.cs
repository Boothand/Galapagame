using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    public float speed = 1f;
    Vector3 targetPos;
    public float scrollDistance = 10f;

	void Start ()
    {
        targetPos = transform.position;
	}
	
	void Update ()
    {
		if (Input.GetMouseButton(2))
		{
			targetPos += new Vector3(-Input.GetAxisRaw("Mouse X"), -Input.GetAxisRaw("Mouse Y"), 0f) * speed * Time.deltaTime;
		}
        targetPos += new Vector3(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"), 0f) * speed * Time.deltaTime;
		targetPos += Vector3.forward * Input.GetAxis("Mouse ScrollWheel") * scrollDistance;
		transform.position = targetPos;
	}
}

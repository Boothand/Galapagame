using UnityEngine;
using UnityEngine.UI;

public class NotificationText : MonoBehaviour
{
	float lifetime = 4f;
	float timer;
	[HideInInspector] public Text text;

	void Awake ()
	{
		text = GetComponent<Text>();
	}
	
	void Update ()
	{
		timer += Time.deltaTime;

		if (timer > lifetime)
		{
			Destroy(gameObject);
		}
	}
}
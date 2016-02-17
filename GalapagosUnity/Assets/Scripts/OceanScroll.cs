using UnityEngine;

public class OceanScroll : MonoBehaviour
{
	Renderer rend;
	public Vector2 diffuseScroll = Vector2.one;
	public Vector2 normalScroll = Vector2.one;
	Vector2 diffScroll;
	Vector2 nrmScroll;
	float waveTimer;

	void Start ()
	{
		rend = GetComponent<Renderer>();
		diffScroll = rend.material.GetTextureOffset("_MainTex");
		nrmScroll = rend.material.GetTextureOffset("_BumpMap");
	}
	
	void Update ()
	{
		diffScroll += diffuseScroll * Time.deltaTime;
		nrmScroll += normalScroll * Time.deltaTime;

		waveTimer += Time.deltaTime;

		rend.material.SetTextureOffset("_MainTex", diffScroll);
		rend.material.SetTextureOffset("_DetailAlbedoMap", nrmScroll);
		
	}
}
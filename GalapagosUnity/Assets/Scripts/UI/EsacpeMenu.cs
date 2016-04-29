using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


public class EsacpeMenu : MonoBehaviour
{


	public void Resume(GameObject obj)
	{
		obj.SetActive(false);
	}

	public void Restart()
	{
		SceneManager.LoadScene(0);
	}

	public void Exit()
	{
		Application.Quit();
	}
}
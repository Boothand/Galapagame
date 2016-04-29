using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


public class EsacpeMenu : MonoBehaviour
{


	public void Resume(GameObject obj)
	{
		obj.SetActive(false);
		Time.timeScale = 1;
	}

	public void Restart()
	{
		SceneManager.LoadScene(0);
		Time.timeScale = 1;
	}

	public void Exit()
	{
		Application.Quit();
	}
}
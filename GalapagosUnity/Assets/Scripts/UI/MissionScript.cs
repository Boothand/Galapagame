using UnityEngine;
using UnityEngine.UI;

public class MissionScript : MonoBehaviour
{
	public GameObject missionInfoBar;
	public GameObject missionPanel;

	public GameObject survivalMission;
	public GameObject fishermenImpact;
	public GameObject timeWaits;

	void Start()
	{
		missionInfoBar.SetActive(false);
		missionPanel.SetActive(false);
	}

	public void CheckMissions()
	{
		missionInfoBar.SetActive(true);
	}

	public void ExitMissionPanel()
	{
		missionInfoBar.SetActive(false);
	}

	public void exitMissionLogPanel()
	{
		missionPanel.SetActive(false);
		survivalMission.SetActive(false);
		fishermenImpact.SetActive(false);
		timeWaits.SetActive(false);
	}

	public void MissionLog1()
	{
		missionPanel.SetActive(true);
		survivalMission.SetActive(true);
	}

	public void MissionLog2()
	{
		missionPanel.SetActive(true);
		fishermenImpact.SetActive(true);
	}

	public void MissionLog3()
	{
		missionPanel.SetActive(true);
		timeWaits.SetActive(true);
	}

	void Update()
	{

	}
}
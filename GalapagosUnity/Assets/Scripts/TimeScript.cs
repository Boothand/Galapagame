using UnityEngine;
using System.Collections;
using UnityEngine.UI;


public class TimeScript : MonoBehaviour {

	// time variables
	int month;
	int year;

	public int daysinmonth;
	public int currentDay;

	string monthName;
	public Text timeControl;
	//end of time variables



	// Use this for initialization
	void Start () {
		//shows the time.
		month = 1;
		year = 0;
		currentDay = 1;

		time(month);

		timeControl.text = "Year " + year + ", " + currentDay + ". " + monthName;

		StartCoroutine(timeTable(2));
		//end of time part of gamemanager script
		//use this later when setting up events with boats or other stuff.

	}
	
	// Update is called once per frame
	void Update () {
	
	}

	IEnumerator timeTable(int secondsToWait)
	{
		yield return new WaitForSeconds(secondsToWait);

		if (currentDay >= daysinmonth)
		{
			if (monthName == "Dec")
			{
				year++;
				month = 1;
				currentDay = 1;
				print("Printer");
				time(month);
				timeControl.text = "Year " + year + ", " + currentDay + ". " + monthName;
				yield return new WaitForSeconds(2);
				StartCoroutine(timeTable(0));
				yield break;
			}

			currentDay = 1;
			month++;
		}
		else
		{
			currentDay++;
		}

		time(month);

		timeControl.text = "Year " + year + ", " + currentDay + ". " + monthName;

		StartCoroutine(timeTable(2));
		yield break;
	}


	void time(int whatMonth)
	{
		switch (whatMonth)
		{
			case 1:
				monthName = "Jan";
				daysinmonth = 31;
				break;
			case 2:
				monthName = "Feb";
				daysinmonth = 28;
				break;
			case 3:
				monthName = "Mar";
				daysinmonth = 31;
				break;
			case 4:
				monthName = "Apr";
				daysinmonth = 30;
				break;
			case 5:
				monthName = "May";
				daysinmonth = 31;
				break;
			case 6:
				monthName = "Jun";
				daysinmonth = 30;
				break;
			case 7:
				monthName = "Jul";
				daysinmonth = 31;
				break;
			case 8:
				monthName = "Aug";
				daysinmonth = 31;
				break;
			case 9:
				monthName = "Sep";
				daysinmonth = 30;
				break;
			case 10:
				monthName = "Oct";
				daysinmonth = 31;
				break;
			case 11:
				monthName = "Nov";
				daysinmonth = 30;
				break;
			case 12:
				monthName = "Dec";
				daysinmonth = 31;
				break;

		}

	}


}

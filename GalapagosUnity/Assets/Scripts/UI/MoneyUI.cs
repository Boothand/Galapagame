using UnityEngine;
using UnityEngine.UI;

public class MoneyUI : MonoBehaviour
{
	Text text;
	FactionScript faction;
	
	void Start()
	{
		text = GetComponent<Text>();
		faction = GameObject.Find("GameManager").GetComponent<GameManager>().player;
	}
	
	void Update()
	{
		text.text = faction.monthlyDebt + " / " + faction.totalMoney.ToString() + " Debt/Money";
	}
}
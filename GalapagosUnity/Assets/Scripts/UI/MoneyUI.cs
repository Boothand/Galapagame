using UnityEngine;
using UnityEngine.UI;

public class MoneyUI : MonoBehaviour
{
	Text text;
	public FactionScript faction;
	
	void Start()
	{
		text = GetComponent<Text>();
	}
	
	void Update()
	{
		text.text = faction.monthlyDebt + " / " + faction.totalMoney.ToString() + " USD/month";
	}
}
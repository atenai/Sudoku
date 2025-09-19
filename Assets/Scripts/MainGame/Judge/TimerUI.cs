using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using DG.Tweening;

public class TimerUI : MonoBehaviour
{
	[SerializeField] private TextMeshProUGUI timerText;

	/// <summary>
	/// タイマーのテキストをセットする
	/// </summary>
	/// <param name="timer"></param>
	public void SetTimerText(int minute, float seconds)
	{
		string secondsText = seconds.ToString("00.00");
		string timerText = minute.ToString("00") + ":" + secondsText;
		this.timerText.text = timerText;
	}
}

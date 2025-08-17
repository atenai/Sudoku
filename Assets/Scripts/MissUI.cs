using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class MissUI : MonoBehaviour, IMissUI
{
	[SerializeField] private TextMeshProUGUI missCount;
	[SerializeField] private TextMeshProUGUI failNumber;

	/// <summary>
	/// ミスカウントをセットする
	/// </summary>
	/// <param name="missCount"></param>
	public void SetMissCount(int missCount)
	{
		this.missCount.text = missCount.ToString();
	}

	/// <summary>
	/// ミスナンバーをセットする
	/// </summary>
	/// <param name="failNumber"></param>
	public void SetFailNumber(int failNumber)
	{
		this.failNumber.text = failNumber.ToString();
	}
}

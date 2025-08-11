using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class MissUI : MonoBehaviour
{
	[SerializeField] private TextMeshProUGUI missCount;
	[SerializeField] private TextMeshProUGUI missNumber;

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
	/// <param name="missNumber"></param>
	public void SetMissNumber(int missNumber)
	{
		this.missNumber.text = missNumber.ToString();
	}
}

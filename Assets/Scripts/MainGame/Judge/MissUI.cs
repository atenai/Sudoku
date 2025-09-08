using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using DG.Tweening;

public class MissUI : MonoBehaviour
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
		this.missCount.transform.DOShakeScale(0.5f, 1, 10, 90, false).SetEase(Ease.Linear);
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

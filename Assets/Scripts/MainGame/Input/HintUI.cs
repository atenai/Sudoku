using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class HintUI : MonoBehaviour
{
	[SerializeField] private TextMeshProUGUI hintCount;

	/// <summary>
	/// ヒントカウントをセットする
	/// </summary>
	/// <param name="hintCount"></param>
	public void SetHintCount(int hintCount)
	{
		this.hintCount.text = hintCount.ToString();
	}
}

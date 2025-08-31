using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainGameInput
{
	/// <summary>
	/// コンストラクタ
	/// </summary>
	public MainGameInput()
	{

	}

	/// <summary>
	/// メモモードの切り替え
	/// </summary>
	public void ToggleMemoMode(ref bool memoMode)
	{
		memoMode = !memoMode;
		Debug.Log("メモモード: " + (memoMode ? "ON" : "OFF"));
	}
}

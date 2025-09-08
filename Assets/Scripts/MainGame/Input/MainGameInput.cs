using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainGameInput
{
	/// <summary>
	/// コンストラクタ
	/// </summary>
	public MainGameInput() { }

	/// <summary>
	/// メモモードの切り替え
	/// </summary>
	/// <param name="memoMode">メモモード</param>
	public void ToggleMemoMode(ref bool memoMode)
	{
		memoMode = !memoMode;
		Debug.Log("メモモード: " + (memoMode ? "ON" : "OFF"));
	}

	/// <summary>
	/// ヒント数をセットする
	/// </summary>
	/// <param name="difficultyType">設定した難易度</param>
	public int HintCount(MainGameSetting.DifficultyType difficultyType)
	{
		switch (difficultyType)
		{
			case MainGameSetting.DifficultyType.Easy:
				return 5;
			case MainGameSetting.DifficultyType.Normal:
				return 10;
			case MainGameSetting.DifficultyType.Hard:
				return 20;
		}
		return 0;
	}
}

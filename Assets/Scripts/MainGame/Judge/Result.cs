using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Result
{
	/// <summary>
	/// コンストラクタ
	/// </summary>
	public Result() { }

	/// <summary>
	/// ミス数をセットする
	/// </summary>
	/// <param name="difficultyType">設定した難易度</param>
	public int FailNumber(MainGameSetting.DifficultyType difficultyType)
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

	/// <summary>
	/// 正解処理
	/// </summary>
	/// <param name="isAllCorrect">全てのマスが正解かどうか？</param>
	public void Correct(bool isAllCorrect)
	{
		Debug.Log("<color=green>正解！</color>");
		if (isAllCorrect)
		{
			Debug.Log("<color=green>ゲームクリアー！</color>");
		}
	}

	/// <summary>
	/// 不正解処理
	/// </summary>
	/// <param name="missCount">ミスカウント</param>
	/// <param name="failNumber">失敗できる数</param>
	public bool InCorrect(ref int missCount, int failNumber)
	{
		Debug.Log("<color=red>不正解！</color>");
		missCount++;
		if (failNumber <= missCount)
		{
			Debug.Log("<color=red>ゲームオーバー！</color>");
			return true;
		}
		return false;
	}

	/// <summary>
	/// 全てのマスが正解かどうか？
	/// </summary>
	/// <param name="qGrid">問題グリッド</param>
	/// <param name="aGrid">答えグリッド</param>
	/// <returns>全てのマスが正解しているならtrue 一つでも不正解または空ならfalse</returns>
	public bool IsAllCorrect(int[,] qGrid, int[,] aGrid)
	{
		for (int r = 0; r < MainGame.Cell_Number; r++)
		{
			for (int c = 0; c < MainGame.Cell_Number; c++)
			{
				if (qGrid[r, c] != aGrid[r, c])
				{
					return false;
				}
			}
		}
		return true;
	}
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 正誤判定クラス
/// </summary>
public class Judge
{
	/// <summary>
	/// コンストラクタ
	/// </summary>
	public Judge()
	{

	}

	/// <summary>
	/// 正誤判定処理
	/// </summary>
	/// <param name="selectCellAnswerNumber">選択したセルの答え番号</param>
	/// <param name="inputNumber">入力番号</param>
	public bool CheckAnswer(int selectCellAnswerNumber, int inputNumber)
	{
		if (selectCellAnswerNumber == inputNumber)
		{
			//正解
			return true;
		}
		else
		{
			//不正解
			return false;
		}
	}
}

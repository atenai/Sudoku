using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 正誤判定クラス
/// </summary>
public class Judge : IJudge, IRegister
{
	/// <summary>
	/// 生成された全てのセルを管理
	/// </summary>
	private ICellNumber[,] allCells;

	/// <summary>
	/// ミス数
	/// </summary>
	private int failNumber = 5;

	/// <summary>
	/// ミスカウント
	/// </summary>
	private int missCount = 0;

	/// <summary>
	/// ミス数
	/// </summary>
	public int GetFailNumber()
	{
		return failNumber;
	}

	/// <summary>
	/// ミスカウント
	/// </summary>
	public int GetMissCount()
	{
		return missCount;
	}

	private IMissUI missUI;

	private IMemo memo;

	/// <summary>
	/// コンストラクタ
	/// </summary>
	/// <param name="missUI">ミスUI</param>
	/// <param name="mainGameInput">メインゲームインプット</param>
	/// <param name="difficultyType">設定した難易度</param>
	public Judge(IMissUI missUI, IMemo memo, MainGameSetting.DifficultyType difficultyType)
	{
		this.missUI = missUI;
		this.memo = memo;

		SetMissNumber(difficultyType);
	}

	/// <summary>
	/// ミス数をセットする
	/// </summary>
	/// <param name="difficultyType">設定した難易度</param>
	private void SetMissNumber(MainGameSetting.DifficultyType difficultyType)
	{
		switch (difficultyType)
		{
			case MainGameSetting.DifficultyType.Easy:
				failNumber = 10;
				break;
			case MainGameSetting.DifficultyType.Normal:
				failNumber = 10;
				break;
			case MainGameSetting.DifficultyType.Hard:
				failNumber = 20;
				break;
		}
	}

	/// <summary>
	/// マスを登録
	/// </summary>
	/// <param name="cells">登録した全てのセル</param>
	public void IRegisterCells(ICellNumber[,] cells)
	{
		allCells = cells;
	}

	//7
	/// <summary>
	/// 正誤判定処理
	/// </summary>
	/// <param name="cell">選択したセル</param>
	/// <param name="number">入力番号</param>
	public void ICheckAnswer(ICellNumber cell, int number)
	{
		if (memo.GetMemoMode()) { return; } // メモ入力時は判定しない
		if (number == 0) { return; } // 入力を消した場合は判定しない

		if (cell.IGetAnswerNumber() == number)
		{
			//Debug.Log($"({cell.Row},{cell.Col}) 正解！");
			Debug.Log("<color=green>正解！</color>");
			cell.ISetColor(Color.green);
			cell.ISetLockCell(); // 正解したらそのセルをロック

			// クリア判定
			if (CheckAllCellLock() == true)
			{
				Debug.Log("<color=yellow>ゲームクリア！</color>");
			}
		}
		else
		{
			//Debug.Log($"({cell.Row},{cell.Col}) 不正解！");
			Debug.Log("<color=red>不正解！</color>");
			cell.ISetColor(Color.red);
			missCount++;
			missUI.ISetMissCount(missCount);
			if (failNumber <= missCount)
			{
				Debug.Log("<color=red>ゲームオーバー！</color>");
			}
		}
	}

	/// <summary>
	/// 全セルがロックされているかチェック
	/// </summary>
	/// <returns>全てのマスがロックされていたらtrue 1つでもロックされていない場合はfalse</returns>
	private bool CheckAllCellLock()
	{
		foreach (var cell in allCells)
		{
			if (cell.IGetIsInteractable())
			{
				return false;
			}
		}
		return true;
	}
}

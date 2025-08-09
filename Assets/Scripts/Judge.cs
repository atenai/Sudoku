using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 正誤判定クラス
/// </summary>
public class Judge
{
	/// <summary>
	/// 生成された全てのセルを管理
	/// </summary>
	private CellButton[,] allCells;

	/// <summary>
	/// ミス数
	/// </summary>
	private int missNumber = 5;

	/// <summary>
	/// ミスカウント
	/// </summary>
	private int missCount = 0;

	private MainGame mainGame;

	public Judge(MainGame mainGame, MainGameSetting.DifficultyType difficultyType)
	{
		//Debug.Log("<color=red>ジャッジクラス！</color>");
		this.mainGame = mainGame;
		SetMissNumber(difficultyType);
	}

	/// <summary>
	/// ミス数をセットする
	/// </summary>
	/// <param name="difficultyType"></param>
	private void SetMissNumber(MainGameSetting.DifficultyType difficultyType)
	{
		switch (difficultyType)
		{
			case MainGameSetting.DifficultyType.Easy:
				missNumber = 10;
				break;
			case MainGameSetting.DifficultyType.Normal:
				missNumber = 10;
				break;
			case MainGameSetting.DifficultyType.Hard:
				missNumber = 20;
				break;
		}
	}

	/// <summary>
	/// マスを登録
	/// </summary>
	/// <param name="cells"></param>
	public void RegisterCells(CellButton[,] cells)
	{
		allCells = cells;
	}

	//7
	/// <summary>
	/// 正誤判定処理
	/// </summary>
	/// <param name="cell"></param>
	/// <param name="number"></param>
	public void CheckAnswer(CellButton cell, int number)
	{
		if (mainGame.MemoMode) return; // メモ入力時は判定しない
		if (number == 0) return; // 入力を消した場合は判定しない

		if (cell.AnswerNumber == number)
		{
			//Debug.Log($"({cell.Row},{cell.Col}) 正解！");
			Debug.Log("<color=green>正解！</color>");
			cell.SetColor(Color.green);
			cell.LockCell(); // 正解したらそのセルをロック

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
			cell.SetColor(Color.red);
			missCount++;
			if (missNumber <= missCount)
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
			if (cell.GetComponent<UnityEngine.UI.Button>().interactable)
			{
				return false;
			}
		}
		return true;
	}
}

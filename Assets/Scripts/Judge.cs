using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 正誤判定クラス
/// </summary>
public class Judge : IJudge
{
	/// <summary>
	/// 生成された全てのセルを管理
	/// </summary>
	private ICellButton[,] allCells;

	/// <summary>
	/// ミス数
	/// </summary>
	private int failNumber = 5;

	/// <summary>
	/// ミスカウント
	/// </summary>
	private int missCount = 0;

	private MainGame mainGame;

	public int FailNumber => failNumber;
	public int MissCount => missCount;

	/// <summary>
	/// コンストラクタ
	/// </summary>
	/// <param name="mainGame"></param>
	/// <param name="difficultyType">設定した難易度</param>
	public Judge(MainGame mainGame, MainGameSetting.DifficultyType difficultyType)
	{
		//Debug.Log("<color=red>ジャッジクラス！</color>");
		this.mainGame = mainGame;
		SetMissNumber(difficultyType);

		mainGame.MainGameUI.IMissUI.SetMissCount(missCount);
		mainGame.MainGameUI.IMissUI.SetFailNumber(failNumber);
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
	public void RegisterCells(ICellButton[,] cells)
	{
		allCells = cells;
	}

	//7
	/// <summary>
	/// 正誤判定処理
	/// </summary>
	/// <param name="cell">選択したセル</param>
	/// <param name="number">入力番号</param>
	public void CheckAnswer(ICellButton cell, int number)
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
			mainGame.MainGameUI.IMissUI.SetMissCount(missCount);
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
			if (cell.IsInteractable)
			{
				return false;
			}
		}
		return true;
	}
}

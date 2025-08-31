using System;
using UnityEngine;

public class MainGame : MonoBehaviour
{
	/// <summary>
	/// マス数
	/// </summary>
	public static readonly int Cell_Number = 9;

	/// <summary>
	/// 答えグリッド
	/// </summary>
	private int[,] answerGrid = new int[Cell_Number, Cell_Number];

	/// <summary>
	/// 問題グリッド
	/// </summary>
	private int[,] questionGrid = new int[Cell_Number, Cell_Number];

	/// <summary>
	/// ミスカウント
	/// </summary>
	private int missCount = 0;

	/// <summary>
	/// ミス数
	/// </summary>
	private int failNumber = 5;

	/// <summary>
	/// メモモード切替
	/// </summary>
	private bool memoMode = false;

	// ★ ここを座標で保持
	private int selectedRow = -1;
	private int selectedCol = -1;

	/// <summary>
	/// メインゲームUI
	/// </summary>
	[SerializeField] private MainGameUI mainGameUI;

	/// <summary>
	/// 初期化処理
	/// </summary>
	private void Start()
	{
		if (GameManager.SingletonInstance.GetSetting() is MainGameSetting mainGameSetting)
		{
			//問題生成

			// 1. 完全な数独を生成
			MainGameLogicFacade.CreateAnswerGrid(0, 0, answerGrid);
			Debug.Log("<color=red>答えを生成しました！</color>");
			MainGameLogicFacade.DebugGrid(answerGrid);

			// 2. 完全解をコピーして問題用にする
			System.Array.Copy(answerGrid, questionGrid, answerGrid.Length);

			// 3. マスを1つずつ消して唯一解を保つ
			int emptyCells = MainGameLogicFacade.EmptyCell(mainGameSetting.Difficulty);
			MainGameLogicFacade.CreateQuestionGrid(questionGrid, emptyCells);
			Debug.Log("<color=blue>問題を生成しました！</color>");
			MainGameLogicFacade.DebugGrid(questionGrid);

			mainGameUI.Board.CreateCell
			(
				answerGrid,
				questionGrid,
				(row, col) => OnCellSelected(row, col)
			);

			//ミスカウントをセット
			missCount = 0;
			mainGameUI.MissUI.SetMissCount(missCount);
			failNumber = MainGameLogicFacade.FailNumber(mainGameSetting.Difficulty);
			mainGameUI.MissUI.SetFailNumber(failNumber);

			//ボタンにイベントを登録
			foreach (var InputNumberButton in mainGameUI.InputNumberButtons)
			{
				InputNumberButton.Initialize((int number) =>
				{
					Debug.Log($"入力ボタンがクリックされました: {number}");
					OnNumberInput(number);
				});
			}
			mainGameUI.ClearButton.Initialize(() => Debug.Log("クリアボタンがクリックされました"));
			mainGameUI.MemoButton.Initialize(GetMemoMode, () => MainGameLogicFacade.ToggleMemoMode(ref memoMode));
		}
		else
		{
			Debug.LogError("メインゲームに必要な値を取得できませんでした。");
		}
	}

	public bool GetMemoMode()
	{
		return memoMode;
	}

	// ★ セル選択
	private void OnCellSelected(int row, int col)
	{
		selectedRow = row;
		selectedCol = col;
	}

	// ★ 数字入力
	private void OnNumberInput(int inputNumber)
	{
		if (selectedRow < 0 || selectedCol < 0) return;

		// 固定マスは無視
		if (questionGrid[selectedRow, selectedCol] != 0) return;

		if (memoMode)
		{
			return;
		}

		if (MainGameLogicFacade.CheckAnswer(answerGrid[selectedRow, selectedCol], inputNumber))
		{
			MainGameLogicFacade.Correct();
		}
		else
		{
			MainGameLogicFacade.InCorrect(ref missCount, failNumber);
			mainGameUI.MissUI.SetMissCount(missCount);
		}
	}
}

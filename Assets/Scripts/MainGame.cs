using System;
using UnityEngine;

/// <summary>
/// メインゲームクラス
/// </summary>
/// <remarks>
/// 数独のシステム全体を管理
/// </remarks>
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

	//ここで座標を保持
	private int currentRow = -1;
	private int currentCol = -1;


	private int oldRow = -1;
	private int oldCol = -1;

	/// <summary>
	/// メインゲームUI
	/// </summary>
	[SerializeField] private MainGameUI mainGameUI;

	MainGameLogicFacade mainGameLogicFacade = new MainGameLogicFacade();

	// ★ 追加：UIイベントハブ
	private GameEventHub uiEvents = new GameEventHub();

	/// <summary>
	/// 初期化処理
	/// </summary>
	private void Start()
	{
		if (GameManager.SingletonInstance.GetSetting() is MainGameSetting mainGameSetting)
		{
			//問題生成

			// 1. 完全な数独を生成
			mainGameLogicFacade.CreateAnswerGrid(0, 0, answerGrid);
			Debug.Log("<color=red>答えを生成しました！</color>");
			mainGameLogicFacade.DebugGrid(answerGrid);

			// 2. 完全解をコピーして問題用にする
			System.Array.Copy(answerGrid, questionGrid, answerGrid.Length);

			// 3. マスを1つずつ消して唯一解を保つ
			int emptyCells = mainGameLogicFacade.EmptyCell(mainGameSetting.Difficulty);
			mainGameLogicFacade.CreateQuestionGrid(questionGrid, emptyCells);
			Debug.Log("<color=blue>問題を生成しました！</color>");
			mainGameLogicFacade.DebugGrid(questionGrid);


			//============================================================//
			// Start() 抜粋（この順で）
			mainGameUI.Bind(uiEvents); // ① Bind

			uiEvents.OnCellSelectedFromUI += (newRow, newCol) =>
			{
				currentRow = newRow;
				currentCol = newCol;
				uiEvents.RaiseSelectionChanged(currentRow, currentCol, oldRow, oldCol);
				oldRow = currentRow;
				oldCol = currentCol;
			};

			uiEvents.RaiseBoardInitialized(answerGrid, questionGrid); // ② Raise（空振り防止）
			missCount = 0;
			failNumber = mainGameLogicFacade.FailNumber(mainGameSetting.Difficulty);
			uiEvents.RaiseMissCountChanged(missCount, failNumber);

			// ↓ ここで再度 Bind を呼ばないように（あなたのコードだと2回呼んでます）
			mainGameUI.Initialize(
				onNumberClick: number =>
				{
					OnNumberInput(number);
					if (!memoMode)
					{
						ClearCellSelected();
					}
				},
				onClearClick: () =>
				{
					if (currentRow < 0 || currentCol < 0)
					{
						return;
					}
					if (questionGrid[currentRow, currentCol] != 0)
					{
						return;
					}
					uiEvents.RaiseCellCleared(currentRow, currentCol);
					questionGrid[currentRow, currentCol] = 0;
					ClearCellSelected();
				},
				getMemoMode: GetMemoMode,
				onToggleMemo: () =>
				{
					mainGameLogicFacade.ToggleMemoMode(ref memoMode);
					uiEvents.RaiseMemoModeChanged(memoMode);
				}
			);
			//============================================================//
		}
		else
		{
			Debug.LogError("メインゲームに必要な値を取得できませんでした。");
		}
	}

	/// <summary>
	/// メモモードの取得
	/// </summary>
	/// <returns></returns>
	public bool GetMemoMode()
	{
		return memoMode;
	}

	/// <summary>
	/// セルが選択されたときの処理
	/// </summary>
	/// <param name="row"></param>
	/// <param name="col"></param>
	private void OnCellSelected(int newRow, int newCol)
	{
		currentRow = newRow;
		currentCol = newCol;

		mainGameUI.Board.SetSelectedHighlight(currentRow, currentCol, oldRow, oldCol);

		oldRow = currentRow;
		oldCol = currentCol;
	}

	/// <summary>
	/// セルの選択を解除
	/// </summary>
	private void ClearCellSelected()
	{
		oldRow = -1;
		oldCol = -1;
		currentRow = -1;
		currentCol = -1;
	}

	// MainGame.cs の OnNumberInput（メモ分岐を修正）
	private void OnNumberInput(int inputNumber)
	{
		if (currentRow < 0 || currentCol < 0)
		{
			return;
		}
		if (questionGrid[currentRow, currentCol] != 0)
		{
			return;
		}

		if (memoMode)
		{
			// ★ ここは通常入力イベントではなく、メモ専用イベント
			uiEvents.RaiseMemoToggledAtCell(currentRow, currentCol, inputNumber);
			return;
		}

		bool isCorrect = mainGameLogicFacade.CheckAnswer(answerGrid[currentRow, currentCol], inputNumber);
		if (isCorrect == true)
		{
			questionGrid[currentRow, currentCol] = inputNumber;
			mainGameLogicFacade.Correct(mainGameLogicFacade.IsAllCorrect(questionGrid, answerGrid));
		}
		else
		{
			mainGameLogicFacade.InCorrect(ref missCount, failNumber);
			uiEvents.RaiseMissCountChanged(missCount, failNumber);
		}

		uiEvents.RaiseCellInputResult(currentRow, currentCol, inputNumber, isCorrect);
	}
}

using System;
using UnityEngine;

/// <summary>
/// メインゲームクラス
/// </summary>
/// <remarks>
/// 数独のやり取りを管理
/// MVPパターンのPresenter（プレゼンター）を担当
/// </remarks>
public class MainGame : MonoBehaviour
{
	/// <summary>
	/// マス数
	/// </summary>
	public static readonly int Cell_Number = 9;

	/// <summary>
	/// 区切りブロック
	/// </summary>
	public static readonly int Separator_Block = 3;

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

	/// <summary>
	/// 現在のセル
	/// </summary>
	private int currentRow = -1;

	/// <summary>
	/// 現在のセル
	/// </summary>
	private int currentCol = -1;

	/// <summary>
	/// 以前のセル
	/// </summary>
	private int oldRow = -1;

	/// <summary>
	/// 以前のセル
	/// </summary>
	private int oldCol = -1;

	/// <summary>
	/// ヒント数
	/// </summary>
	private int hintCount = 5;

	/// <summary>
	/// メインゲームUI
	/// </summary>
	[SerializeField] private MainGameUIFacade mainGameUIFacade;

	private MainGameLogicFacade mainGameLogicFacade = new MainGameLogicFacade();

	/// <summary>
	/// 初期化処理
	/// </summary>
	private void Start()
	{
		if (GameManager.SingletonInstance.GetSetting() is MainGameSetting mainGameSetting)
		{
			//問題生成
			mainGameLogicFacade.CreateGrid(answerGrid, questionGrid, mainGameSetting);

			//マスUIを生成
			mainGameUIFacade.CreateCell(answerGrid, questionGrid, (row, col) => OnCellSelected(row, col));
			ClearCellSelected();

			//ミスUIをセット
			missCount = 0;
			mainGameUIFacade.SetMissCount(missCount);
			failNumber = mainGameLogicFacade.FailNumber(mainGameSetting.Difficulty);
			mainGameUIFacade.SetFailNumber(failNumber);

			//ヒントUIをセット
			hintCount = mainGameLogicFacade.HintCount(mainGameSetting.Difficulty);
			mainGameUIFacade.SetHintCount(hintCount);

			//タイマーシステムを初期化
			mainGameLogicFacade.InitTimerSystem(mainGameSetting.Difficulty);
			mainGameUIFacade.SetTimerText(mainGameLogicFacade.GetMinute(), mainGameLogicFacade.GetSeconds());

			//ボタンにイベントを登録	
			mainGameUIFacade.InputNumberButtonInitialize((int number) =>
			{
				Debug.Log($"入力ボタンがクリックされました: {number}");
				OnNumberInput(number);
				ClearCellSelected();
			});

			mainGameUIFacade.ClearButtonInitialize(() =>
			{
				Debug.Log("クリアボタンがクリックされました");
				mainGameUIFacade.ClearNumber(currentRow, currentCol);
				ClearCellSelected();
			});

			mainGameUIFacade.MemoButtonInitialize(GetMemoMode, () =>
			{
				mainGameLogicFacade.ToggleMemoMode(ref memoMode);
			});

			mainGameUIFacade.HintButtonInitialize(() =>
			{
				Debug.Log("ヒントボタンがクリックされました");
				UseSelectHint();
			});
		}
		else
		{
			Debug.LogError("メインゲームに必要な値を取得できませんでした。");
		}
	}

	private void Update()
	{
		if (mainGameLogicFacade.UpdateTimerSystem())
		{
			mainGameUIFacade.SetTimerText(mainGameLogicFacade.GetMinute(), mainGameLogicFacade.GetSeconds());
		}
		else
		{
			mainGameUIFacade.SetTimerText(0, 0f);
			Debug.Log("<color=red>ゲームオーバー！</color>");
			GameManager.SingletonInstance.ChangeScene(new GameOverSetting(), GameManager.SingletonInstance.GameOver_Scene_Name);
		}
	}

	/// <summary>
	/// メモモードの取得
	/// </summary>
	/// <returns></returns>
	private bool GetMemoMode()
	{
		return memoMode;
	}

	/// <summary>
	/// セルが選択されたときの処理
	/// </summary>
	/// <param name="row">行</param>
	/// <param name="col">列</param>
	private void OnCellSelected(int newRow, int newCol)
	{
		currentRow = newRow;
		currentCol = newCol;

		mainGameUIFacade.OldSelectHighlight(oldRow, oldCol);
		mainGameUIFacade.HighlightRelatedCells(currentRow, currentCol);
		mainGameUIFacade.SetSelectedHighlight(currentRow, currentCol);

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

	/// <summary>
	/// 数字が入力されたときの処理
	/// </summary>
	/// <param name="inputNumber">入力数値</param>
	private void OnNumberInput(int inputNumber)
	{
		if (currentRow < 0 || currentCol < 0) { return; }//マスが選択されていない状態なら切り上げ
		if (questionGrid[currentRow, currentCol] != 0) { return; }//既にマスが正解およびロック状態しているなら切り上げ
		if (memoMode)
		{
			mainGameUIFacade.ToggleMemo(currentRow, currentCol, inputNumber);
			return;
		}

		bool isCorrect = mainGameLogicFacade.CheckAnswer(answerGrid[currentRow, currentCol], inputNumber);
		if (isCorrect == true)
		{
			// まず現在の問題グリッドの状態を更新
			questionGrid[currentRow, currentCol] = inputNumber;
			if (mainGameLogicFacade.IsAllCorrect(questionGrid, answerGrid))
			{
				Debug.Log("<color=green>ゲームクリアー！</color>");
				GameManager.SingletonInstance.ChangeScene(new GameClearSetting(), GameManager.SingletonInstance.GameClear_Scene_Name);
			}
		}
		else
		{
			if (mainGameLogicFacade.InCorrect(ref missCount, failNumber))
			{
				Debug.Log("<color=red>ゲームオーバー！</color>");
				GameManager.SingletonInstance.ChangeScene(new GameOverSetting(), GameManager.SingletonInstance.GameOver_Scene_Name);
			}
			mainGameUIFacade.SetMissCount(missCount);
		}

		mainGameUIFacade.ShowNumber(currentRow, currentCol, inputNumber, isCorrect);
	}

	/// <summary>
	/// 選択中のマスにヒントを使用する
	/// </summary>
	private void UseSelectHint()
	{
		if (currentRow < 0 || currentCol < 0) { return; }//マスが選択されていない状態なら切り上げ
		if (questionGrid[currentRow, currentCol] != 0) { return; }//既にマスが正解およびロック状態しているなら切り上げ
		if (hintCount <= 0) { return; }

		int correctNumber = answerGrid[currentRow, currentCol];
		questionGrid[currentRow, currentCol] = correctNumber;
		if (mainGameLogicFacade.IsAllCorrect(questionGrid, answerGrid))
		{
			Debug.Log("<color=green>ゲームクリアー！</color>");
			GameManager.SingletonInstance.ChangeScene(new GameClearSetting(), GameManager.SingletonInstance.GameClear_Scene_Name);
		}
		mainGameUIFacade.ShowNumber(currentRow, currentCol, correctNumber, true);

		hintCount--;
		mainGameUIFacade.SetHintCount(hintCount);
	}
}

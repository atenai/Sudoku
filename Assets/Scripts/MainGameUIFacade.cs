using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using System;

/// <summary>
/// メインゲームUIファサードクラス
/// </summary>
/// <remarks>
/// ファサードパターンを使用したクラス
/// 各サブクラスをここにまとめて、リストとして外部に公開する
/// 数独のUI機能を管理
/// MVPパターンのView（ビュー）を担当
/// </remarks>
public class MainGameUIFacade : MonoBehaviour
{
	/// <summary>
	/// ボード
	/// </summary>
	[SerializeField] private Board board;

	/// <summary>
	/// ミスUI
	/// </summary>
	[SerializeField] private MissUI missUI;

	/// <summary>
	/// ヒントUI
	/// </summary>
	[SerializeField] private HintUI hintUI;

	/// <summary>
	/// 数字入力ボタン
	/// </summary>
	[SerializeField] private InputNumberButton[] inputNumberButtons;

	/// <summary>
	/// クリアボタン
	/// </summary>
	[SerializeField] private ClearButton clearButton;

	/// <summary>
	/// ヒントボタン
	/// </summary>
	[SerializeField] private HintButton hintButton;

	/// <summary>
	/// メモボタン
	/// </summary>
	[SerializeField] private MemoButton memoButton;

	/// <summary>
	/// タイマーUI
	/// </summary>
	[SerializeField] private TimerUI timerUI;

	public void CreateCell(int[,] aGrid, int[,] qGrid, UnityAction<int, int> unityAction)
	{
		board.CreateCell(aGrid, qGrid, unityAction);
	}

	public void ClearNumber(int row, int col)
	{
		board.ClearNumber(row, col);
	}

	public void OldSelectHighlight(int oldRow, int oldCol)
	{
		board.OldSelectHighlight(oldRow, oldCol);
	}

	public void SetSelectedHighlight(int currentRow, int currentCol)
	{
		board.SetSelectedHighlight(currentRow, currentCol);
	}

	public void HighlightRelatedCells(int currentRow, int currentCol)
	{
		board.HighlightRelatedCells(currentRow, currentCol);
	}

	public void ToggleMemo(int row, int col, int number)
	{
		board.ToggleMemo(row, col, number);
	}

	public void ShowNumber(int row, int col, int inputNumber, bool isCorrect)
	{
		board.ShowNumber(row, col, inputNumber, isCorrect);
	}

	public void SetMissCount(int missCount)
	{
		missUI.SetMissCount(missCount);
	}

	public void SetFailNumber(int failNumber)
	{
		missUI.SetFailNumber(failNumber);
	}

	public void InputNumberButtonInitialize(UnityAction<int> unityAction)
	{
		for (int i = 0; i < inputNumberButtons.Length; i++)
		{
			inputNumberButtons[i].Initialize(i + 1, unityAction);
		}
	}

	public void ClearButtonInitialize(UnityAction unityAction)
	{
		clearButton.Initialize(unityAction);
	}

	public void MemoButtonInitialize(Func<bool> memoMode, UnityAction unityAction)
	{
		memoButton.Initialize(memoMode, unityAction);
	}

	public void HintButtonInitialize(UnityAction unityAction)
	{
		hintButton.Initialize(unityAction);
	}

	public void SetHintCount(int hintCount)
	{
		hintUI.SetHintCount(hintCount);
	}

	public void SetTimerText(int minute, float seconds)
	{
		timerUI.SetTimerText(minute, seconds);
	}
}

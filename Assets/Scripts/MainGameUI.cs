using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using System;

public class MainGameUI : MonoBehaviour
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
	/// 数字入力ボタン
	/// </summary>
	[SerializeField] private InputNumberButton[] inputNumberButtons;

	/// <summary>
	/// クリアボタン
	/// </summary>
	[SerializeField] private ClearButton clearButton;

	/// <summary>
	/// メモボタン
	/// </summary>
	[SerializeField] private MemoButton memoButton;

	public void CreateCell(int[,] aGrid, int[,] qGrid, UnityAction<int, int> unityAction)
	{
		board.CreateCell(aGrid, qGrid, unityAction);
	}

	public void ClearNumber(int row, int col)
	{
		board.ClearNumber(row, col);
	}

	public void SetSelectedHighlight(int currentRow, int currentCol, int oldRow, int oldCol)
	{
		board.SetSelectedHighlight(currentRow, currentCol, oldRow, oldCol);
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

	public void InputNumberButtonInitialize(int index, UnityAction<int> unityAction)
	{
		inputNumberButtons[index].Initialize(unityAction);
	}

	public int GetInputNumberButtonsLength()
	{
		return inputNumberButtons.Length;
	}

	public void ClearButtonInitialize(UnityAction unityAction)
	{
		clearButton.Initialize(unityAction);
	}

	public void MemoButtonInitialize(Func<bool> memoMode, UnityAction unityAction)
	{
		memoButton.Initialize(memoMode, unityAction);
	}
}

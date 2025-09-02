// GameEventHub.cs
using System;

public sealed class GameEventHub
{
	public event Action<int[,], int[,]> OnBoardInitialized;
	public event Action<int, int, int, int> OnSelectionChanged;
	public event Action<int, int, int, bool> OnCellInputResult;
	public event Action<int, int> OnCellCleared;
	public event Action<int, int> OnMissCountChanged;
	public event Action<bool> OnMemoModeChanged;
	public event Action<int, int> OnCellSelectedFromUI;

	// ★ 追加：メモ入力（トグル）
	public event Action<int, int, int> OnMemoToggledAtCell;

	public void RaiseBoardInitialized(int[,] a, int[,] q)
	{
		OnBoardInitialized?.Invoke(a, q);
	}

	public void RaiseSelectionChanged(int curR, int curC, int oldR, int oldC)
	{
		OnSelectionChanged?.Invoke(curR, curC, oldR, oldC);
	}

	public void RaiseCellInputResult(int r, int c, int num, bool ok)
	{
		OnCellInputResult?.Invoke(r, c, num, ok);
	}

	public void RaiseCellCleared(int r, int c)
	{
		OnCellCleared?.Invoke(r, c);
	}

	public void RaiseMissCountChanged(int miss, int fail)
	{
		OnMissCountChanged?.Invoke(miss, fail);
	}

	public void RaiseMemoModeChanged(bool on)
	{
		OnMemoModeChanged?.Invoke(on);
	}

	public void RaiseCellSelectedFromUI(int row, int col)
	{
		OnCellSelectedFromUI?.Invoke(row, col);
	}

	// ★ 追加：メモのライザー
	public void RaiseMemoToggledAtCell(int row, int col, int number)
	{
		OnMemoToggledAtCell?.Invoke(row, col, number);
	}
}

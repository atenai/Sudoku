using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainGameInput : IMainGameInput
{
	/// <summary>
	/// 現在選択しているセル
	/// </summary>
	private ICellButton selectedCurrentCell;

	/// <summary>
	/// メモモード切替
	/// </summary>
	private bool memoMode = false;

	/// <summary>
	/// メモモードの取得
	/// </summary>
	/// <returns>メモモードの状態</returns>
	public bool GetMemoMode()
	{
		return memoMode;
	}

	/// <summary>
	/// コンストラクタ
	/// </summary>
	public MainGameInput()
	{

	}

	/// <summary>
	/// セルを選択
	/// </summary>
	/// <param name="cell"></param>
	public void ISelectCell(ICellButton cell)
	{
		// 以前のセルのハイライトを解除
		if (selectedCurrentCell != null && selectedCurrentCell != cell)
		{
			selectedCurrentCell.IHighlight(false);
		}

		// 新しく選んだセルを選択状態に
		selectedCurrentCell = cell;
		selectedCurrentCell.IHighlight(true);// 選択セルをハイライト
	}

	//3
	/// <summary>
	/// 数値を入力
	/// </summary>
	/// <param name="number"></param>
	public void IInputNumber(int number)
	{
		if (selectedCurrentCell != null)
		{
			if (memoMode && number != 0)
			{
				//入力後ハイライト解除
				selectedCurrentCell.IHighlight(false);
				//メモを切り替え
				selectedCurrentCell.IToggleMemo(number);
			}
			else
			{
				//入力後ハイライト解除
				selectedCurrentCell.IHighlight(false);
				//4
				//本数字入力
				selectedCurrentCell.ISetNumber(number);
			}

			selectedCurrentCell = null;
		}
	}

	/// <summary>
	/// メモモードの切り替え
	/// </summary>
	public void IToggleMemoMode()
	{
		memoMode = !memoMode;
		Debug.Log("メモモード: " + (memoMode ? "ON" : "OFF"));
	}
}

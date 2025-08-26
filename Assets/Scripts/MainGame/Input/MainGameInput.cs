using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainGameInput : IInputNumber, ISelectCell, IMemo
{
	/// <summary>
	/// 現在選択しているセル
	/// </summary>
	private ICellNumber selectedCurrentCell;

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
	public void ISetSelectCell(ICellNumber cell)
	{
		// 以前のセルのハイライトを解除
		if (selectedCurrentCell != null && selectedCurrentCell != cell)
		{
			selectedCurrentCell.ISetHighlight(false);
		}

		// 新しく選んだセルを選択状態に
		selectedCurrentCell = cell;
		selectedCurrentCell.ISetHighlight(true);// 選択セルをハイライト
	}

	//3
	/// <summary>
	/// 数値を入力
	/// </summary>
	/// <param name="number"></param>
	public void ISetInputNumber(int number)
	{
		if (selectedCurrentCell != null)
		{
			if (memoMode && number != 0)
			{
				//入力後ハイライト解除
				selectedCurrentCell.ISetHighlight(false);
				//メモへ数字を入力
				selectedCurrentCell.ISetMemoNumber(number);
			}
			else
			{
				//入力後ハイライト解除
				selectedCurrentCell.ISetHighlight(false);
				//4
				//本数字を入力
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

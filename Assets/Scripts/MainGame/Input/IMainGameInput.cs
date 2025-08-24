using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IMainGameInput
{
	/// <summary>
	/// メモモードの状態を取得
	/// </summary>
	/// <returns>メモモードの状態</returns>
	public bool GetMemoMode();

	/// <summary>
	/// セルを選択したときの処理
	/// </summary>
	public void ISetSelectCell(ICellButton cellButton);

	/// <summary>
	/// 数字を入力したときの処理
	/// </summary>
	public void ISetInputNumber(int number);

	/// <summary>
	/// メモモード切り替え
	/// </summary>
	public void IToggleMemoMode();
}

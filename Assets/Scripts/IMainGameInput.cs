using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IMainGameInput
{
	/// メモモード（ON/OFF）
	bool IMemoMode { get; set; }

	/// <summary>
	/// セルを選択したときの処理
	/// </summary>
	void ISelectCell(ICellButton cellButton);

	/// <summary>
	/// 数字を入力したときの処理
	/// </summary>
	void IInputNumber(int number);

	/// <summary>
	/// メモモード切り替え
	/// </summary>
	void IToggleMemoMode();
}

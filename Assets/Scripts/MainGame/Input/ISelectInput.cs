using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ISelectInput
{
	/// <summary>
	/// セルを選択したときの処理
	/// </summary>
	public void ISetSelectCell(ICellNumber cell);

	/// <summary>
	/// 数字を入力したときの処理
	/// </summary>
	public void ISetInputNumber(int number);
}

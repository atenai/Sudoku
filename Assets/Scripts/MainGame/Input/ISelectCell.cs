using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ISelectCell
{
	/// <summary>
	/// セルを選択したときの処理
	/// </summary>
	public void ISetSelectCell(ICellNumber cell);
}

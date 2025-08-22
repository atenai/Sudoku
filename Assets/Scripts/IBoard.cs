using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IBoard
{
	/// <summary>
	/// 初期化処理
	/// </summary>
	public void Initialize(IMainGameInput mainGameInput, IJudge judge);

	/// <summary>
	/// マスを作成
	/// </summary>
	/// <param name="aGrid">答えグリッド</param>
	/// <param name="qGrid">問題グリッド</param>
	public void ICreateCell(IGridData gridData);
}

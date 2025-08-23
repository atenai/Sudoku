using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IGridData
{
	/// <summary>
	/// 答えグリッドの数値
	/// </summary>
	/// </remarks>
	/// <param name="row">横</param>
	/// <param name="col">縦</param>
	/// <returns>答えグリッドの数値</returns>
	public int IGetAnswerGridNumber(int row, int col);

	/// <summary>
	/// 問題グリッドの数値
	/// </summary>
	/// <param name="row">横</param>
	/// <param name="col">縦</param>
	/// <returns>問題グリッドの数値</returns>
	public int IGetQuestionGridNumber(int row, int col);
}

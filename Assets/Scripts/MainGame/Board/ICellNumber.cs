using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ICellNumber : IButtonStatus
{
	/// <summary>
	/// 本数字をセットする
	/// </summary>
	/// <param name="number"></param>
	public void ISetNumber(int number);
	/// <summary>
	/// メモ数字をセットする
	/// </summary>
	/// <param name="number"></param>
	public void ISetMemoNumber(int number);
	/// <summary>
	/// 答えの数字を取得する
	/// </summary>
	/// <returns></returns>
	public int IGetAnswerNumber();
}

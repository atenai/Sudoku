using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IButtonStatus
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
	public int IGetAnswerNumber();
}

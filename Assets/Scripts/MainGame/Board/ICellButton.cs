using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ICellButton
{
	public int IGetAnswerNumber();
	public bool IGetIsInteractable();

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
	public void ISetHighlight(bool isSelected);
	public void ISetColor(Color color);
	public void ISetLockCell();
}

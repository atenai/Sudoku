using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IButtonStatus
{
	/// <summary>
	/// ボタンの状態を取得する
	/// </summary>
	/// <returns></returns>
	public bool IGetIsInteractable();
	/// <summary>
	/// ハイライト状態を設定する
	/// </summary>
	/// <param name="isSelected"></param>
	public void ISetHighlight(bool isSelected);
	/// <summary>
	/// 色を設定する
	/// </summary>
	/// <param name="color"></param>
	public void ISetColor(Color color);
	/// <summary>
	/// セルをロックする
	/// </summary>
	public void ISetLockCell();
}

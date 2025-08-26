using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IMemo
{
	/// <summary>
	/// メモモード切り替え
	/// </summary>
	public void IToggleMemoMode();

	/// <summary>
	/// メモモードの状態を取得
	/// </summary>
	/// <returns>メモモードの状態</returns>
	public bool GetMemoMode();
}

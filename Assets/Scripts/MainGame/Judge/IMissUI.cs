using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IMissUI
{
	/// <summary>
	/// ミスカウントを設定する
	/// </summary>
	/// <param name="missCount"></param>
	public void ISetMissCount(int missCount);
	/// <summary>
	/// 失敗数を設定する
	/// </summary>
	/// <param name="failNumber"></param>
	public void ISetFailNumber(int failNumber);
}

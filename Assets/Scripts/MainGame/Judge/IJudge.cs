using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IJudge
{
	/// <summary>
	/// 答えをチェックする
	/// </summary>
	/// <param name="cell"></param>
	/// <param name="number"></param>
	public void ICheckAnswer(ICellNumber cell, int number);

	/// <summary>
	/// ミス数
	/// </summary>
	public int GetFailNumber();

	/// <summary>
	/// ミスカウント
	/// </summary>
	public int GetMissCount();
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IMainGameUI
{
	/// <summary>
	/// 盤面UIへのアクセス
	/// </summary>
	public IBoard IBoard { get; }

	/// <summary>
	/// ミスUIへのアクセス
	/// </summary>
	public IMissUI IMissUI { get; }
}

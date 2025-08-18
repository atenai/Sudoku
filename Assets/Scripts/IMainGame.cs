using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IMainGame
{
	/// <summary>
	/// メインゲームUI
	/// </summary>
	public IMainGameUI IMainGameUI { get; }

	/// <summary>
	/// メインゲームロジック
	/// </summary>
	public IMainGameLogic IMainGameLogic { get; }
}

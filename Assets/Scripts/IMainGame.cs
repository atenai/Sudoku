using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IMainGame
{
	/// 盤面サイズ（= 9）
	//int BoardSize { get; }

	/// メモモード（ON/OFF）
	bool MemoMode { get; set; }

	/// UI への窓口
	public IMainGameUI IMainGameUI { get; }

	/// ロジックへの窓口
	public IMainGameLogic IMainGameLogic { get; }
}

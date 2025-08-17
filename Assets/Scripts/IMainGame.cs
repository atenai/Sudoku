using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IMainGame
{
	/// UI への窓口
	public IMainGameUI IMainGameUI { get; }

	/// ロジックへの窓口
	public IMainGameLogic IMainGameLogic { get; }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IMainGameLogic : IGridData
{
	public IMainGameInput IMainGameInput { get; }
	public IJudge IJudge { get; }
}

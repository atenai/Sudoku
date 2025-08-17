using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IBoard
{
	/// <summary>盤面セルを生成して配置する</summary>
	/// <param name="gridData">問題/解答の読み取り口</param>
	void CreateCell(IGridData gridData);
}

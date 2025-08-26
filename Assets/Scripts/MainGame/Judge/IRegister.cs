using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IRegister
{
	/// <summary>
	/// セルを登録する
	/// </summary>
	/// <param name="cells"></param>
	public void IRegisterCells(ICellNumber[,] cells);
}

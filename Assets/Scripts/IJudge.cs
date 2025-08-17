using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IJudge
{
	int FailNumber { get; }
	int MissCount { get; }

	void IRegisterCells(ICellButton[,] cells);
	void ICheckAnswer(ICellButton cell, int number);
}

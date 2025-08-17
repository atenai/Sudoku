using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IJudge
{
	int FailNumber { get; }
	int MissCount { get; }

	void RegisterCells(ICellButton[,] cells);
	void CheckAnswer(ICellButton cell, int number);
}

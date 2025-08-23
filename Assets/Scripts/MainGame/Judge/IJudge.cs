using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IJudge
{
	void IRegisterCells(ICellButton[,] cells);
	void ICheckAnswer(ICellButton cell, int number);
}

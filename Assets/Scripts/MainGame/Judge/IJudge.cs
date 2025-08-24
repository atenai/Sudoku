using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IJudge
{
	public void IRegisterCells(ICellButton[,] cells);
	public void ICheckAnswer(ICellButton cell, int number);
}

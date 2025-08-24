using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ICellButton
{
	public int GetAnswerNumber();

	bool IIsInteractable { get; }

	void ISetNumber(int number);
	void IToggleMemo(int number);
	void IHighlight(bool isSelected);
	void ISetColor(Color color);
	void ILockCell();
}

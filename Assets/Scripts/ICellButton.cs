using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ICellButton
{
	int Row { get; }
	int Col { get; }
	int AnswerNumber { get; }
	int QuestionNumber { get; }
	bool IsInteractable { get; }

	void SetNumber(int number);
	void ToggleMemo(int number);
	void Highlight(bool isSelected);
	void SetColor(Color color);
	void LockCell();
}

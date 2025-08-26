using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ICellNumber : IButtonStatus
{
	public bool IGetIsInteractable();
	public void ISetHighlight(bool isSelected);
	public void ISetColor(Color color);
	public void ISetLockCell();
}

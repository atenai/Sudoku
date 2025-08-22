using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainGameUI : MonoBehaviour
{
	/// <summary>
	/// ボード
	/// </summary>
	[SerializeField] private Board board;

	/// <summary>
	/// ミスUI
	/// </summary>
	[SerializeField] private MissUI missUI;

	[SerializeField] private InputNumberButton[] inputNumberButtons;

	[SerializeField] private ClearButton clearButton;

	[SerializeField] private MemoButton memoButton;

	public IBoard Board => board;

	public IMissUI MissUI => missUI;

	public InputNumberButton[] InputNumberButtons => inputNumberButtons;

	public ClearButton ClearButton => clearButton;

	public MemoButton MemoButton => memoButton;
}

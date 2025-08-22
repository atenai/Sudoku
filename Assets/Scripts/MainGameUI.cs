using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainGameUI : MonoBehaviour, IMainGameUI
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

	public IBoard IBoard => board;

	public IMissUI IMissUI => missUI;

	public InputNumberButton[] InputNumberButtons => inputNumberButtons;

	public ClearButton ClearButton => clearButton;

	public MemoButton MemoButton => memoButton;
}

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

	public IBoard IBoard => board;

	public IMissUI IMissUI => missUI;
}

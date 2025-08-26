using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// メインゲームロジッククラス
/// </summary>
public class MainGameLogic : IGridData
{
	/// <summary>
	/// 答えグリッド
	/// </summary>
	private int[,] answerGrid = new int[MainGame.Cell_Number, MainGame.Cell_Number];

	/// <summary>
	/// 問題グリッド
	/// </summary>
	private int[,] questionGrid = new int[MainGame.Cell_Number, MainGame.Cell_Number];

	private MainGameInput mainGameInput;
	public IInputNumber IInputNumber
	{
		get { return mainGameInput; }
	}
	public ISelectCell ISelectCell
	{
		get { return mainGameInput; }
	}
	public IMemo IMemo
	{
		get { return mainGameInput; }
	}

	private Judge judge;
	public IJudge IJudge
	{
		get { return judge; }
	}
	public IRegister IRegister
	{
		get { return judge; }
	}

	/// <summary>
	/// コンストラクタ
	/// </summary>
	/// <param name="mainGame"></param>
	/// <param name="difficultyType">設定した難易度</param>
	public MainGameLogic(IMissUI missUI, MainGameSetting.DifficultyType difficultyType)
	{
		GenerateGrid generateGrid = new GenerateGrid(answerGrid, questionGrid, difficultyType);
		mainGameInput = new MainGameInput();
		judge = new Judge(missUI, mainGameInput, difficultyType);
	}

	/// <summary>
	/// 答えグリッドの数値
	/// </summary>
	/// </remarks>
	/// <param name="row">横</param>
	/// <param name="col">縦</param>
	/// <returns>答えグリッドの数値</returns>
	public int IGetAnswerGridNumber(int row, int col)
	{
		int[,] answerGridCopy = (int[,])answerGrid.Clone();
		return answerGridCopy[row, col];
	}

	/// <summary>
	/// 問題グリッドの数値
	/// </summary>
	/// <param name="row">横</param>
	/// <param name="col">縦</param>
	/// <returns>問題グリッドの数値</returns>
	public int IGetQuestionGridNumber(int row, int col)
	{
		int[,] questionGridCopy = (int[,])questionGrid.Clone();
		return questionGridCopy[row, col];
	}
}

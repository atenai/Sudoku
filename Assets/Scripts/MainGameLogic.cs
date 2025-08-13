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
	private Judge judge;

	public MainGameInput MainGameInput => mainGameInput;
	public Judge Judge => judge;

	/// <summary>
	/// コンストラクタ
	/// </summary>
	/// <param name="mainGame"></param>
	/// <param name="difficultyType">設定した難易度</param>
	public MainGameLogic(MainGame mainGame, MainGameSetting.DifficultyType difficultyType)
	{
		GenerateGrid generateGrid = new GenerateGrid(answerGrid, questionGrid, difficultyType);
		mainGameInput = new MainGameInput(mainGame);
		judge = new Judge(mainGame, difficultyType);
	}

	/// <summary>
	/// 答えグリッドの数値
	/// </summary>
	/// </remarks>
	/// <param name="row">横</param>
	/// <param name="col">縦</param>
	/// <returns>答えグリッドの数値</returns>
	public int GetAnswerGridNumber(int row, int col)
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
	public int GetQuestionGridNumber(int row, int col)
	{
		int[,] questionGridCopy = (int[,])questionGrid.Clone();
		return questionGridCopy[row, col];
	}
}

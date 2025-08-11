using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// メインゲームロジッククラス
/// </summary>
public class MainGameLogic
{
	/// <summary>
	/// 答えグリッド
	/// </summary>
	private int[,] answerGrid = new int[MainGame.Cell_Number, MainGame.Cell_Number];

	/// <summary>
	/// 問題グリッド
	/// </summary>
	private int[,] questionGrid = new int[MainGame.Cell_Number, MainGame.Cell_Number];

	/// <summary>
	/// 答えグリッドのプロパティ
	/// </summary>
	public int[,] AnswerGrid => answerGrid;

	/// <summary>
	/// 問題グリッドのプロパティ
	/// </summary>
	public int[,] QuestionGrid => questionGrid;

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
}

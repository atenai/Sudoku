using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// メインゲームロジックファサードクラス
/// </summary>
/// <remarks>
/// ファサードパターンを使用したクラス
/// 各サブクラスをここにまとめて、リストとして外部に公開する
/// </remarks>
public static class MainGameLogicFacade
{
	public static int EmptyCell(MainGameSetting.DifficultyType difficultyType)
	{
		return new GenerateGrid().EmptyCell(difficultyType);
	}

	public static void CreateAnswerGrid(int row, int col, int[,] aGrid)
	{
		new GenerateGrid().CreateAnswerGrid(row, col, aGrid);
	}

	public static void CreateQuestionGrid(int[,] qGrid, int emptyCell)
	{
		new GenerateGrid().CreateQuestionGrid(qGrid, emptyCell);
	}

	public static void DebugGrid(int[,] grid)
	{
		new GenerateGrid().DebugGrid(grid);
	}

	public static bool CheckAnswer(int answerNumber, int inputNumber)
	{
		return new Judge().CheckAnswer(answerNumber, inputNumber);
	}

	public static int FailNumber(MainGameSetting.DifficultyType difficultyType)
	{
		return new Result().FailNumber(difficultyType);
	}

	public static void Correct(bool isAllCorrect)
	{
		new Result().Correct(isAllCorrect);
	}

	public static void InCorrect(ref int missCount, int failNumber)
	{
		new Result().InCorrect(ref missCount, failNumber);
	}

	public static void ToggleMemoMode(ref bool memoMode)
	{
		new MainGameInput().ToggleMemoMode(ref memoMode);
	}
}

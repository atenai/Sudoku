using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// メインゲームロジックファサードクラス
/// </summary>
/// <remarks>
/// ファサードパターンを使用したクラス
/// 各サブクラスをここにまとめて、リストとして外部に公開する
/// 数独のルール機能を管理
/// </remarks>
public class MainGameLogicFacade
{
	private GenerateGrid generateGrid = new GenerateGrid();
	private Judge judge = new Judge();
	private Result result = new Result();
	private MainGameInput mainGameInput = new MainGameInput();

	public int EmptyCell(MainGameSetting.DifficultyType difficultyType)
	{
		return generateGrid.EmptyCell(difficultyType);
	}

	public void CreateAnswerGrid(int row, int col, int[,] aGrid)
	{
		generateGrid.CreateAnswerGrid(row, col, aGrid);
	}

	public void CreateQuestionGrid(int[,] qGrid, int emptyCell)
	{
		generateGrid.CreateQuestionGrid(qGrid, emptyCell);
	}

	public void DebugGrid(int[,] grid)
	{
		generateGrid.DebugGrid(grid);
	}

	public bool CheckAnswer(int answerNumber, int inputNumber)
	{
		return judge.CheckAnswer(answerNumber, inputNumber);
	}

	public int FailNumber(MainGameSetting.DifficultyType difficultyType)
	{
		return result.FailNumber(difficultyType);
	}

	public void Correct(bool isAllCorrect)
	{
		result.Correct(isAllCorrect);
	}

	public void InCorrect(ref int missCount, int failNumber)
	{
		result.InCorrect(ref missCount, failNumber);
	}

	public bool IsAllCorrect(int[,] cGrid, int[,] aGrid)
	{
		return result.IsAllCorrect(cGrid, aGrid);
	}

	public void ToggleMemoMode(ref bool memoMode)
	{
		mainGameInput.ToggleMemoMode(ref memoMode);
	}

	public int HintCount(MainGameSetting.DifficultyType difficultyType)
	{
		return mainGameInput.HintCount(difficultyType);
	}
}

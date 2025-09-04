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
	private GenerateGrid generateGrid;
	private Judge judge;
	private Result result;
	private MainGameInput mainGameInput;

	public int EmptyCell(MainGameSetting.DifficultyType difficultyType)
	{
		if (generateGrid == null)
		{
			generateGrid = new GenerateGrid();
		}
		return generateGrid.EmptyCell(difficultyType);
	}

	public void CreateAnswerGrid(int row, int col, int[,] aGrid)
	{
		if (generateGrid == null)
		{
			generateGrid = new GenerateGrid();
		}
		generateGrid.CreateAnswerGrid(row, col, aGrid);
	}

	public void CreateQuestionGrid(int[,] qGrid, int emptyCell)
	{
		if (generateGrid == null)
		{
			generateGrid = new GenerateGrid();
		}
		generateGrid.CreateQuestionGrid(qGrid, emptyCell);
	}

	public void DebugGrid(int[,] grid)
	{
		if (generateGrid == null)
		{
			generateGrid = new GenerateGrid();
		}
		generateGrid.DebugGrid(grid);
	}

	public bool CheckAnswer(int answerNumber, int inputNumber)
	{
		if (judge == null)
		{
			judge = new Judge();
		}
		return judge.CheckAnswer(answerNumber, inputNumber);
	}

	public int FailNumber(MainGameSetting.DifficultyType difficultyType)
	{
		if (result == null)
		{
			result = new Result();
		}
		return result.FailNumber(difficultyType);
	}

	public void Correct(bool isAllCorrect)
	{
		if (result == null)
		{
			result = new Result();
		}
		result.Correct(isAllCorrect);
	}

	public void InCorrect(ref int missCount, int failNumber)
	{
		if (result == null)
		{
			result = new Result();
		}
		result.InCorrect(ref missCount, failNumber);
	}

	public bool IsAllCorrect(int[,] cGrid, int[,] aGrid)
	{
		if (result == null)
		{
			result = new Result();
		}
		return result.IsAllCorrect(cGrid, aGrid);
	}

	public void ToggleMemoMode(ref bool memoMode)
	{
		if (mainGameInput == null)
		{
			mainGameInput = new MainGameInput();
		}
		mainGameInput.ToggleMemoMode(ref memoMode);
	}
}

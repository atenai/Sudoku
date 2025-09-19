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
	private Timer timer = new Timer();

	public void CreateGrid(int[,] answerGrid, int[,] questionGrid, MainGameSetting mainGameSetting)
	{
		generateGrid.CreateGrid(answerGrid, questionGrid, mainGameSetting);
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

	public bool InCorrect(ref int missCount, int failNumber)
	{
		return result.InCorrect(ref missCount, failNumber);
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

	public void InitTimerSystem(MainGameSetting.DifficultyType difficultyType)
	{
		timer.InitTimerSystem(difficultyType);
	}

	public bool UpdateTimerSystem()
	{
		return timer.UpdateTimerSystem();
	}

	public int GetMinute()
	{
		return timer.GetMinute();
	}

	public float GetSeconds()
	{
		return timer.GetSeconds();
	}
}

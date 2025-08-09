using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// メインゲームロジッククラス
/// </summary>
public class MainGameLogic
{
	/// <summary>
	/// 区切りブロック
	/// </summary>
	private const int Separator_Block = 3;

	/// <summary>
	/// 答えグリッド
	/// </summary>
	private int[,] answerGrid = new int[MainGame.Cell_Number, MainGame.Cell_Number];

	/// <summary>
	/// 問題グリッド
	/// </summary>
	private int[,] questionGrid = new int[MainGame.Cell_Number, MainGame.Cell_Number];

	/// <summary>
	/// 難易度に応じた空白数
	/// </summary>
	private int emptyCell = 55;

	/// <summary>
	/// 答えグリッドのプロパティ
	/// </summary>
	public int[,] AnswerGrid => answerGrid;

	/// <summary>
	/// 問題グリッドのプロパティ
	/// </summary>
	public int[,] QuestionGrid => questionGrid;

	public MainGameLogic(MainGameSetting.DifficultyType difficultyType)
	{
		// 0. 難易度設定
		SetEmptyCell(difficultyType);

		// 1. 完全な数独を生成
		CreateAnswerGrid(0, 0, answerGrid);
		Debug.Log("<color=red>答えを生成しました！</color>");
		DebugGrid(answerGrid);

		// 2. 完全解をコピーして問題用にする
		System.Array.Copy(answerGrid, questionGrid, answerGrid.Length);

		// 3. マスを1つずつ消して唯一解を保つ
		CreateQuestionGrid();
		Debug.Log("<color=blue>問題を生成しました！</color>");
		DebugGrid(questionGrid);
	}

	/// <summary>
	/// 空白数を設定
	/// </summary>
	/// <param name="difficultyType"></param>
	private void SetEmptyCell(MainGameSetting.DifficultyType difficultyType)
	{
		switch (difficultyType)
		{
			case MainGameSetting.DifficultyType.Easy:
				emptyCell = 5;
				break;
			case MainGameSetting.DifficultyType.Normal:
				emptyCell = 20;
				break;
			case MainGameSetting.DifficultyType.Hard:
				emptyCell = 55;
				break;
		}
	}

	/// <summary>
	/// 答えグリッドを作成
	/// </summary>
	/// <remarks>
	/// 再帰的に数独の解を生成します。
	/// 各マスに1から9の数字を配置し、
	/// そのマスに数字を配置できるかチェックします。
	/// 成功した場合は次のマスに進み、
	/// すべてのマスが埋まったら成功とします。
	/// </remarks>
	/// <param name="row"></param>
	/// <param name="col"></param>
	/// <param name="aGrid"></param>
	/// <returns></returns>
	private bool CreateAnswerGrid(int row, int col, int[,] aGrid)
	{
		if (row == MainGame.Cell_Number)
		{
			// 全てのマスが埋まったら成功
			return true;
		}

		int nextRow = (col == MainGame.Cell_Number - 1) ? row + 1 : row;
		int nextCol = (col + 1) % MainGame.Cell_Number;

		List<int> numbers = new List<int>() { 1, 2, 3, 4, 5, 6, 7, 8, 9 };
		ShuffleNumbers(numbers);

		foreach (int num in numbers)
		{
			if (CheckNumber(row, col, num, aGrid) == true)
			{
				aGrid[row, col] = num;
				if (CreateAnswerGrid(nextRow, nextCol, aGrid) == true)
				{
					return true;
				}
				aGrid[row, col] = 0;
			}
		}

		return false;
	}

	/// <summary>
	/// 指定された行と列のマスに数字が配置できるかチェックします。
	/// 行に同じ数字がある場合はfalseを返し、
	/// 列に同じ数字がある場合はfalseを返し、
	/// 3×3ブロックに同じ数字がある場合はfalseを返します。
	/// </summary>
	/// <param name="row">横</param>
	/// <param name="col">縦</param>
	/// <param name="num">数値</param>
	/// <param name="grid">チェックするグリッド</param>
	/// <returns>数字が配置できる場合はtrueを返します。数字が配置できない場合はfalseを返します。</returns>
	private bool CheckNumber(int row, int col, int num, int[,] grid)
	{
		for (int i = 0; i < MainGame.Cell_Number; i++)
		{
			if (grid[row, i] == num) { return false; }// 行に同じ数字があるか
			if (grid[i, col] == num) { return false; }// 列に同じ数字があるか
		}

		int startRow = row / Separator_Block * Separator_Block;
		int startCol = col / Separator_Block * Separator_Block;
		for (int r = 0; r < Separator_Block; r++)
		{
			for (int c = 0; c < Separator_Block; c++)
			{
				if (grid[startRow + r, startCol + c] == num) { return false; }// 3×3ブロックに同じ数字があるか
			}
		}

		return true;
	}

	/// <summary>
	/// リスト内の要素をシャッフルします。
	/// </summary>
	/// <typeparam name="T">あらゆる型</typeparam>
	/// <param name="list">シャフルするリスト</param>
	private void ShuffleNumbers<T>(List<T> list)
	{
		for (int i = 0; i < list.Count; i++)
		{
			int rand = Random.Range(i, list.Count);
			T temp = list[i];
			list[i] = list[rand];
			list[rand] = temp;
		}
	}

	/// <summary>
	/// 問題グリッドを生成します。
	/// 問題グリッドは、空白のマスを持ち、唯一の解を持つように調整されます。
	/// 空白のマスの数は、`emptyCellTarget`で指定された数に基づいています。
	/// </summary>
	private void CreateQuestionGrid()
	{
		List<Vector2Int> cells = new List<Vector2Int>();
		for (int r = 0; r < MainGame.Cell_Number; r++)
		{
			for (int c = 0; c < MainGame.Cell_Number; c++)
			{
				cells.Add(new Vector2Int(r, c));
			}
		}
		ShuffleNumbers(cells);

		int removed = 0;
		foreach (var cell in cells)
		{
			if (emptyCell <= removed) { break; }

			int backup = questionGrid[cell.x, cell.y];
			questionGrid[cell.x, cell.y] = 0;

			int solutions = CountSolutions((int[,])questionGrid.Clone());
			if (solutions == 1)
			{
				removed++;
			}
			else
			{
				// 一意解でない → 元に戻す
				questionGrid[cell.x, cell.y] = backup;
			}
		}
	}

	/// <summary>
	/// 解の数
	/// </summary>
	/// <param name="board"></param>
	/// <returns></returns>
	private int CountSolutions(int[,] board)
	{
		int count = 0;
		CheckSolutions(0, 0, board, ref count);
		return count;
	}

	/// <summary>
	/// 解の数をチェックします。
	/// </summary>
	/// <param name="row"></param>
	/// <param name="col"></param>
	/// <param name="board"></param>
	/// <param name="count"></param>
	/// <returns></returns>
	private bool CheckSolutions(int row, int col, int[,] board, ref int count)
	{
		if (count >= 2)
		{
			//解が2個以上見つかったら打ち切り
			return false;
		}

		//横行が最後まで来たら
		//全てのマスが埋まったら中身を実行する
		if (row == MainGame.Cell_Number)
		{
			//解が見つかったら+1
			count++;
			return true;
		}

		//次のセル位置を計算
		// (Rowは[横][行],Colは[縦][列])
		// (row,col) の進み方
		// (0,0) → (0,1) → (0,2) ... → (0,8)
		//  ↓
		// (1,0) → (1,1) → (1,2) ... → (1,8)
		//  ↓
		// (2,0) → ...
		// colが最後の縦列なら次の横行へ、そうでなければ次の縦列へ進む
		int nextRow = (col == MainGame.Cell_Number - 1) ? row + 1 : row;
		// 次の縦列は、現在の縦列が最後の縦列なら0に戻り、そうでなければ+1
		int nextCol = (col + 1) % MainGame.Cell_Number;

		//既に数字があるならスキップ
		if (board[row, col] != 0)
		{
			return CheckSolutions(nextRow, nextCol, board, ref count);
		}

		//候補を1～9まで順に試す
		for (int num = 1; num <= MainGame.Cell_Number; num++)
		{
			if (CheckNumber(row, col, num, board) == true)
			{
				board[row, col] = num;
				CheckSolutions(nextRow, nextCol, board, ref count);
				board[row, col] = 0;
			}
		}
		return false;
	}

	/// <summary>
	/// デバッグ用：グリッドの内容をコンソールに出力します。
	/// </summary>
	/// <param name="grid">デバッグログに表示したいグリッド</param>
	private void DebugGrid(int[,] grid)
	{
		string s = "";
		for (int r = 0; r < MainGame.Cell_Number; r++)
		{
			s = s + "|";
			for (int c = 0; c < MainGame.Cell_Number; c++)
			{
				s = s + grid[r, c].ToString();
				if (c % 3 == 2)
				{
					s = s + "|";
				}
			}
			s = s + "\n";
		}
		Debug.Log(s);
	}
}

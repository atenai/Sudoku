using System.Collections.Generic;
using UnityEngine;

public class 数独唯一解生成 : MonoBehaviour
{
	const int NUM = 9;
	const int BLOCK = 3;

	int[,] solvedGrid = new int[NUM, NUM];
	int[,] puzzleGrid = new int[NUM, NUM];

	[SerializeField] int emptyCellTarget = 40; // 難易度に応じた空白数

	[SerializeField] バックトラック法 backtrackMethod;

	void Start()
	{
		// 1. 完全な数独を生成
		GenerateSolvedGrid(ref solvedGrid);

		// 2. 完全解をコピーして問題用にする
		System.Array.Copy(solvedGrid, puzzleGrid, solvedGrid.Length);

		// 3. マスを1つずつ消して唯一解を保つ
		CreateUniquePuzzle();

		Debug.Log("<color=green>唯一解の問題を生成しました！</color>");
		DebugGrid(ref puzzleGrid);

		backtrackMethod.StartBacktracking(puzzleGrid);
	}

	// 完全な数独を作成
	void GenerateSolvedGrid(ref int[,] grid)
	{
		FillGrid(0, 0, ref grid);
	}

	bool FillGrid(int row, int col, ref int[,] grid)
	{
		if (row == NUM) return true; // 完成

		int nextRow = (col == NUM - 1) ? row + 1 : row;
		int nextCol = (col + 1) % NUM;

		List<int> numbers = new List<int>() { 1, 2, 3, 4, 5, 6, 7, 8, 9 };
		Shuffle(numbers);

		foreach (int num in numbers)
		{
			if (IsValid(row, col, num, ref grid))
			{
				grid[row, col] = num;
				if (FillGrid(nextRow, nextCol, ref grid))
					return true;
				grid[row, col] = 0;
			}
		}
		return false;
	}

	void CreateUniquePuzzle()
	{
		List<Vector2Int> cells = new List<Vector2Int>();
		for (int r = 0; r < NUM; r++)
			for (int c = 0; c < NUM; c++)
				cells.Add(new Vector2Int(r, c));

		Shuffle(cells);

		int removed = 0;
		foreach (var cell in cells)
		{
			if (removed >= emptyCellTarget) break;

			int backup = puzzleGrid[cell.x, cell.y];
			puzzleGrid[cell.x, cell.y] = 0;

			int solutions = CountSolutions((int[,])puzzleGrid.Clone());

			if (solutions == 1)
			{
				removed++;
			}
			else
			{
				// 一意解でない → 元に戻す
				puzzleGrid[cell.x, cell.y] = backup;
			}
		}
	}

	int CountSolutions(int[,] board)
	{
		int count = 0;
		Solve(0, 0, board, ref count);
		return count;
	}

	bool Solve(int row, int col, int[,] board, ref int count)
	{
		if (count >= 2) return false; // 2個以上見つかったら打ち切り

		if (row == NUM)
		{
			count++;
			return true;
		}

		int nextRow = (col == NUM - 1) ? row + 1 : row;
		int nextCol = (col + 1) % NUM;

		if (board[row, col] != 0)
			return Solve(nextRow, nextCol, board, ref count);

		for (int num = 1; num <= NUM; num++)
		{
			if (IsValid(row, col, num, ref board))
			{
				board[row, col] = num;
				Solve(nextRow, nextCol, board, ref count);
				board[row, col] = 0;
			}
		}
		return false;
	}

	bool IsValid(int row, int col, int num, ref int[,] grid)
	{
		for (int i = 0; i < NUM; i++)
		{
			if (grid[row, i] == num) return false;
			if (grid[i, col] == num) return false;
		}

		int startRow = row / BLOCK * BLOCK;
		int startCol = col / BLOCK * BLOCK;
		for (int r = 0; r < BLOCK; r++)
			for (int c = 0; c < BLOCK; c++)
				if (grid[startRow + r, startCol + c] == num)
					return false;

		return true;
	}

	void Shuffle<T>(List<T> list)
	{
		for (int i = 0; i < list.Count; i++)
		{
			int rand = Random.Range(i, list.Count);
			T temp = list[i];
			list[i] = list[rand];
			list[rand] = temp;
		}
	}

	void DebugGrid(ref int[,] grid)
	{
		string s = "";
		for (int r = 0; r < NUM; r++)
		{
			s += "|";
			for (int c = 0; c < NUM; c++)
			{
				s += grid[r, c].ToString();
				if (c % 3 == 2) s += "|";
			}
			s += "\n";
		}
		Debug.Log(s);
	}
}

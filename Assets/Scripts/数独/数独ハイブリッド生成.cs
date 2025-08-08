using System.Collections.Generic;
using UnityEngine;

public class 数独ハイブリッド生成 : MonoBehaviour
{
	const int NUM = 9;
	const int BLOCK = 3;

	int[,] solvedGrid = new int[NUM, NUM];
	int[,] puzzleGrid = new int[NUM, NUM];

	[SerializeField] int emptyCellTarget = 40; // 難易度に応じた空白数

	[SerializeField] バックトラック法 backtrackMethod;

	void Start()
	{
		// 1. 完全解を生成
		GenerateSolvedGrid(ref solvedGrid);

		// 2. 完全解をコピーして問題用に
		System.Array.Copy(solvedGrid, puzzleGrid, solvedGrid.Length);

		// 3. ランダムに削除（高速）
		RandomRemove();

		// 4. 微調整フェーズ
		EnsureUniqueSolution();

		Debug.Log("<color=green>唯一解の問題を生成しました！（ハイブリッド方式）</color>");
		DebugGrid(ref puzzleGrid);

		backtrackMethod.StartBacktracking(puzzleGrid);
	}

	// 完全な数独を生成
	void GenerateSolvedGrid(ref int[,] grid)
	{
		FillGrid(0, 0, ref grid);
	}

	bool FillGrid(int row, int col, ref int[,] grid)
	{
		if (row == NUM) return true;

		int nextRow = (col == NUM - 1) ? row + 1 : row;
		int nextCol = (col + 1) % NUM;

		List<int> numbers = new List<int>() { 1, 2, 3, 4, 5, 6, 7, 8, 9 };
		Shuffle(numbers);

		foreach (int num in numbers)
		{
			if (IsValid(row, col, num, ref grid))
			{
				grid[row, col] = num;
				if (FillGrid(nextRow, nextCol, ref grid)) return true;
				grid[row, col] = 0;
			}
		}
		return false;
	}

	// 高速：ランダム削除
	void RandomRemove()
	{
		int removed = 0;
		while (removed < emptyCellTarget)
		{
			int r = Random.Range(0, NUM);
			int c = Random.Range(0, NUM);

			if (puzzleGrid[r, c] != 0)
			{
				puzzleGrid[r, c] = 0;
				removed++;
			}
		}
	}

	// 安全：唯一性保証の調整
	void EnsureUniqueSolution()
	{
		bool unique = false;

		while (!unique)
		{
			int solutions = CountSolutions((int[,])puzzleGrid.Clone());

			if (solutions == 1)
			{
				unique = true;
			}
			else
			{
				Debug.Log("<color=yellow>調整中…（解：" + solutions + "個）</color>");
				// ランダムに埋め戻す → 再試行
				RevertOneCell();
			}
		}
	}

	// ランダムに空欄を1つ復活させる
	void RevertOneCell()
	{
		List<Vector2Int> empties = new List<Vector2Int>();
		for (int r = 0; r < NUM; r++)
			for (int c = 0; c < NUM; c++)
				if (puzzleGrid[r, c] == 0)
					empties.Add(new Vector2Int(r, c));

		if (empties.Count == 0) return;

		Vector2Int cell = empties[Random.Range(0, empties.Count)];
		puzzleGrid[cell.x, cell.y] = solvedGrid[cell.x, cell.y];
	}

	// 解の数を数える
	int CountSolutions(int[,] board)
	{
		int count = 0;
		Solve(0, 0, board, ref count);
		return count;
	}

	bool Solve(int row, int col, int[,] board, ref int count)
	{
		if (count >= 2) return false;

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

	// 妥当性チェック
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

	// シャッフル
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

	// デバッグ表示
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

using System.Collections.Generic;
using UnityEngine;

public class 数独MRV生成 : MonoBehaviour
{
	const int NUM = 9;
	const int BLOCK = 3;

	int[,] solvedGrid = new int[NUM, NUM];
	int[,] puzzleGrid = new int[NUM, NUM];

	[SerializeField] int emptyCellTarget = 40;

	[SerializeField] バックトラック法 backtrackMethod;

	void Start()
	{
		// 1. 完全な数独を生成
		GenerateSolvedGrid(ref solvedGrid);

		// 2. 完全解をコピーして問題用にする
		System.Array.Copy(solvedGrid, puzzleGrid, solvedGrid.Length);

		// 3. MRVを利用して唯一解保証の問題を生成
		CreateUniquePuzzle();

		Debug.Log("<color=green>MRV戦略で唯一解問題を生成しました！</color>");
		DebugGrid(ref puzzleGrid);

		backtrackMethod.StartBacktracking(puzzleGrid);
	}

	// 完全な数独を作成
	void GenerateSolvedGrid(ref int[,] grid)
	{
		FillGrid(ref grid);
	}

	bool FillGrid(ref int[,] grid)
	{
		Vector2Int cell = FindCellWithFewestCandidates(grid);
		if (cell.x == -1) return true; // 全セル埋まった

		List<int> candidates = GetCandidates(cell.x, cell.y, grid);
		Shuffle(candidates);

		foreach (int num in candidates)
		{
			grid[cell.x, cell.y] = num;
			if (FillGrid(ref grid)) return true;
			grid[cell.x, cell.y] = 0;
		}
		return false;
	}

	// 唯一解保証の問題を作成
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
				puzzleGrid[cell.x, cell.y] = backup;
			}
		}
	}

	// MRV戦略：候補が最小のセルを探す
	Vector2Int FindCellWithFewestCandidates(int[,] grid)
	{
		int minCount = 10;
		Vector2Int bestCell = new Vector2Int(-1, -1);

		for (int r = 0; r < NUM; r++)
		{
			for (int c = 0; c < NUM; c++)
			{
				if (grid[r, c] == 0)
				{
					List<int> candidates = GetCandidates(r, c, grid);
					if (candidates.Count < minCount)
					{
						minCount = candidates.Count;
						bestCell = new Vector2Int(r, c);
						if (minCount == 1) return bestCell; // 最適な早期終了
					}
				}
			}
		}
		return bestCell;
	}

	List<int> GetCandidates(int row, int col, int[,] grid)
	{
		List<int> candidates = new List<int>();
		for (int num = 1; num <= NUM; num++)
			if (IsValid(row, col, num, ref grid))
				candidates.Add(num);
		return candidates;
	}

	// 解を数える（MRV戦略付き）
	int CountSolutions(int[,] board)
	{
		int count = 0;
		Solve(board, ref count);
		return count;
	}

	bool Solve(int[,] board, ref int count)
	{
		if (count >= 2) return false; // 2解以上なら打ち切り

		Vector2Int cell = FindCellWithFewestCandidates(board);
		if (cell.x == -1)
		{
			count++;
			return true;
		}

		List<int> candidates = GetCandidates(cell.x, cell.y, board);
		foreach (int num in candidates)
		{
			board[cell.x, cell.y] = num;
			Solve(board, ref count);
			board[cell.x, cell.y] = 0;
			if (count >= 2) return false;
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

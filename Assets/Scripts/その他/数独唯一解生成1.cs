using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class 数独唯一解生成1 : MonoBehaviour
{
	const int Cell_Number = 9;
	const int Separator_Block = 3;

	/// <summary>
	/// 答えグリッド
	/// </summary>
	int[,] answerGrid = new int[Cell_Number, Cell_Number];
	/// <summary>
	/// 問題グリッド
	/// </summary>
	int[,] questionGrid = new int[Cell_Number, Cell_Number];

	[SerializeField] int emptyCell = 55; // 難易度に応じた空白数

	[SerializeField] バックトラック法 backtrackMethod;

	int[,] originalBoard = new int[9, 9]
	{
		{5, 3, 0, 0, 7, 0, 0, 0, 0},
		{6, 0, 0, 1, 9, 5, 0, 0, 0},
		{0, 9, 8, 0, 0, 0, 0, 6, 0},
		{8, 0, 0, 0, 6, 0, 0, 0, 3},
		{4, 0, 0, 8, 0, 3, 0, 0, 1},
		{7, 0, 0, 0, 2, 0, 0, 0, 6},
		{0, 6, 0, 0, 0, 0, 2, 8, 0},
		{0, 0, 0, 4, 1, 9, 0, 0, 5},
		{0, 0, 0, 0, 8, 0, 0, 7, 9}
	};

	void Start()
	{
		// 1. 完全な数独を生成
		CreateAnswerGrid(0, 0, ref answerGrid);
		Debug.Log("<color=red>答えを生成しました！</color>");
		DebugGrid(ref answerGrid);

		// 2. 完全解をコピーして問題用にする
		System.Array.Copy(answerGrid, questionGrid, answerGrid.Length);

		// 3. マスを1つずつ消して唯一解を保つ
		CreateQuestionGrid();
		Debug.Log("<color=blue>問題を生成しました！</color>");
		DebugGrid(ref questionGrid);

		backtrackMethod.StartBacktracking(questionGrid);

		CreateCell();

		// 縦（行）と横（列）を持つデータの表形式の配列
		// 型名 変数名[縦][横] = {要素0,0の値, 要素0,1の値, …},
		//                      {要素1,0の値, 要素1,1の値, …},
		//PrintGrid(originalBoard);
		//Debug.Log("<color=green>オリジナルボード(3,0) : " + originalBoard[3, 0] + " = 8</color>");
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
	bool CreateAnswerGrid(int row, int col, ref int[,] aGrid)
	{
		if (row == Cell_Number)
		{
			// 全てのマスが埋まったら成功
			return true;
		}

		int nextRow = (col == Cell_Number - 1) ? row + 1 : row;
		int nextCol = (col + 1) % Cell_Number;

		List<int> numbers = new List<int>() { 1, 2, 3, 4, 5, 6, 7, 8, 9 };
		ShuffleNumbers(numbers);

		foreach (int num in numbers)
		{
			if (CheckNumber(row, col, num, ref aGrid) == true)
			{
				aGrid[row, col] = num;
				if (CreateAnswerGrid(nextRow, nextCol, ref aGrid) == true)
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
	bool CheckNumber(int row, int col, int num, ref int[,] grid)
	{
		for (int i = 0; i < Cell_Number; i++)
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
	void ShuffleNumbers<T>(List<T> list)
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
	void CreateQuestionGrid()
	{
		List<Vector2Int> cells = new List<Vector2Int>();
		for (int r = 0; r < Cell_Number; r++)
		{
			for (int c = 0; c < Cell_Number; c++)
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
	int CountSolutions(int[,] board)
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
	bool CheckSolutions(int row, int col, int[,] board, ref int count)
	{
		if (count >= 2)
		{
			//解が2個以上見つかったら打ち切り
			return false;
		}

		//横行が最後まで来たら
		//全てのマスが埋まったら中身を実行する
		if (row == Cell_Number)
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
		int nextRow = (col == Cell_Number - 1) ? row + 1 : row;
		// 次の縦列は、現在の縦列が最後の縦列なら0に戻り、そうでなければ+1
		int nextCol = (col + 1) % Cell_Number;

		//既に数字があるならスキップ
		if (board[row, col] != 0)
		{
			return CheckSolutions(nextRow, nextCol, board, ref count);
		}

		//候補を1～9まで順に試す
		for (int num = 1; num <= Cell_Number; num++)
		{
			if (CheckNumber(row, col, num, ref board) == true)
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
	void DebugGrid(ref int[,] grid)
	{
		string s = "";
		for (int r = 0; r < Cell_Number; r++)
		{
			s = s + "|";
			for (int c = 0; c < Cell_Number; c++)
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

	/// <summary>
	/// 各マスのプレハブ
	/// </summary>
	[SerializeField] GameObject buttonPrefab;
	/// <summary>
	/// 
	/// </summary>
	[SerializeField] Transform parrentTransform;
	/// <summary>
	/// メインゲーム
	/// </summary>
	[SerializeField] MainGame mainGame;

	private void CreateCell()
	{
		CellButton[,] cells = new CellButton[Cell_Number, Cell_Number];

		for (int r = 0; r < Cell_Number; r++)
		{
			for (int c = 0; c < Cell_Number; c++)
			{
				GameObject newButton = Instantiate(buttonPrefab);
				newButton.transform.SetParent(parrentTransform, false);
				CellButton cellButton = newButton.GetComponent<CellButton>();
				//cellButton.Initialize(r, c, answerGrid[r, c], questionGrid[r, c], mainGame.IMainGameLogic.ISelectCell, mainGame.IMainGameLogic.IJudge);
				cells[r, c] = cellButton;
			}
		}

		//mainGame.IMainGameLogic.IRegister.IRegisterCells(cells);
	}

	/// <summary>
	/// 2次元配列をログ出力する
	/// </summary>
	/// <param name="grid"></param>
	public static void PrintGrid(int[,] grid)
	{
		int rows = grid.GetLength(0);
		int cols = grid.GetLength(1);

		string output = "";
		for (int r = 0; r < rows; r++)
		{
			for (int c = 0; c < cols; c++)
			{
				output += grid[r, c] + " ";
			}
			output += "\n"; // 行ごとに改行
		}

		Debug.Log(output);
	}
}

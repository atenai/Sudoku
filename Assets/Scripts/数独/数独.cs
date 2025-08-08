using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class 数独 : MonoBehaviour
{
	int[,] questionGrid = new int[9, 9];
	int[,] answerGrid = new int[9, 9];
	//空白の数を設定する
	int emptyMathCount = 35;

	//難易度
	enum Difficulties
	{
		DEBUG,
		EASY,
		MEDIUM,
		HARD,
		INSANE,
	}
	[SerializeField] Difficulties difficulty;

	[SerializeField] バックトラック法 backtrackMethod;

	void Start()
	{
		CreateBaseGrid(ref answerGrid);
		CreateSolveGrid(ref answerGrid);
		CreateRiddleGrid(ref answerGrid, ref questionGrid);

		backtrackMethod.StartBacktracking(questionGrid);
	}

	void CreateBaseGrid(ref int[,] grid)
	{
		List<int> rowValues = new List<int>() { 1, 2, 3, 4, 5, 6, 7, 8, 9 };
		List<int> colValues = new List<int>() { 1, 2, 3, 4, 5, 6, 7, 8, 9 };

		int value = rowValues[Random.Range(0, rowValues.Count)];
		grid[0, 0] = value;
		rowValues.Remove(value);
		colValues.Remove(value);

		//1行目の値をランダムに設定
		for (int i = 1; i < 9; i++)
		{
			value = rowValues[Random.Range(0, rowValues.Count)];
			grid[i, 0] = value;
			rowValues.Remove(value);
		}

		for (int i = 1; i < 9; i++)
		{
			value = colValues[Random.Range(0, colValues.Count)];

			if (i < 3)
			{
				while (BlockContainsNumber(0, 0, value, ref grid))
				{
					value = colValues[Random.Range(0, colValues.Count)];
				}
			}
			grid[0, i] = value;
			colValues.Remove(value);
		}

		DebugGrid(ref grid);
	}

	bool BlockContainsNumber(int x, int y, int value, ref int[,] grid)
	{
		for (int i = 0; i < 3; i++)
		{
			for (int j = 0; j < 3; j++)
			{
				if (grid[x - (x % 3) + i, y - (y % 3) + j] == value)
				{
					return true;
				}
			}
		}

		return false;
	}

	bool CreateSolveGrid(ref int[,] grid)
	{
		DebugGrid(ref grid);

		if (IsValidGrid(ref grid) == true)
		{
			return true;
		}

		//FIND FIRST FREECELL
		int x = 0;
		int y = 0;

		for (int i = 0; i < 9; i++)
		{
			for (int j = 0; j < 9; j++)
			{
				if (grid[i, j] == 0)
				{
					x = i;
					y = j;
					//Debug.Log(x + "," + y);//マスの位置
					break;
				}
			}
		}
		List<int> possibilities = new List<int>();
		possibilities = GetAllPossibilities(x, y, ref grid);
		for (int p = 0; p < possibilities.Count; p++)
		{
			//SET A POSSIBLE VALUE
			grid[x, y] = possibilities[p];
			//BACK TRACK
			if (CreateSolveGrid(ref grid))
			{
				return true; //もし解けたらtrueを返す
			}

			grid[x, y] = 0; //もし解けなかったら、0に戻す
		}

		return false;
	}

	bool IsValidGrid(ref int[,] grid)
	{
		for (int i = 0; i < 9; i++)
		{
			for (int j = 0; j < 9; j++)
			{
				if (grid[i, j] == 0)
				{
					return false;
				}
			}
		}
		return true;
	}

	List<int> GetAllPossibilities(int x, int y, ref int[,] grid)
	{
		List<int> possibilities = new List<int>();
		for (int val = 1; val <= 9; val++)
		{
			if (CheckAll(x, y, val, ref grid))
			{
				possibilities.Add(val);
			}
		}
		return possibilities;
	}

	bool CheckAll(int x, int y, int value, ref int[,] grid)
	{
		if (ColumnContainsNumber(y, value, ref grid))
		{
			return false;
		}

		if (RowContainsNumber(x, value, ref grid))
		{
			return false;
		}

		if (BlockContainsNumber(x, y, value, ref grid))
		{
			return false;
		}

		return true;
	}

	//Columnは横
	bool ColumnContainsNumber(int y, int value, ref int[,] grid)
	{
		for (int x = 0; x < 9; x++)
		{
			if (grid[x, y] == value)
			{
				return true; //もし列に同じ値があればtrueを返す
			}
		}

		return false;
	}
	//Rowは縦
	bool RowContainsNumber(int x, int value, ref int[,] grid)
	{
		for (int y = 0; y < 9; y++)
		{
			if (grid[x, y] == value)
			{
				return true; //もし列に同じ値があればtrueを返す
			}
		}

		return false;
	}

	//NEW GAME PLAY
	/// <summary>
	/// ➃ここで数独の解を元に、難易度に応じて数独の問題を生成します。
	/// 「完成された数独の解答」から、指定数だけランダムにマスを消して「問題」を作り、その内容をデバッグ表示するメソッドです。
	/// </summary>
	void CreateRiddleGrid(ref int[,] aGrid, ref int[,] qGrid)
	{
		//解答グリッドのコピー
		//まず、solvedGrid（完成された数独の解答）をriddleGrid（問題用グリッド）にコピーします。
		//これでriddleGridは一旦「完成された状態」になります。
		System.Array.Copy(aGrid, qGrid, aGrid.Length);


		//難易度設定
		SetDifficulty();

		//マスを消して問題を作る
		//emptyMathCount回だけ、ランダムな位置を選び、そのマスがまだ消されていなければ（0でなければ）、そのマスを0（空欄）にします。
		//これで「空欄のある数独の問題」ができます。
		for (int i = 0; i < emptyMathCount; i++)
		{
			int x1 = Random.Range(0, 9);
			int y1 = Random.Range(0, 9);
			//REROLL UNTIL WE FIND ONE WITHOUT A 0
			while (qGrid[x1, y1] == 0)
			{
				x1 = Random.Range(0, 9);
				y1 = Random.Range(0, 9);
			}
			//ONCE WE FOUND ONE WITH NO 0
			qGrid[x1, y1] = 0;
		}

		//空白部分を0にした全てのマスの情報をデバッグログに表示します。
		DebugGrid(ref questionGrid);
	}

	/// <summary>
	/// デバッグログにマス情報（グリッド）を出力します。
	/// </summary>
	/// <param name="grid"></param>
	void DebugGrid(ref int[,] grid)
	{
		string s = "";
		int sep = 0;

		//横セル
		for (int i = 0; i < 9; i++)
		{
			s += "|";
			//縦セル
			for (int j = 0; j < 9; j++)
			{
				//各セルの値を文字列に変換して追加
				s += grid[i, j].ToString();
				sep = j % 3;
				if (sep == 2)
				{
					s += "|"; //3つごとに区切り線を追加
				}
			}
			//改行を追加
			s += "\n";
		}
		//デバッグログに出力
		Debug.Log(s);
	}

	/// <summary>
	/// 難易度に応じて空白の数を設定します。
	/// </summary>
	void SetDifficulty()
	{
		switch (difficulty)
		{
			case Difficulties.DEBUG:
				emptyMathCount = 5; //デバッグ
				break;
			case Difficulties.EASY:
				emptyMathCount = 35; //簡単な数独
				break;
			case Difficulties.MEDIUM:
				emptyMathCount = 40; //中程度の数独
				break;
			case Difficulties.HARD:
				emptyMathCount = 45; //難しい数独
				break;
			case Difficulties.INSANE:
				emptyMathCount = 55; //非常に難しい数独
				break;
		}
	}
}

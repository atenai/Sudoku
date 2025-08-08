using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class 数独バックトラック法 : MonoBehaviour
{
	int[,] answerGrid = new int[9, 9];
	int[,] questionGrid = new int[9, 9];

	//空白の数を設定する
	int emptyCellCount = 35;

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

	void Start()
	{
		CreateBaseGrid(ref answerGrid);
		CreateAnswerGrid(ref answerGrid);
		CreateQuestionGrid(ref answerGrid, ref questionGrid);

		while (StartBacktracking(questionGrid) != 1)
		{
			Debug.Log("<color=red>問題を再生成します。</color>");
			CreateQuestionGrid(ref answerGrid, ref questionGrid);
		}

		Debug.Log("<color=blue>終了</color>");
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

	bool CreateAnswerGrid(ref int[,] grid)
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
			if (CreateAnswerGrid(ref grid))
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

	//Columnは縦
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

	//Rowは横
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

	/// <summary>
	/// ➃ここで数独の解を元に、難易度に応じて数独の問題を生成します。
	/// 「完成された数独の解答」から、指定数だけランダムにマスを消して「問題」を作り、その内容をデバッグ表示するメソッドです。
	/// </summary>
	void CreateQuestionGrid(ref int[,] aGrid, ref int[,] qGrid)
	{
		//解答グリッドのコピー
		//まず、solvedGrid（完成された数独の解答）をriddleGrid（問題用グリッド）にコピーします。
		//これでriddleGridは一旦「完成された状態」になります。
		System.Array.Copy(aGrid, qGrid, aGrid.Length);


		//難易度設定
		SetDifficulty();

		//マスを消して問題を作る
		//emptyCellCount回だけ、ランダムな位置を選び、そのマスがまだ消されていなければ（0でなければ）、そのマスを0（空欄）にします。
		//これで「空欄のある数独の問題」ができます。
		for (int i = 0; i < emptyCellCount; i++)
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
				emptyCellCount = 5; //デバッグ
				break;
			case Difficulties.EASY:
				emptyCellCount = 35; //簡単な数独
				break;
			case Difficulties.MEDIUM:
				emptyCellCount = 40; //中程度の数独
				break;
			case Difficulties.HARD:
				emptyCellCount = 45; //難しい数独
				break;
			case Difficulties.INSANE:
				emptyCellCount = 55; //非常に難しい数独
				break;
		}
	}

	/* ボードの幅・高さ・数字の最大値 */
	const int NUM = 9;

	/* ブロックの幅・高さ */
	const int BLOCK = 3;

	/* 見つかった答えの個数 */
	int answer = 0;

	int[,] copyBoard = new int[NUM, NUM];

	public int StartBacktracking(int[,] originalBoard)
	{
		copyBoard = originalBoard;
		answer = 0;

		for (int n = 1; n <= NUM; n++)
		{
			/* (0, 0)座標に数字nを入れてゲーム開始 */
			PutNumber(0, 0, n);
		}

		Debug.Log("回答数：" + answer);

		return answer;
	}

	bool PutNumber(int i, int j, int number)
	{
		bool fix_flag = false;

		/* 最初から(i, j)座標にnumberが入っているかを確認 */
		if (copyBoard[j, i] != number)
		{
			/* 入っているのがnumber以外の場合 */

			/* number以外の数字が入っているかを確認 */
			if (copyBoard[j, i] != 0)
			{
				/* 異なる数字が入っている場合は入れられない */
				return false;
			}

			/* (i, j)座標にnumberが入れたパターンが解になり得るかを確認 */
			if (!CheckNumber(i, j, number))
			{
				/* 解になり得ない場合はこのパターンを調べても無駄 */
				return false;
			}

			/* (i, j)座標にnumberを入れる */
			copyBoard[j, i] = number;
		}
		else
		{
			/* 最初から入ってた場合はフラグを立てておく */
			fix_flag = true;
		}

		/* 全マスに数字を入れたかを確認 */
		if (i == NUM - 1 && j == NUM - 1)
		{

			/* 解の数をカウントアップ */
			answer++;

			/* 結果を表示 */
			ShowBoard();
		}
		else
		{
			/* まだ入れていないマスがある場合 */

			int n;
			int next_i, next_j;

			/* 次の行に移るかを確認 */
			if (i + 1 >= NUM)
			{
				/* 次に数字を入れる場所を次の行に設定 */
				next_i = 0;
				next_j = j + 1;
			}
			else
			{
				/* 今の行のまま次に数字を入れる場所を設定 */
				next_i = i + 1;
				next_j = j;
			}

			/* 次のマスに数字を入れてみる */
			for (n = 1; n <= NUM; n++)
			{
				PutNumber(next_i, next_j, n);
			}
		}

		/* numberが最初から入れられていたかを確認 */
		if (!fix_flag)
		{
			/* 入れた数字を取り除く */
			copyBoard[j, i] = 0;
		}

		return true;
	}

	bool CheckNumber(int i, int j, int number)
	{
		int bi, bj;

		/* 第j行に同じ数字があるかどうかを判断 */
		for (int x = 0; x < NUM; x++)
		{
			if (copyBoard[j, x] == number)
			{
				/* あった場合は入れられない */
				return false;
			}
		}

		/* 第i行に同じ数字があるかどうかを判断 */
		for (int y = 0; y < NUM; y++)
		{
			if (copyBoard[y, i] == number)
			{
				/* あった場合は入れられない */
				return false;
			}
		}

		/* 同じブロック内に同じ数字があるかどうかを判断 */

		/* 同じブロックの先頭座標を計算 */
		bi = i / BLOCK * BLOCK;
		bj = j / BLOCK * BLOCK;

		for (int y = 0; y < BLOCK; y++)
		{
			for (int x = 0; x < BLOCK; x++)
			{
				if (copyBoard[bj + y, bi + x] == number)
				{
					/* あった場合は入れられない */
					return false;
				}
			}
		}

		/* 同じ行・同じ列・同じグループに同じ数字がない場合 */
		return true;
	}

	void ShowBoard()
	{
		Debug.Log(answer + "個目の解答です");

		string s = "";

		for (int j = 0; j < NUM; j++)
		{
			s += "|";
			for (int i = 0; i < NUM; i++)
			{
				s += copyBoard[j, i].ToString();
			}
			s += "|\n";
		}
		Debug.Log(s);
	}
}

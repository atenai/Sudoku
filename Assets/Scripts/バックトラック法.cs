using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class バックトラック法 : MonoBehaviour
{
	/* ボードの幅・高さ・数字の最大値 */
	const int NUM = 9;

	/* ブロックの幅・高さ */
	const int BLOCK = 3;

	/* ボードの初期状態 */
	// int[,] originalBoard = new int[NUM, NUM]
	// {
	// 	{5, 3, 0, 0, 7, 0, 0, 0, 0},
	// 	{6, 0, 0, 1, 9, 5, 0, 0, 0},
	// 	{0, 9, 8, 0, 0, 0, 0, 6, 0},
	// 	{8, 0, 0, 0, 6, 0, 0, 0, 3},
	// 	{4, 0, 0, 8, 0, 3, 0, 0, 1},
	// 	{7, 0, 0, 0, 2, 0, 0, 0, 6},
	// 	{0, 6, 0, 0, 0, 0, 2, 8, 0},
	// 	{0, 0, 0, 4, 1, 9, 0, 0, 5},
	// 	{0, 0, 0, 0, 8, 0, 0, 7, 9}
	// };

	int[,] originalBoard = new int[NUM, NUM]
	{
		{0,2,0,0,7,5,0,1,0},
		{1,0,0,0,0,4,5,0,8},
		{0,5,6,8,0,0,2,0,0},
		{8,1,0,0,0,0,7,0,0},
		{9,0,0,0,0,0,0,0,3},
		{0,0,2,0,0,0,0,4,5},
		{0,0,9,0,0,1,4,3,0},
		{3,0,1,7,0,0,0,0,9},
		{0,7,0,3,9,0,0,2,0},
	};

	/* 見つかった答えの個数 */
	int answer = 0;

	int[,] copyBoard = new int[NUM, NUM];

	void Start()
	{
		//StartBacktracking(originalBoard);
	}

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

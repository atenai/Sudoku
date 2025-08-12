using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GenerateGrid
{
	/// <summary>
	/// 区切りブロック
	/// </summary>
	private const int Separator_Block = 3;

	/// <summary>
	/// 難易度に応じた空白数
	/// </summary>
	private int emptyCell = 55;

	/// <summary>
	/// コンストラクタ
	/// </summary>
	/// <param name="aGrid"></param>
	/// <param name="qGrid"></param>
	/// <param name="difficultyType"></param>
	public GenerateGrid(int[,] aGrid, int[,] qGrid, MainGameSetting.DifficultyType difficultyType)
	{
		// 0. 難易度設定
		SetEmptyCell(difficultyType);

		// 1. 完全な数独を生成
		CreateAnswerGrid(0, 0, aGrid);
		Debug.Log("<color=red>答えを生成しました！</color>");
		DebugGrid(aGrid);

		// 2. 完全解をコピーして問題用にする
		System.Array.Copy(aGrid, qGrid, aGrid.Length);

		// 3. マスを1つずつ消して唯一解を保つ
		CreateQuestionGrid(qGrid);
		Debug.Log("<color=blue>問題を生成しました！</color>");
		DebugGrid(qGrid);
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
	private void CreateQuestionGrid(int[,] qGrid)
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

			int backup = qGrid[cell.x, cell.y];
			qGrid[cell.x, cell.y] = 0;

			int solutions = CountSolutions((int[,])qGrid.Clone());
			if (solutions == 1)
			{
				removed++;
			}
			else
			{
				// 一意解でない → 元に戻す
				qGrid[cell.x, cell.y] = backup;
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
	/// <remarks>
	/// この関数の全体の流れ
	/// 1.空いているマスを 左上から右下へ 順にたどる（nextRow / nextCol）。
	/// 2.各マスで 1〜9 を順番に試す。
	/// 3.置ける数字なら 仮に置く → 次のマスへ再帰。
	/// 4.どこかで行き詰まったら 元に戻して（=0にして） 別の数字を試す（これが“バックトラック”）。
	/// 5.盤面の最後まで埋まったら 解を1つ発見 → count++。
	/// 6.count >= 2 になったら「複数解」確定で 探索打ち切り。
	/// </remarks>
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

		//「すでに数字が入っているマスは飛ばして、次のマスの探索に進む」
		//board[row, col]が0なら「空白マス」という意味。
		//このマスに1〜9を順番に試す処理に入ります（バックトラック探索）。
		//board[row, col]が0でないなら、
		//そのマスは、
		//・元々の問題（クエスチョン）で固定された数字
		//・または すでに探索中に数字を仮置きした状態
		//のどちらかです。
		//こういうマスは「これ以上数字を変えられない」ので、
		//数字を試す処理はスキップして、次の座標（nextRow, nextCol）へ再帰します。
		if (board[row, col] != 0)
		{
			return CheckSolutions(nextRow, nextCol, board, ref count);
		}

		//1.マス (row, col) に 1 を試す
		//2.置けたら次のマスへ再帰
		//3.行き詰まったら戻って 2 を試す
		//4.最後まで行けたら解を1つ発見
		//5.他の解も探すために戻って別の数字を試す

		//候補を1～9まで順に試す
		//MainGame.Cell_Number は 9 なので、num は 1～9 まで順番に試されます。
		//つまり「このマスに 1 を入れてみる → ダメなら 2 → … → 9」という流れです。
		for (int num = 1; num <= MainGame.Cell_Number; num++)
		{
			//CheckNumber() は、その数字 num が行・列・3×3ブロックのルールに反していないかを確認します。
			//true なら「このマスに num を置いてもOK」。
			if (CheckNumber(row, col, num, board) == true)
			{
				//この「置く→進む→戻す」を機械的に繰り返すのがバックトラッキングです。

				//実際に盤面 board に num を入れます。
				//この時点ではあくまで仮置きで、次のマスを埋められるか試すための一時的な状態です。
				//① 仮置き：その場でやってみる
				board[row, col] = num;
				//再帰呼び出しで次のマスを解こうとする。
				//count は解の個数カウント用。ref なので呼び出し先で加算されると呼び出し元にも反映されます。
				//もしここで解が完成したら count が増える。
				//② 再帰：次のマスがちゃんと埋め切れるか検証
				CheckSolutions(nextRow, nextCol, board, ref count);
				//「この数字でやってみたけど解が確定しなかった」または「他の解を探したい」場合は、そのマスを空（0）に戻す。
				//これにより他の数字（num+1 など）を試せる。
				//③ 戻す：ダメだった時や、他の解も探すために“状態を元に戻す”
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

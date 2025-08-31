using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

/// <summary>
/// ボードクラス
/// </summary>
public class Board : MonoBehaviour
{
	/// <summary>
	/// ボードのトランスフォーム
	/// </summary>
	[SerializeField] private Transform boardTransform;

	/// <summary>
	/// 各マスのプレハブ
	/// </summary>
	[SerializeField] private GameObject cellButtonPrefab;

	/// <summary>
	/// セルボタンの配列
	/// </summary>
	private CellButton[,] cells = new CellButton[MainGame.Cell_Number, MainGame.Cell_Number];

	/// <summary>
	/// マスを作成
	/// </summary>
	/// <param name="aGrid">答えグリッド</param>
	/// <param name="qGrid">問題グリッド</param>
	public void CreateCell(int[,] aGrid, int[,] qGrid, UnityAction<int, int> unityAction)
	{
		for (int r = 0; r < MainGame.Cell_Number; r++)
		{
			for (int c = 0; c < MainGame.Cell_Number; c++)
			{
				GameObject newButton = Instantiate(cellButtonPrefab);
				newButton.transform.SetParent(boardTransform, false);
				CellButton cellButton = newButton.GetComponent<CellButton>();
				cellButton.Initialize(r, c, aGrid[r, c], qGrid[r, c], unityAction);
				cells[r, c] = cellButton;
			}
		}
	}

	/// <summary>
	/// セルに数字を表示する
	/// </summary>
	/// <param name="row"></param>
	/// <param name="col"></param>
	/// <param name="value"></param>
	/// <param name="isCorrect"></param>
	public void ShowNumber(int row, int col, int value, bool isCorrect)
	{
		if (cells == null) { return; }
		CellButton cellButton = cells[row, col];
		if (cellButton == null) { return; }

		cellButton.SetNumber(value);
		if (isCorrect)
		{
			//正解時の表示処理
			cellButton.SetLockCell();
			cellButton.SetColor(Color.green);
		}
		else
		{
			//不正解時の表示処理
			cellButton.SetColor(Color.red);
		}
	}
}

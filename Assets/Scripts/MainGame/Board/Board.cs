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

	private Transform[,] blocks = new Transform[MainGame.Separator_Block, MainGame.Separator_Block];

	[SerializeField] private Transform[] block;

	/// <summary>
	/// マスを作成
	/// </summary>
	/// <param name="aGrid">答えグリッド</param>
	/// <param name="qGrid">問題グリッド</param>
	public void CreateCell(int[,] aGrid, int[,] qGrid, UnityAction<int, int> unityAction)
	{
		blocks[0, 0] = block[0];
		blocks[0, 1] = block[1];
		blocks[0, 2] = block[2];
		blocks[1, 0] = block[3];
		blocks[1, 1] = block[4];
		blocks[1, 2] = block[5];
		blocks[2, 0] = block[6];
		blocks[2, 1] = block[7];
		blocks[2, 2] = block[8];

		for (int r = 0; r < MainGame.Cell_Number; r++)
		{
			for (int c = 0; c < MainGame.Cell_Number; c++)
			{
				//どの親ブロックに所属するか
				int blockRow = r / MainGame.Separator_Block;
				int blockCol = c / MainGame.Separator_Block;
				Transform parentBlock = blocks[blockRow, blockCol];

				//マスを生成
				GameObject newButton = Instantiate(cellButtonPrefab);
				newButton.transform.SetParent(parentBlock, false);
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
	/// <param name="inputNumber">入力番号</param>
	/// <param name="isCorrect"></param>
	public void ShowNumber(int row, int col, int inputNumber, bool isCorrect)
	{
		if (cells == null) { return; }
		CellButton cellButton = cells[row, col];
		if (cellButton == null) { return; }

		cellButton.SetNumber(inputNumber);
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

	/// <summary>
	/// セルの数字をクリア
	/// </summary>
	/// <param name="row"></param>
	/// <param name="col"></param>
	public void ClearNumber(int row, int col)
	{
		if (cells == null) { return; }
		CellButton cellButton = cells[row, col];
		if (cellButton == null) { return; }

		cellButton.SetNumber(0);
		cellButton.SetColor(Color.white);
	}

	/// <summary>
	/// メモをトグル
	/// </summary>
	/// <param name="row"></param>
	/// <param name="col"></param>
	/// <param name="number"></param>
	public void ToggleMemo(int row, int col, int number)
	{
		if (cells == null) { return; }
		CellButton cellButton = cells[row, col];
		if (cellButton == null) { return; }

		cellButton.SetMemoNumber(number);
		cellButton.SetColor(Color.white);
	}

	/// <summary>
	/// 指定セルをハイライト（前回は解除）
	/// </summary>
	public void SetSelectedHighlight(int currentRow, int currentCol, int oldRow, int oldCol)
	{
		if (cells == null) { return; }

		//前回の選択を解除
		if (0 <= oldRow && 0 <= oldCol)
		{
			cells[oldRow, oldCol]?.SetHighlight(false);
		}

		//今回の選択をハイライト
		cells[currentRow, currentCol]?.SetHighlight(true);
	}
}

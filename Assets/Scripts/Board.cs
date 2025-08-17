using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// ボードクラス
/// </summary>
public class Board : MonoBehaviour
{
	/// <summary>
	/// ボードのトランスフォーム
	/// </summary>
	[SerializeField] private Transform BoardTransform;
	/// <summary>
	/// 各マスのプレハブ
	/// </summary>
	[SerializeField] private GameObject cellButtonPrefab;
	/// <summary>
	/// メインゲーム
	/// </summary>
	[SerializeField] private MainGame mainGame;

	/// <summary>
	/// マスを作成
	/// </summary>
	/// <param name="aGrid">答えグリッド</param>
	/// <param name="qGrid">問題グリッド</param>
	public void CreateCell(IGridData gridData)
	{
		ICellButton[,] cells = new ICellButton[MainGame.Cell_Number, MainGame.Cell_Number];

		for (int r = 0; r < MainGame.Cell_Number; r++)
		{
			for (int c = 0; c < MainGame.Cell_Number; c++)
			{
				GameObject newButton = Instantiate(cellButtonPrefab);
				newButton.transform.SetParent(BoardTransform, false);
				CellButton cellButton = newButton.GetComponent<CellButton>();
				cellButton.Initialize(r, c, gridData.GetAnswerGridNumber(r, c), gridData.GetQuestionGridNumber(r, c), mainGame);
				cells[r, c] = cellButton;
			}
		}

		// 全てのマスを登録
		mainGame.MainGameLogic.IJudge.RegisterCells(cells);
	}
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// ボードクラス
/// </summary>
public class Board : MonoBehaviour, IBoard
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
	/// メインゲームインプット
	/// </summary>
	private IMainGameInput mainGameInput;

	private IJudge judge;

	/// <summary>
	/// 初期化処理
	/// </summary>
	public void Initialize(IMainGameInput mainGameInput, IJudge judge)
	{
		this.mainGameInput = mainGameInput;
		this.judge = judge;
	}

	/// <summary>
	/// マスを作成
	/// </summary>
	/// <param name="aGrid">答えグリッド</param>
	/// <param name="qGrid">問題グリッド</param>
	public void ICreateCell(IGridData gridData)
	{
		ICellButton[,] cells = new ICellButton[MainGame.Cell_Number, MainGame.Cell_Number];

		for (int r = 0; r < MainGame.Cell_Number; r++)
		{
			for (int c = 0; c < MainGame.Cell_Number; c++)
			{
				GameObject newButton = Instantiate(cellButtonPrefab);
				newButton.transform.SetParent(boardTransform, false);
				CellButton cellButton = newButton.GetComponent<CellButton>();
				cellButton.Initialize(r, c, gridData.IGetAnswerGridNumber(r, c), gridData.IGetQuestionGridNumber(r, c), mainGameInput, judge);
				cells[r, c] = cellButton;
			}
		}

		// 全てのマスを登録
		judge.IRegisterCells(cells);
	}
}

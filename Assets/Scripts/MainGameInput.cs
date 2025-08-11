using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainGameInput
{
	/// <summary>
	/// 現在選択しているセル
	/// </summary>
	private CellButton selectedCurrentCell;

	MainGame mainGame;

	/// <summary>
	/// コンストラクタ
	/// </summary>
	/// <param name="mainGame"></param>
	public MainGameInput(MainGame mainGame)
	{
		this.mainGame = mainGame;
	}

	/// <summary>
	/// セルを選択
	/// </summary>
	/// <param name="cell"></param>
	public void SelectCell(CellButton cell)
	{
		// 以前のセルのハイライトを解除
		if (selectedCurrentCell != null && selectedCurrentCell != cell)
		{
			selectedCurrentCell.Highlight(false);
		}

		// 新しく選んだセルを選択状態に
		selectedCurrentCell = cell;
		//cell.SetColor(Color.blue);
		selectedCurrentCell.Highlight(true); // ✅ 選択セルをハイライト
		Debug.Log($"ボタン (縦:{selectedCurrentCell.Row}, 横:{selectedCurrentCell.Col}) がクリックされました!");
		Debug.Log($"答え番号: {selectedCurrentCell.AnswerNumber}");
		Debug.Log($"問題番号: {selectedCurrentCell.QuestionNumber}");
	}

	//3
	/// <summary>
	/// 数値を入力
	/// </summary>
	/// <param name="number"></param>
	public void InputNumber(int number)
	{
		if (selectedCurrentCell != null)
		{
			if (mainGame.MemoMode && number != 0)
			{
				//入力後ハイライト解除
				selectedCurrentCell.Highlight(false);
				//メモを切り替え
				selectedCurrentCell.ToggleMemo(number);
			}
			else
			{
				//入力後ハイライト解除
				selectedCurrentCell.Highlight(false);
				//4
				//本数字入力
				selectedCurrentCell.SetNumber(number);
			}

			selectedCurrentCell = null;
		}
	}

	/// <summary>
	/// メモモードの切り替え
	/// </summary>
	public void ToggleMemoMode()
	{
		mainGame.MemoMode = !mainGame.MemoMode;
		Debug.Log("メモモード: " + (mainGame.MemoMode ? "ON" : "OFF"));
	}
}

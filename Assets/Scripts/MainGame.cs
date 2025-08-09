using UnityEngine;

public class MainGame : MonoBehaviour
{
	/// <summary>
	/// メモモード切替
	/// </summary>
	public bool memoMode = false;

	/// <summary>
	/// 生成された全てのセルを管理
	/// </summary>
	private CellButton[,] allCells;

	/// <summary>
	/// ミス数
	/// </summary>
	private int missNumber = 5;

	/// <summary>
	/// ミスカウント
	/// </summary>
	private int missCount = 0;

	/// <summary>
	/// マス数
	/// </summary>
	public static readonly int Cell_Number = 9;

	/// <summary>
	/// ボード
	/// </summary>
	[SerializeField] private Board board;

	MainGameLogic mainGameLogic;
	MainGameInput mainGameInput;

	public MainGameInput MainGameInput => mainGameInput;

	private void Start()
	{
		if (GameManager.SingletonInstance.GetSetting() is MainGameSetting mainGameSetting)
		{
			mainGameLogic = new MainGameLogic(mainGameSetting.Difficulty);
			mainGameInput = new MainGameInput(this);
			board.CreateCell(mainGameLogic.AnswerGrid, mainGameLogic.QuestionGrid);
		}
		else
		{
			Debug.LogError("メインゲームに必要な値を取得できませんでした。");
		}
	}

	/// <summary>
	/// マスを登録
	/// </summary>
	/// <param name="cells"></param>
	public void RegisterCells(CellButton[,] cells)
	{
		allCells = cells;
	}

	//7
	/// <summary>
	/// 正誤判定処理
	/// </summary>
	/// <param name="cell"></param>
	/// <param name="number"></param>
	public void CheckAnswer(CellButton cell, int number)
	{
		if (memoMode) return; // メモ入力時は判定しない
		if (number == 0) return; // 入力を消した場合は判定しない

		if (cell.AnswerNumber == number)
		{
			//Debug.Log($"({cell.Row},{cell.Col}) 正解！");
			Debug.Log("<color=green>正解！</color>");
			cell.SetColor(Color.green);
			cell.LockCell(); // 正解したらそのセルをロック

			// クリア判定
			if (CheckAllCellLock() == true)
			{
				Debug.Log("<color=yellow>ゲームクリア！</color>");
			}
		}
		else
		{
			//Debug.Log($"({cell.Row},{cell.Col}) 不正解！");
			Debug.Log("<color=red>不正解！</color>");
			cell.SetColor(Color.red);
			missCount++;
			if (missNumber <= missCount)
			{
				Debug.Log("<color=red>ゲームオーバー！</color>");
			}
		}
	}

	/// <summary>
	/// 全セルがロックされているかチェック
	/// </summary>
	/// <returns>全てのマスがロックされていたらtrue 1つでもロックされていない場合はfalse</returns>
	private bool CheckAllCellLock()
	{
		foreach (var cell in allCells)
		{
			if (cell.GetComponent<UnityEngine.UI.Button>().interactable)
			{
				return false;
			}
		}
		return true;
	}
}

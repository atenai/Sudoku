using UnityEngine;

public class MainGame : MonoBehaviour
{
	/// <summary>
	/// マス数
	/// </summary>
	public static readonly int Cell_Number = 9;

	/// <summary>
	/// ボード
	/// </summary>
	[SerializeField] private Board board;

	private MainGameLogic mainGameLogic;
	public MainGameLogic MainGameLogic => mainGameLogic;

	/// <summary>
	/// メモモード切替
	/// </summary>
	private bool memoMode = false;
	public bool MemoMode
	{
		get { return memoMode; }
		set { memoMode = value; }
	}

	private void Start()
	{
		if (GameManager.SingletonInstance.GetSetting() is MainGameSetting mainGameSetting)
		{
			mainGameLogic = new MainGameLogic(this, mainGameSetting.Difficulty);
			board.CreateCell(mainGameLogic.AnswerGrid, mainGameLogic.QuestionGrid);
		}
		else
		{
			Debug.LogError("メインゲームに必要な値を取得できませんでした。");
		}
	}
}

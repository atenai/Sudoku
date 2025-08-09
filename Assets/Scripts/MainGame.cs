using UnityEngine;

public class MainGame : MonoBehaviour
{
	/// <summary>
	/// メモモード切替
	/// </summary>
	public bool memoMode = false;

	/// <summary>
	/// マス数
	/// </summary>
	public static readonly int Cell_Number = 9;

	/// <summary>
	/// ボード
	/// </summary>
	[SerializeField] private Board board;

	MainGameInput mainGameInput;
	Judge judge;

	public MainGameInput MainGameInput => mainGameInput;
	public Judge Judge => judge;

	private void Start()
	{
		if (GameManager.SingletonInstance.GetSetting() is MainGameSetting mainGameSetting)
		{
			MainGameLogic mainGameLogic = new MainGameLogic(mainGameSetting.Difficulty);
			mainGameInput = new MainGameInput(this);
			judge = new Judge(this);
			board.CreateCell(mainGameLogic.AnswerGrid, mainGameLogic.QuestionGrid);
		}
		else
		{
			Debug.LogError("メインゲームに必要な値を取得できませんでした。");
		}
	}
}

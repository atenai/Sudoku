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

	/// <summary>
	/// ミスUI
	/// </summary>
	[SerializeField] private MissUI missUI;
	public MissUI MissUI => missUI;

	/// <summary>
	/// メインゲームロジック
	/// </summary>
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

	/// <summary>
	/// 初期化処理
	/// </summary>
	private void Start()
	{
		if (GameManager.SingletonInstance.GetSetting() is MainGameSetting mainGameSetting)
		{
			mainGameLogic = new MainGameLogic(this, mainGameSetting.Difficulty);
			board.CreateCell(mainGameLogic.AnswerGrid, mainGameLogic.QuestionGrid);
			//missUI.SetMissNumber(mainGameLogic.Judge.MissNumber);
		}
		else
		{
			Debug.LogError("メインゲームに必要な値を取得できませんでした。");
		}
	}

	private void Update()
	{

	}
}

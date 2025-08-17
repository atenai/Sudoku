using UnityEngine;

public class MainGame : MonoBehaviour
{
	/// <summary>
	/// マス数
	/// </summary>
	public static readonly int Cell_Number = 9;

	[SerializeField] private MainGameUI mainGameUI;
	public MainGameUI MainGameUI => mainGameUI;

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
			mainGameUI.IBoard.CreateCell(mainGameLogic);

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

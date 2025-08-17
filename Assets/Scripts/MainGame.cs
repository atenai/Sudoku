using UnityEngine;

public class MainGame : MonoBehaviour, IMainGame
{
	/// <summary>
	/// マス数
	/// </summary>
	public static readonly int Cell_Number = 9;

	[SerializeField] private MainGameUI mainGameUI;
	public IMainGameUI IMainGameUI
	{
		get { return mainGameUI; }
	}

	/// <summary>
	/// メインゲームロジック
	/// </summary>
	private IMainGameLogic mainGameLogic;
	public IMainGameLogic IMainGameLogic
	{
		get { return mainGameLogic; }
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

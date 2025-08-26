using UnityEngine;

public class MainGame : MonoBehaviour
{
	/// <summary>
	/// マス数
	/// </summary>
	public static readonly int Cell_Number = 9;

	/// <summary>
	/// メインゲームUI
	/// </summary>
	[SerializeField] private MainGameUI mainGameUI;

	/// <summary>
	/// メインゲームロジック
	/// </summary>
	private MainGameLogic mainGameLogic;
	/// <summary>
	/// メインゲームロジックのプロパティ
	/// </summary>
	public MainGameLogic IMainGameLogic
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
			mainGameLogic = new MainGameLogic(mainGameUI.MissUI, mainGameSetting.Difficulty);
			mainGameUI.Board.ICreateCell(mainGameLogic, mainGameLogic.ISelectInput, mainGameLogic.IJudge, mainGameLogic.IRegister);

			foreach (var inputNumberButton in mainGameUI.InputNumberButtons)
			{
				inputNumberButton.Initialize(mainGameLogic.ISelectInput);
			}
			mainGameUI.ClearButton.Initialize(mainGameLogic.ISelectInput);
			mainGameUI.MemoButton.Initialize(mainGameLogic.IMemo);

			mainGameUI.MissUI.ISetMissCount(mainGameLogic.IJudge.GetMissCount());
			mainGameUI.MissUI.ISetFailNumber(mainGameLogic.IJudge.GetFailNumber());
		}
		else
		{
			Debug.LogError("メインゲームに必要な値を取得できませんでした。");
		}
	}
}

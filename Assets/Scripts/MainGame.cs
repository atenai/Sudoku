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
	private IMainGameLogic mainGameLogic;
	/// <summary>
	/// メインゲームロジックのプロパティ
	/// </summary>
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
			mainGameLogic = new MainGameLogic(mainGameUI.MissUI, mainGameSetting.Difficulty);
			mainGameUI.Board.ICreateCell(mainGameLogic, mainGameLogic.IMainGameInput, mainGameLogic.IJudge);

			foreach (var inputNumberButton in mainGameUI.InputNumberButtons)
			{
				inputNumberButton.Initialize(mainGameLogic.IMainGameInput);
			}
			mainGameUI.ClearButton.Initialize(mainGameLogic.IMainGameInput);
			mainGameUI.MemoButton.Initialize(mainGameLogic.IMainGameInput);
		}
		else
		{
			Debug.LogError("メインゲームに必要な値を取得できませんでした。");
		}
	}
}

using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// クリアーボタン（消しゴム）クラス
/// </summary>
public class ClearButton : MonoBehaviour
{
	/// <summary>
	/// ボタン
	/// </summary>
	[SerializeField] private Button button;

	/// <summary>
	/// メインゲーム
	/// </summary>
	[SerializeField] private MainGame mainGame;

	/// <summary>
	/// 初期化処理
	/// </summary>
	private void Start()
	{
		button.onClick.AddListener(OnClick);
	}

	/// <summary>
	/// クリアーボタン（消しゴム）を押した際の処理
	/// </summary>
	private void OnClick()
	{
		//number=0で選択セルをクリア
		mainGame.MainGameLogic.MainGameInput.InputNumber(0);
	}
}

using UnityEngine;
using UnityEngine.UI;

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

	private void Start()
	{
		button.onClick.AddListener(OnClick);
	}

	private void OnClick()
	{
		mainGame.InputNumber(0); //number=0で選択セルをクリア
	}
}

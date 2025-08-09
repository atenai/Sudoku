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
		//number=0で選択セルをクリア
		mainGame.MainGameInput.InputNumber(0);
	}
}

using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// 答え入力ボタンクラス
/// </summary>
public class InputNumberButton : MonoBehaviour
{
	[SerializeField] private Button button;
	[SerializeField] int number;
	[SerializeField] MainGame mainGame;

	/// <summary>
	/// 初期化処理
	/// </summary>
	void Start()
	{
		button.onClick.AddListener(OnClick);
	}

	//1
	/// <summary>
	/// 答え入力ボタンを押した際の処理
	/// </summary>
	private void OnClick()
	{
		//2
		mainGame.MainGameLogic.MainGameInput.InputNumber(number);
	}
}

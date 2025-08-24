using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// 答え入力ボタンクラス
/// </summary>
public class InputNumberButton : MonoBehaviour
{
	[SerializeField] private Button button;
	[SerializeField] int number;

	/// <summary>
	/// メインゲームインプット
	/// </summary>
	private IMainGameInput mainGameInput;

	/// <summary>
	/// 初期化処理
	/// </summary>
	public void Initialize(IMainGameInput mainGameInput)
	{
		this.mainGameInput = mainGameInput;
		button.onClick.AddListener(OnClick);
	}

	//1
	/// <summary>
	/// 答え入力ボタンを押した際の処理
	/// </summary>
	private void OnClick()
	{
		//2
		mainGameInput.ISetInputNumber(number);
	}
}

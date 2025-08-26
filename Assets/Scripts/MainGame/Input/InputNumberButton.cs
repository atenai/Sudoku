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
	private IInputNumber inputNumber;

	/// <summary>
	/// 初期化処理
	/// </summary>
	public void Initialize(IInputNumber inputNumber)
	{
		this.inputNumber = inputNumber;
		button.onClick.AddListener(OnClick);
	}

	//1
	/// <summary>
	/// 答え入力ボタンを押した際の処理
	/// </summary>
	private void OnClick()
	{
		//2
		inputNumber.ISetInputNumber(number);
	}
}

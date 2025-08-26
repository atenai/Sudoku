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

	/// <summary>
	/// クリアーボタン（消しゴム）を押した際の処理
	/// </summary>
	private void OnClick()
	{
		//number=0で選択セルをクリア
		inputNumber.ISetInputNumber(0);
	}
}

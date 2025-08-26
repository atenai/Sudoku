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
	private ISelectInput selectInput;

	/// <summary>
	/// 初期化処理
	/// </summary>
	public void Initialize(ISelectInput selectInput)
	{
		this.selectInput = selectInput;
		button.onClick.AddListener(OnClick);
	}

	/// <summary>
	/// クリアーボタン（消しゴム）を押した際の処理
	/// </summary>
	private void OnClick()
	{
		//number=0で選択セルをクリア
		selectInput.ISetInputNumber(0);
	}
}

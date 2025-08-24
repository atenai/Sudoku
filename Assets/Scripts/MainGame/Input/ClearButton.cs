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
	private IMainGameInput mainGameInput;

	/// <summary>
	/// 初期化処理
	/// </summary>
	public void Initialize(IMainGameInput mainGameInput)
	{
		this.mainGameInput = mainGameInput;
		button.onClick.AddListener(OnClick);
	}

	/// <summary>
	/// クリアーボタン（消しゴム）を押した際の処理
	/// </summary>
	private void OnClick()
	{
		//number=0で選択セルをクリア
		mainGameInput.ISetInputNumber(0);
	}
}

using UnityEngine;
using UnityEngine.UI;
using TMPro;

/// <summary>
/// メモボタンクラス
/// </summary>
public class MemoButton : MonoBehaviour
{
	/// <summary>
	/// ボタンの背景
	/// </summary>
	[SerializeField] private Image image;

	/// <summary>
	/// ボタン
	/// </summary>
	[SerializeField] private Button button;

	/// <summary>
	/// ボタンのテキスト
	/// </summary>
	[SerializeField] private TextMeshProUGUI text;

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
	/// メモボタンを押した際の処理
	/// </summary>
	private void OnClick()
	{
		mainGameInput.IToggleMemoMode();
		UpdateVisual();
	}

	/// <summary>
	/// メモボタンのビジュアルを更新
	/// </summary>
	private void UpdateVisual()
	{
		if (mainGameInput.GetMemoMode() == true)
		{
			if (text != null)
			{
				text.text = "Memo: ON";
			}
			if (image != null)
			{
				image.color = Color.green;
			}
		}
		else
		{
			if (text != null)
			{
				text.text = "Memo: OFF";
			}
			if (image != null)
			{
				image.color = Color.white;
			}
		}
	}
}

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
	/// メインゲーム
	/// </summary>
	[SerializeField] private MainGame mainGame;

	private void Start()
	{
		button.onClick.AddListener(OnClick);
		InitVisual();
	}

	/// <summary>
	/// メモボタンを押した際の処理
	/// </summary>
	private void OnClick()
	{
		mainGame.IMainGameLogic.IMainGameInput.IToggleMemoMode();
		UpdateVisual();
	}

	private void InitVisual()
	{
		// どれか未初期化ならデフォルト表示で早期return
		if (mainGame == null || mainGame.IMainGameLogic == null)
		{
			if (text)
			{
				text.text = "Memo: OFF";
			}
			if (image)
			{
				image.color = Color.white;
			}
			return;
		}
	}

	/// <summary>
	/// メモボタンのビジュアルを更新
	/// </summary>
	private void UpdateVisual()
	{
		if (mainGame.IMainGameLogic.IMainGameInput.IMemoMode == true)
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

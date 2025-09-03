using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;
using UnityEngine.Events;

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
	/// メモモードの取得
	/// </summary>
	Func<bool> memoMode;

	/// <summary>
	/// 初期化処理
	/// </summary>
	public void Initialize(Func<bool> memoMode, UnityAction unityAction)
	{
		this.memoMode = memoMode;
		button.onClick.AddListener(() =>
		{
			unityAction.Invoke();
			UpdateVisual();
		});
	}

	/// <summary>
	/// メモボタンのビジュアルを更新
	/// </summary>
	private void UpdateVisual()
	{
		if (memoMode.Invoke() == true)
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

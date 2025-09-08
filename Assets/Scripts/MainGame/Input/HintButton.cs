using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

/// <summary>
/// ヒントボタンクラス
/// </summary>
public class HintButton : MonoBehaviour
{
	/// <summary>
	/// ボタン
	/// </summary>
	[SerializeField] private Button button;

	/// <summary>
	/// 初期化処理
	/// </summary>
	public void Initialize(UnityAction unityAction)
	{
		button.onClick.AddListener(unityAction);
	}
}

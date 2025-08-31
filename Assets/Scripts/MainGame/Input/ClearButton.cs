using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

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
	/// 初期化処理
	/// </summary>
	public void Initialize(UnityAction unityAction)
	{
		button.onClick.AddListener(unityAction);
	}
}

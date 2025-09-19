using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

/// <summary>
/// 答え入力ボタンクラス
/// </summary>
public class InputNumberButton : MonoBehaviour
{
	[SerializeField] private Button button;

	/// <summary>
	/// 初期化処理
	/// </summary>
	public void Initialize(int number, UnityAction<int> unityAction)
	{
		button.onClick.AddListener(() => unityAction(number));
	}
}

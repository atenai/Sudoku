using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using DG.Tweening;

/// <summary>
/// ゲームクリアUIクラス
/// </summary>
/// <remarks>
/// ゲームクリアのUIを管理
/// MVPパターンのView（ビュー）を担当
/// </remarks>
public class GameClearUI : MonoBehaviour
{
	[SerializeField] TextMeshProUGUI titleText;
	[SerializeField] private Button goToTitleButton;
	[SerializeField] private TextMeshProUGUI goToTitleButtonText;

	public Button GoToTitleButton => goToTitleButton;

	/// <summary>
	/// 初期化処理
	/// </summary>
	private void Start()
	{
		goToTitleButton.transform.DOPunchScale(new Vector3(0.1f, 0.1f, 0), 0.5f, 10, 1).SetLoops(-1, LoopType.Restart).SetEase(Ease.Linear);

		titleText.DOText("Game Clear", 1, scrambleMode: ScrambleMode.All).SetEase(Ease.Linear);
		goToTitleButtonText.DOText("Go to Title", 1, scrambleMode: ScrambleMode.All).SetEase(Ease.Linear);
	}
}

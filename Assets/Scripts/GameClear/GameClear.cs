using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// ゲームクリアクラス
/// </summary>
/// <remarks>
/// ゲームクリアのやり取りを管理
/// MVPパターンのPresenter（プレゼンター）を担当
/// </remarks>
public class GameClear : MonoBehaviour
{
	[SerializeField] GameClearUI gameClearUI;

	/// <summary>
	/// 初期化処理
	/// </summary>
	private void Start()
	{
		GameClearLogic gameClearLogic = new GameClearLogic();

		gameClearUI.GoToTitleButton.onClick.AddListener(gameClearLogic.OnClickGoToTitleButton);
	}
}

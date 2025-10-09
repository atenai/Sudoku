using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// ゲームオーバークラス
/// </summary>
/// <remarks>
/// ゲームオーバーのやり取りを管理
/// MVPパターンのPresenter（プレゼンター）を担当
/// </remarks>
public class GameOver : MonoBehaviour
{
	[SerializeField] GameOverUI gameOverUI;

	/// <summary>
	/// 初期化処理
	/// </summary>
	private void Start()
	{
		GameOverLogic gameOverLogic = new GameOverLogic();

		gameOverUI.GoToTitleButton.onClick.AddListener(gameOverLogic.OnClickGoToTitleButton);
	}
}

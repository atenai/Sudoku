using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// ゲームクリアロジッククラス
/// </summary>
/// <remarks>
/// ゲームクリアのロジックを管理
/// MVPパターンのModel（モデル）を担当
/// </remarks>
public class GameClearLogic
{
	/// <summary>
	/// タイトルへ行くボタンを押した際の処理
	/// </summary>
	public void OnClickGoToTitleButton()
	{
		GameManager.SingletonInstance.ChangeScene(new TitleSetting(), GameManager.SingletonInstance.Title_Scene_Name);
	}
}

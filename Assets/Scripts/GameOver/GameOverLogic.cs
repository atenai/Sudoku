using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// ゲームオーバーロジッククラス
/// </summary>
/// <remarks>
/// ゲームオーバーのロジックを管理
/// MVPパターンのModel（モデル）を担当
/// </remarks>
public class GameOverLogic
{
	/// <summary>
	/// タイトルへ行くボタンを押した際の処理
	/// </summary>
	public void OnClickGoToTitleButton()
	{
		GameManager.SingletonInstance.ChangeScene(new TitleSetting(), GameManager.SingletonInstance.Title_Scene_Name);
	}
}

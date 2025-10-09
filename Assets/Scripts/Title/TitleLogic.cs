using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// タイトルロジッククラス
/// </summary>
/// <remarks>
/// タイトルのロジックを管理
/// MVPパターンのModel（モデル）を担当
/// </remarks>
public class TitleLogic
{
	/// <summary>
	/// イージーボタンを押した際の処理
	/// </summary>
	public void OnClickEasyButton()
	{
		GameManager.SingletonInstance.ChangeScene(new MainGameSetting(MainGameSetting.DifficultyType.Easy), GameManager.SingletonInstance.MainGame_Scene_Name);
	}

	/// <summary>
	/// ノーマルボタンを押した際の処理
	/// </summary>
	public void OnClickNormalButton()
	{
		GameManager.SingletonInstance.ChangeScene(new MainGameSetting(MainGameSetting.DifficultyType.Normal), GameManager.SingletonInstance.MainGame_Scene_Name);
	}

	/// <summary>
	/// ハードボタンを押した際の処理
	/// </summary>
	public void OnClickHardButton()
	{
		GameManager.SingletonInstance.ChangeScene(new MainGameSetting(MainGameSetting.DifficultyType.Hard), GameManager.SingletonInstance.MainGame_Scene_Name);
	}
}

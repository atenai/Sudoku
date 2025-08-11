using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// タイトルロジッククラス
/// </summary>
public class TitleLogic
{
	private const string MainGame_Scene_Name = "MainGameScene";

	/// <summary>
	/// イージーボタンを押した際の処理
	/// </summary>
	public void OnClickEasyButton()
	{
		GameManager.SingletonInstance.ChangeScene(new MainGameSetting(MainGameSetting.DifficultyType.Easy), MainGame_Scene_Name);
	}

	/// <summary>
	/// ノーマルボタンを押した際の処理
	/// </summary>
	public void OnClickNormalButton()
	{
		GameManager.SingletonInstance.ChangeScene(new MainGameSetting(MainGameSetting.DifficultyType.Normal), MainGame_Scene_Name);
	}

	/// <summary>
	/// ハードボタンを押した際の処理
	/// </summary>
	public void OnClickHardButton()
	{
		GameManager.SingletonInstance.ChangeScene(new MainGameSetting(MainGameSetting.DifficultyType.Hard), MainGame_Scene_Name);
	}
}

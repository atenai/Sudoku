using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// タイトルロジッククラス
/// </summary>
public class TitleLogic
{
	private const string MainGame_Scene_Name = "MainGameScene";

	public void OnClickEasyButton()
	{
		GameManager.SingletonInstance.ChangeScene(new MainGameSetting(MainGameSetting.DifficultyType.Easy), MainGame_Scene_Name);
	}

	public void OnClickNormalButton()
	{
		GameManager.SingletonInstance.ChangeScene(new MainGameSetting(MainGameSetting.DifficultyType.Normal), MainGame_Scene_Name);
	}

	public void OnClickHardButton()
	{
		GameManager.SingletonInstance.ChangeScene(new MainGameSetting(MainGameSetting.DifficultyType.Hard), MainGame_Scene_Name);
	}
}

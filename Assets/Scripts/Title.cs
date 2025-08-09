using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Title : MonoBehaviour
{
	private const string MainGame_Scene_Name = "MainGameScene";

	[SerializeField] TitleUI titleUI;

	private void Start()
	{
		titleUI.EasyButton.onClick.AddListener(OnClickEasyButton);
		titleUI.NormalButton.onClick.AddListener(OnClickNormalButton);
		titleUI.HardButton.onClick.AddListener(OnClickHardButton);
	}

	private void OnClickEasyButton()
	{
		GameManager.SingletonInstance.ChangeScene(new MainGameSetting(MainGameSetting.DifficultyType.Easy), MainGame_Scene_Name);
	}

	private void OnClickNormalButton()
	{
		GameManager.SingletonInstance.ChangeScene(new MainGameSetting(MainGameSetting.DifficultyType.Normal), MainGame_Scene_Name);
	}

	private void OnClickHardButton()
	{
		GameManager.SingletonInstance.ChangeScene(new MainGameSetting(MainGameSetting.DifficultyType.Hard), MainGame_Scene_Name);
	}
}

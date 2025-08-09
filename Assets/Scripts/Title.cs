using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Title : MonoBehaviour
{
	private const string MainGame_Scene_Name = "MainGameScene";

	[SerializeField] private Button button;

	private void Start()
	{
		button.onClick.AddListener(OnClick);
	}

	private void OnClick()
	{
		GameManager.SingletonInstance.ChangeScene(new MainGameSetting(MainGameSetting.DifficultyType.Easy), MainGame_Scene_Name);
	}
}

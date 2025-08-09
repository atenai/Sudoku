using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// メインゲームセッティングクラス
/// </summary>
public class MainGameSetting : ISetting
{
	public enum DifficultyType
	{
		Easy,
		Normal,
		Hard,
	}

	private DifficultyType difficulty = DifficultyType.Easy;
	public DifficultyType Difficulty => difficulty;

	public MainGameSetting(DifficultyType difficulty)
	{
		this.difficulty = difficulty;
	}
}

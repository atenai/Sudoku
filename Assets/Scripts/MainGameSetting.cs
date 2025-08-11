using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// メインゲームセッティングクラス
/// </summary>
public class MainGameSetting : ISetting
{
	/// <summary>
	/// 難易度タイプ
	/// </summary>
	public enum DifficultyType
	{
		Easy,
		Normal,
		Hard,
	}

	private DifficultyType difficulty = DifficultyType.Easy;
	public DifficultyType Difficulty => difficulty;

	/// <summary>
	/// コンストラクタ
	/// </summary>
	/// <param name="difficulty">設定した難易度</param>
	public MainGameSetting(DifficultyType difficulty)
	{
		this.difficulty = difficulty;
	}
}

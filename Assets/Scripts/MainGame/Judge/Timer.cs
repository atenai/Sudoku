using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Timer
{
	int minute = 1;
	float seconds = 0f;
	float totalTime = 0f;

	public Timer() { }

	public void InitTimerSystem(MainGameSetting.DifficultyType difficultyType)
	{
		switch (difficultyType)
		{
			case MainGameSetting.DifficultyType.Easy:
				this.minute = 10;
				this.seconds = 30f;
				break;
			case MainGameSetting.DifficultyType.Normal:
				this.minute = 1;
				this.seconds = 0f;
				break;
			case MainGameSetting.DifficultyType.Hard:
				this.minute = 30;
				this.seconds = 0f;
				break;
		}
	}

	public bool UpdateTimerSystem()
	{
		totalTime = (minute * 60f) + seconds;
		totalTime = totalTime - Time.deltaTime;
		minute = (int)(totalTime / 60f);
		seconds = totalTime - (minute * 60f);
		if (minute <= 0 && seconds <= 0f)
		{
			return false;
		}

		return true;
	}

	public int GetMinute()
	{
		return minute;
	}

	public float GetSeconds()
	{
		return seconds;
	}
}

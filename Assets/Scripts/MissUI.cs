using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class MissUI : MonoBehaviour
{
	[SerializeField] private TextMeshProUGUI missCount;
	[SerializeField] private TextMeshProUGUI missNumber;
	[SerializeField] private MainGame mainGame;

	private void Start()
	{

	}

	private void Update()
	{
		missNumber.text = mainGame.MainGameLogic.Judge.MissNumber.ToString();
		missCount.text = mainGame.MainGameLogic.Judge.MissCount.ToString();
	}
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Title : MonoBehaviour
{
	[SerializeField] TitleUI titleUI;

	/// <summary>
	/// 初期化処理
	/// </summary>
	private void Start()
	{
		TitleLogic titleLogic = new TitleLogic();

		titleUI.EasyButton.onClick.AddListener(titleLogic.OnClickEasyButton);
		titleUI.NormalButton.onClick.AddListener(titleLogic.OnClickNormalButton);
		titleUI.HardButton.onClick.AddListener(titleLogic.OnClickHardButton);
	}
}

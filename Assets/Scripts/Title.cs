using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Title : MonoBehaviour
{
	[SerializeField] TitleUI titleUI;

	private void Start()
	{
		TitleLogic titleLogic = new TitleLogic();

		titleUI.EasyButton.onClick.AddListener(titleLogic.OnClickEasyButton);
		titleUI.NormalButton.onClick.AddListener(titleLogic.OnClickNormalButton);
		titleUI.HardButton.onClick.AddListener(titleLogic.OnClickHardButton);
	}


}

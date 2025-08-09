using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TitleUI : MonoBehaviour
{
	[SerializeField] private Button playButton;

	[SerializeField] private Image difficultyImage;
	[SerializeField] private Button easyButton;
	[SerializeField] private Button normalButton;
	[SerializeField] private Button hardButton;

	public Button EasyButton => easyButton;
	public Button NormalButton => normalButton;
	public Button HardButton => hardButton;

	void Start()
	{
		difficultyImage.gameObject.SetActive(false);
		playButton.onClick.AddListener(OnClickPlay);
	}

	private void OnClickPlay()
	{
		difficultyImage.gameObject.SetActive(true);
	}
}

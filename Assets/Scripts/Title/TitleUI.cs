using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using DG.Tweening;

public class TitleUI : MonoBehaviour
{
	[SerializeField] TextMeshProUGUI titleText;
	[SerializeField] private Button playButton;
	[SerializeField] private TextMeshProUGUI playButtonText;

	[SerializeField] private Image difficultyImage;
	[SerializeField] private Button easyButton;
	[SerializeField] private TextMeshProUGUI easyButtonText;
	[SerializeField] private Button normalButton;
	[SerializeField] private TextMeshProUGUI normalButtonText;
	[SerializeField] private Button hardButton;
	[SerializeField] private TextMeshProUGUI hardButtonText;

	public Button EasyButton => easyButton;
	public Button NormalButton => normalButton;
	public Button HardButton => hardButton;

	/// <summary>
	/// 初期化処理
	/// </summary>
	private void Start()
	{
		difficultyImage.gameObject.SetActive(false);
		playButton.onClick.AddListener(OnClickPlay);

		titleText.DOText("NumberPlace(Sudoku)", 1, scrambleMode: ScrambleMode.All).SetEase(Ease.Linear);
		playButtonText.DOText("Play", 1, scrambleMode: ScrambleMode.All).SetEase(Ease.Linear);
	}

	/// <summary>
	/// プレイボタンを押した際の処理
	/// </summary>
	private void OnClickPlay()
	{
		playButton.gameObject.SetActive(false);
		difficultyImage.gameObject.SetActive(true);
		easyButtonText.DOText("Easy", 1, scrambleMode: ScrambleMode.All).SetEase(Ease.Linear);
		normalButtonText.DOText("Normal", 1, scrambleMode: ScrambleMode.All).SetEase(Ease.Linear);
		hardButtonText.DOText("Hard", 1, scrambleMode: ScrambleMode.All).SetEase(Ease.Linear);
	}
}

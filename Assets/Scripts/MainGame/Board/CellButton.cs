using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.Events;
using DG.Tweening;

public class CellButton : MonoBehaviour
{
	public enum ColorType
	{
		Normal,
		Select,
		Correct,
		Incorrect,
		RelatedHighlight,
	}

	public ColorType currentColorType = ColorType.Normal;
	public ColorType oldColorType = ColorType.Normal;

	/// <summary>
	/// セルの背景画像
	/// </summary>
	[SerializeField] private Image image;

	/// <summary>
	/// セルのボタン
	/// </summary>
	[SerializeField] private Button button;

	/// <summary>
	/// セルの数字テキスト
	/// </summary>
	[SerializeField] private TextMeshProUGUI numberText;

	/// <summary>
	/// 9個の小メモテキスト
	/// </summary>
	[SerializeField] private TextMeshProUGUI[] memoTexts;

	/// <summary>
	/// メモON/OFF管理
	/// </summary>
	private bool[] memoActive = new bool[9];

	/// <summary>
	/// 初期化処理
	/// </summary>
	/// <param name="row"></param>
	/// <param name="col"></param>
	/// <param name="answerNumber">答え数値</param>
	/// <param name="questionNumber">問題数値</param>
	/// <param name="unityAction">ボタンがクリックされたときの処理</param>
	public void Initialize(int row, int col, int answerNumber, int questionNumber, UnityAction<int, int> unityAction)
	{
		numberText.DOText(questionNumber == 0 ? "" : questionNumber.ToString(), 1, scrambleMode: ScrambleMode.All).SetEase(Ease.Linear);
		button.onClick.AddListener(() =>
		{
			Debug.Log($"ボタン (縦:{row}, 横:{col}) がクリックされました!");
			Debug.Log($"答え番号: {answerNumber}");
			unityAction.Invoke(row, col);
		});

		// 問題に数字があるセルは入力不可にする
		button.interactable = questionNumber == 0;

		// メモ初期化
		ClearMemos();
	}

	/// <summary>
	/// 入力番号
	/// </summary>
	/// <param name="inputNumber">入力番号</param>
	public void SetNumber(int inputNumber)
	{
		Debug.Log("<color=green>入力番号 : " + inputNumber + "</color>");
		numberText.text = inputNumber == 0 ? "" : inputNumber.ToString();

		// 数字を入れたらメモを全消去
		ClearMemos();
	}

	/// <summary>
	/// メモ数字をセットする
	/// </summary>
	/// <param name="inputNumber">入力番号</param>
	public void SetMemoNumber(int inputNumber)
	{
		//メモテキストは配列で0～8で指定している為-1している
		int index = inputNumber - 1;
		//indexが0以下　または　indexが9と同じかそれ以上　なら切り上げる（だって対応したメモテキストが無いから）
		if (index < 0 || 9 <= index)
		{
			return;
		}

		memoActive[index] = !memoActive[index];

		memoTexts[index].gameObject.SetActive(memoActive[index]);
		//メモを入力したらメインの数字はOFFにする
		numberText.text = "";
	}

	/// <summary>
	/// 選択したセルをハイライト
	/// </summary>
	/// <param name="isSelected">選択中か？</param>
	public void SetHighlight(bool isSelected)
	{
		if (isSelected)
		{
			SetCurrentColorType(ColorType.Select);
		}
		else
		{
			SetCurrentColorType(oldColorType);
		}
		ViewCurrentColor();
	}

	public void SetRelatedHighlight(bool isRelated)
	{
		if (isRelated)
		{
			SetCurrentColorType(ColorType.RelatedHighlight);
		}
		else
		{
			SetCurrentColorType(oldColorType);
		}
		ViewCurrentColor();
	}

	public void SetCurrentColorType(ColorType colorType)
	{
		currentColorType = colorType;
	}

	public void SetOldColorType(ColorType colorType)
	{
		oldColorType = colorType;
	}

	public void ViewCurrentColor()
	{
		SetColor(currentColorType);
	}

	/// <summary>
	/// セルの背景色を変更する（正解・不正解・選択状態）
	/// </summary>
	/// <param name="color">セットする色</param>
	private void SetColor(ColorType colorType)
	{
		switch (colorType)
		{
			case ColorType.Normal:
				image.color = Color.white;
				break;
			case ColorType.Select:
				image.color = Color.cyan;
				break;
			case ColorType.Correct:
				image.color = Color.green;
				break;
			case ColorType.Incorrect:
				image.color = Color.red;
				break;
			case ColorType.RelatedHighlight:
				image.color = Color.yellow;
				break;
			default:
				image.color = Color.white;
				break;
		}
	}

	/// <summary>
	/// 正解時にセルを固定
	/// </summary>
	public void SetLockCell()
	{
		button.interactable = false;
		ClearMemos(); //正解時にメモも削除
	}

	/// <summary>
	/// 全メモをクリア
	/// </summary>
	private void ClearMemos()
	{
		for (int i = 0; i < memoTexts.Length; i++)
		{
			memoTexts[i].gameObject.SetActive(false);
			memoActive[i] = false;
		}
	}
}
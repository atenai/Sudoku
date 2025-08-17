using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class CellButton : MonoBehaviour, ICellButton
{
	[SerializeField] private Image image;
	[SerializeField] private Button button;
	public bool IsInteractable { get => button.interactable; }
	[SerializeField] private TextMeshProUGUI numberText;
	/// <summary>
	/// 9個の小テキスト
	/// </summary>
	[SerializeField] private TextMeshProUGUI[] memoTexts;
	/// <summary>
	/// メモON/OFF管理
	/// </summary>
	private bool[] memoActive = new bool[9];

	private int row;
	public int Row { get => row; set => row = value; }
	private int col;
	public int Col { get => col; set => col = value; }
	/// <summary>
	/// 答えの数値
	/// </summary>
	private int answerNumber;
	public int AnswerNumber { get => answerNumber; set => answerNumber = value; }
	/// <summary>
	/// 問題の数値
	/// </summary>
	private int questionNumber;
	public int QuestionNumber { get => questionNumber; set => questionNumber = value; }

	private MainGame mainGame;

	/// <summary>
	/// 初期化処理
	/// </summary>
	/// <param name="row"></param>
	/// <param name="col"></param>
	/// <param name="answerNumber"></param>
	/// <param name="questionNumber"></param>
	public void Initialize(int row, int col, int answerNumber, int questionNumber, MainGame mainGame)
	{
		this.row = row;
		this.col = col;
		this.answerNumber = answerNumber;
		this.questionNumber = questionNumber;
		this.mainGame = mainGame;

		numberText.text = questionNumber == 0 ? "" : questionNumber.ToString();
		button.onClick.AddListener(OnClick);

		// 問題に数字があるセルは入力不可にする
		button.interactable = questionNumber == 0;

		// メモ初期化
		ClearMemos();
	}

	/// <summary>
	/// マス（セル）が押された行う処理
	/// </summary>
	public void OnClick()
	{
		Debug.Log($"ボタン (縦:{row}, 横:{col}) がクリックされました!");
		Debug.Log($"答え番号: {answerNumber}");
		Debug.Log($"問題番号: {questionNumber}");

		mainGame.IMainGameLogic.IMainGameInput.ISelectCell(this);
	}

	//5
	/// <summary>
	/// 入力番号
	/// </summary>
	/// <param name="number"></param>
	public void SetNumber(int number)
	{
		Debug.Log("<color=green>入力番号 : " + number + "</color>");
		numberText.text = number == 0 ? "" : number.ToString();

		// 数字を入れたらメモを全消去
		ClearMemos();

		//6
		// 入力ごとに判定する
		mainGame.IMainGameLogic.IJudge.CheckAnswer(this, number);
	}

	/// <summary>
	/// メモON/OFF切り替え
	/// </summary>
	/// <param name="number">入力番号</param>
	public void ToggleMemo(int number)
	{
		//メモテキストは配列で0～8で指定している為-1している
		int index = number - 1;
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

	/// <summary>
	/// 選択したセルをハイライト
	/// </summary>
	/// <param name="isSelected">選択中か？</param>
	public void Highlight(bool isSelected)
	{
		if (isSelected)
		{
			SetColor(Color.cyan); //選択中は水色で表示
		}
		else
		{
			SetColor(Color.white); //通常時は白
		}
	}

	/// <summary>
	/// セルの背景色を変更する（正解・不正解・選択状態）
	/// </summary>
	/// <param name="color">セットする色</param>
	public void SetColor(Color color)
	{
		if (image != null)
		{
			image.color = color;
		}
	}

	/// <summary>
	/// 正解時にセルを固定
	/// </summary>
	public void LockCell()
	{
		button.interactable = false;
		ClearMemos(); //正解時にメモも削除
	}
}
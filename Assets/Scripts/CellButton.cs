using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class CellButton : MonoBehaviour, ICellButton
{
	[SerializeField] private Image image;
	[SerializeField] private Button button;
	public bool IIsInteractable { get => button.interactable; }
	[SerializeField] private TextMeshProUGUI numberText;
	/// <summary>
	/// 9個の小メモテキスト
	/// </summary>
	[SerializeField] private TextMeshProUGUI[] memoTexts;
	/// <summary>
	/// メモON/OFF管理
	/// </summary>
	private bool[] memoActive = new bool[9];

	private int row;
	public int IRow { get => row; set => row = value; }
	private int col;
	public int ICol { get => col; set => col = value; }
	/// <summary>
	/// 答え数値
	/// </summary>
	private int answerNumber;
	/// <summary>
	/// 答え数値のプロパティ
	/// </summary>
	public int IAnswerNumber { get => answerNumber; set => answerNumber = value; }
	/// <summary>
	/// 問題数値
	/// </summary>
	private int questionNumber;
	/// <summary>
	/// 問題数値のプロパティ
	/// </summary>
	public int IQuestionNumber { get => questionNumber; set => questionNumber = value; }

	/// <summary>
	/// メインゲーム
	/// </summary>
	private IMainGame iMainGame;

	/// <summary>
	/// 初期化処理
	/// </summary>
	/// <param name="row"></param>
	/// <param name="col"></param>
	/// <param name="answerNumber">答え数値</param>
	/// <param name="questionNumber">問題数値</param>
	/// /// <param name="mainGame">メインゲーム</param>
	public void Initialize(int row, int col, int answerNumber, int questionNumber, IMainGame iMainGame)
	{
		this.row = row;
		this.col = col;
		this.answerNumber = answerNumber;
		this.questionNumber = questionNumber;
		this.iMainGame = iMainGame;

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

		iMainGame.IMainGameLogic.IMainGameInput.ISelectCell(this);
	}

	//5
	/// <summary>
	/// 入力番号
	/// </summary>
	/// <param name="number"></param>
	public void ISetNumber(int number)
	{
		Debug.Log("<color=green>入力番号 : " + number + "</color>");
		numberText.text = number == 0 ? "" : number.ToString();

		// 数字を入れたらメモを全消去
		ClearMemos();

		//6
		// 入力ごとに判定する
		iMainGame.IMainGameLogic.IJudge.ICheckAnswer(this, number);
	}

	/// <summary>
	/// メモON/OFF切り替え
	/// </summary>
	/// <param name="number">入力番号</param>
	public void IToggleMemo(int number)
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
	public void IHighlight(bool isSelected)
	{
		if (isSelected)
		{
			ISetColor(Color.cyan); //選択中は水色で表示
		}
		else
		{
			ISetColor(Color.white); //通常時は白
		}
	}

	/// <summary>
	/// セルの背景色を変更する（正解・不正解・選択状態）
	/// </summary>
	/// <param name="color">セットする色</param>
	public void ISetColor(Color color)
	{
		if (image != null)
		{
			image.color = color;
		}
	}

	/// <summary>
	/// 正解時にセルを固定
	/// </summary>
	public void ILockCell()
	{
		button.interactable = false;
		ClearMemos(); //正解時にメモも削除
	}
}
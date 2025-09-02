using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.Events;

public class CellButton : MonoBehaviour
{
	[SerializeField] private Image image;
	[SerializeField] private Button button;
	[SerializeField] private TextMeshProUGUI numberText;
	[SerializeField] private TextMeshProUGUI[] memoTexts;

	private bool[] memoActive = new bool[9];

	// ★ 追加：固定セル判定
	public bool IsLocked { get; private set; } = false;

	public void Initialize(int row, int col, int answerNumber, int questionNumber, UnityAction<int, int> unityAction)
	{
		if (button == null || numberText == null || image == null || memoTexts == null || memoTexts.Length < 9)
		{
			Debug.LogError($"[CellButton] 参照未設定あり: button={button != null}, numberText={numberText != null}, image={image != null}, memoTextsLen={(memoTexts == null ? 0 : memoTexts.Length)} ({name})");
			return;
		}

		// 表示
		numberText.text = questionNumber == 0 ? "" : questionNumber.ToString();

		// ★ 固定セルかどうかを決めて interactable を反転
		IsLocked = questionNumber != 0;
		button.interactable = !IsLocked;

		// クリック -> 選択通知
		button.onClick.RemoveAllListeners();
		button.onClick.AddListener(() =>
		{
			Debug.Log($"ボタン (縦:{row}, 横:{col}) がクリックされました!");
			Debug.Log($"答え番号: {answerNumber}");
			if (unityAction != null)
			{
				unityAction.Invoke(row, col);
			}
		});

		// メモ初期化
		ClearMemos();
	}

	public void SetNumber(int inputNumber)
	{
		if (numberText == null)
		{
			return;
		}

		Debug.Log("<color=green>入力番号 : " + inputNumber + "</color>");
		numberText.text = inputNumber == 0 ? "" : inputNumber.ToString();

		// 数字を入れたらメモを全消去
		ClearMemos();
	}

	public void SetMemoNumber(int inputNumber)
	{
		// 以前: if (button.interactable) { return; } ←逆でした
		if (IsLocked)
		{
			return;
		}

		if (memoTexts == null || memoTexts.Length < 9)
		{
			Debug.LogError($"[CellButton] memoTexts 未設定または数不足 ({name})");
			return;
		}

		int index = inputNumber - 1;
		if (index < 0 || index >= 9)
		{
			return;
		}

		memoActive[index] = !memoActive[index];
		memoTexts[index].gameObject.SetActive(memoActive[index]);

		if (numberText != null)
		{
			numberText.text = "";
		}
	}

	public void SetHighlight(bool isSelected)
	{
		if (isSelected)
		{
			SetColor(Color.cyan);
		}
		else
		{
			SetColor(Color.white);
		}
	}

	public void SetColor(Color color)
	{
		if (image != null)
		{
			image.color = color;
		}
	}

	public void SetLockCell()
	{
		IsLocked = true;
		if (button != null)
		{
			button.interactable = false;
		}
		ClearMemos();
	}

	private void ClearMemos()
	{
		if (memoTexts == null)
		{
			return;
		}

		for (int i = 0; i < memoTexts.Length; i++)
		{
			if (memoTexts[i] != null)
			{
				memoTexts[i].gameObject.SetActive(false);
			}
			memoActive[i] = false;
		}
	}
}

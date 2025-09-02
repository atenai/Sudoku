using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainGameUI : MonoBehaviour
{
	/// <summary>
	/// ボード
	/// </summary>
	[SerializeField] private Board board;

	/// <summary>
	/// ミスUI
	/// </summary>
	[SerializeField] private MissUI missUI;

	[SerializeField] private InputNumberButton[] inputNumberButtons;

	[SerializeField] private ClearButton clearButton;

	[SerializeField] private MemoButton memoButton;

	public Board Board => board;

	public MissUI MissUI => missUI;

	public InputNumberButton[] InputNumberButtons => inputNumberButtons;

	public ClearButton ClearButton => clearButton;

	public MemoButton MemoButton => memoButton;


	private GameEventHub hub;

	private void Awake()
	{
		if (board == null)
		{
			Debug.LogError("[MainGameUI] Board が未設定です", this);
		}
		if (missUI == null)
		{
			Debug.LogError("[MainGameUI] MissUI が未設定です", this);
		}
		if (clearButton == null)
		{
			Debug.LogError("[MainGameUI] ClearButton が未設定です", this);
		}
		if (memoButton == null)
		{
			Debug.LogError("[MainGameUI] MemoButton が未設定です", this);
		}
		if (inputNumberButtons == null || inputNumberButtons.Length == 0)
		{
			Debug.LogError("[MainGameUI] InputNumberButtons が未設定または空です", this);
		}
	}

	/// <summary>
	/// MainGame からイベントハブを受け取り、購読する
	/// </summary>
	// MainGameUI.cs の Bind() 内で購読を追加
	// MainGameUI.cs
	public void Bind(GameEventHub eventHub)
	{
		if (hub != null)
		{
			Unbind();
		}

		hub = eventHub;
		if (hub == null)
		{
			return;
		}

		hub.OnBoardInitialized += HandleBoardInitialized;
		hub.OnSelectionChanged += HandleSelectionChanged;
		hub.OnCellInputResult += HandleCellInputResult;
		hub.OnCellCleared += HandleCellCleared;
		hub.OnMissCountChanged += HandleMissCountChanged;
		hub.OnMemoModeChanged += HandleMemoModeChanged;

		// ★ メモ入力（必須）
		hub.OnMemoToggledAtCell += HandleMemoToggledAtCell;
	}

	private void HandleMemoToggledAtCell(int row, int col, int number)
	{
		board.ToggleMemo(row, col, number);
	}

	private void OnDestroy() => Unbind();

	private void Unbind()
	{
		if (hub == null)
		{
			return;
		}

		hub.OnBoardInitialized -= HandleBoardInitialized;
		hub.OnSelectionChanged -= HandleSelectionChanged;
		hub.OnCellInputResult -= HandleCellInputResult;
		hub.OnCellCleared -= HandleCellCleared;
		hub.OnMissCountChanged -= HandleMissCountChanged;
		hub.OnMemoModeChanged -= HandleMemoModeChanged;

		// ★ これを忘れずに
		hub.OnMemoToggledAtCell -= HandleMemoToggledAtCell;

		hub = null;
	}

	// ---- イベント → 個別UIへ反映 ----

	// MainGameUI.cs 内の購読ハンドラ
	private void HandleBoardInitialized(int[,] aGrid, int[,] qGrid)
	{
		board.CreateCell(aGrid, qGrid, (r, c) =>
		{
			if (hub != null)
			{
				hub.RaiseCellSelectedFromUI(r, c);
			}
		});
	}

	private void HandleSelectionChanged(int curR, int curC, int oldR, int oldC)
	{
		board.SetSelectedHighlight(curR, curC, oldR, oldC);
	}

	private void HandleCellInputResult(int r, int c, int number, bool isCorrect)
	{
		board.ShowNumber(r, c, number, isCorrect);
	}

	private void HandleCellCleared(int r, int c)
	{
		board.ClearNumber(r, c);
	}

	private void HandleMissCountChanged(int miss, int fail)
	{
		missUI.SetMissCount(miss);
		missUI.SetFailNumber(fail);
	}

	private void HandleMemoModeChanged(bool on)
	{
		// MemoButton は Initialize 時にクリック後の UpdateVisual を呼ぶが、
		// 外部からも状態反映できるよう public メソッドを用意しておく（下章参照）
		memoButton.SetVisual(on);
	}

	// 入力ボタンなどの初期化は従来どおり MainGame からコールバックでもOK
	public void Initialize(System.Action<int> onNumberClick, System.Action onClearClick, System.Func<bool> getMemoMode, System.Action onToggleMemo)
	{
		if (inputNumberButtons != null)
		{
			foreach (var btn in inputNumberButtons)
			{
				if (btn != null)
				{
					btn.Initialize(n => onNumberClick?.Invoke(n));
				}
			}
		}
		if (clearButton != null)
		{
			clearButton.Initialize(() => onClearClick?.Invoke());
		}
		if (memoButton != null)
		{
			memoButton.Initialize(getMemoMode, () => onToggleMemo?.Invoke());
		}
	}
}

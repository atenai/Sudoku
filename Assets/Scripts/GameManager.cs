using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// ゲームマネージャークラス
/// </summary>
public class GameManager : MonoBehaviour
{
	//シングルトンで作成（ゲーム中に１つのみにする）
	private static GameManager singletonInstance = null;
	public static GameManager SingletonInstance => singletonInstance;

	public readonly string Title_Scene_Name = "TitleScene";
	public readonly string MainGame_Scene_Name = "MainGameScene";
	public readonly string GameClear_Scene_Name = "GameClearScene";
	public readonly string GameOver_Scene_Name = "GameOverScene";

	private ISetting setting;

	/// <summary>
	/// 初期化処理
	/// </summary>
	private void Awake()
	{
		//staticな変数instanceはメモリ領域は確保されていますが、初回では中身が入っていないので、中身を入れます。
		if (singletonInstance == null)
		{
			singletonInstance = this;//thisというのは自分自身のインスタンスという意味になります。この場合、Playerのインスタンスという意味になります。
			DontDestroyOnLoad(this.gameObject);//シーンを切り替えた時に破棄しない
		}
		else
		{
			Destroy(this.gameObject);//中身がすでに入っていた場合、自身のインスタンスがくっついているゲームオブジェクトを破棄します。
		}
	}

	/// <summary>
	/// そのシーンに必要な値のクラスを取得する
	/// </summary>
	/// <returns></returns>
	public ISetting GetSetting()
	{
		return setting;
	}

	// public void SetSetting(ISetting setting)
	// {
	// 	this.setting = setting;
	// }

	/// <summary>
	/// シーン切り替え
	/// </summary>
	/// <param name="setting">そのシーン先で必要な値のクラス</param>
	/// <param name="sceneName">遷移先のシーン名</param>
	public void ChangeScene(ISetting setting, string sceneName)
	{
		this.setting = setting;
		SceneManager.LoadScene(sceneName);
	}
}

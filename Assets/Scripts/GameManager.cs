using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
	//シングルトンで作成（ゲーム中に１つのみにする）
	private static GameManager singletonInstance = null;
	public static GameManager SingletonInstance => singletonInstance;

	private ISetting setting;

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

	public ISetting GetSetting()
	{
		return setting;
	}

	// public void SetSetting(ISetting setting)
	// {
	// 	this.setting = setting;
	// }

	public void ChangeScene(ISetting setting, string sceneName)
	{
		this.setting = setting;
		SceneManager.LoadScene(sceneName);
	}
}

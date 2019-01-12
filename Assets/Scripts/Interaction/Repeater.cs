using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// 最初に戻る操作を制御します
/// </summary>
public class Repeater : MonoBehaviour
{
#if UNITY_EDITOR
	static bool BackButton => Input.GetKeyDown(KeyCode.Backspace); //Unity 上ならバックスペースキーを
#else
	static bool BackButton => OVRInput.GetDown(OVRInput.Button.Back);	//Oculus Go / Gear VR ではバックボタンを使用します
#endif

	void Update()
	{
		if (BackButton)
		{
			SceneManager.LoadScene(SceneManager.GetActiveScene().name); //バックボタンを押したら、シーンを再読み込みします
		}
	}
}
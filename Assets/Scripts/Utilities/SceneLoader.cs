using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// アプリサイズの制限と起動速度の問題を回避するためのロード専用シーンをハンドリングします。
/// </summary>
public class SceneLoader : MonoBehaviour
{
	[SerializeField] string sceneName;
	[SerializeField] OVROverlay overlay;
	[SerializeField] Renderer fadable;

	IEnumerator Start()
	{
		overlay.enabled = false;
		fadable.enabled = true;
		fadable.material.color = Color.clear;
		yield return Tween.FadeMaterial(this, fadable.material, Color.white, 1f);

		// OVROverlay が描画されるのに2フレームかかるので待ち
		overlay.enabled = true;
		yield return null;
		yield return null;

		fadable.enabled = false;

		var operation = SceneManager.LoadSceneAsync(sceneName, LoadSceneMode.Single);
		operation.allowSceneActivation = false;

		yield return new WaitForSeconds(2f);
		yield return new WaitUntil(() => operation.progress >= 0.9f);

		overlay.enabled = false;
		fadable.enabled = true;
		yield return Tween.FadeMaterial(this, fadable.material, Color.clear, 1f);

		operation.allowSceneActivation = true;
	}
}
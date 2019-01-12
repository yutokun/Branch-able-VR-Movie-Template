using System;
using TMPro;
using UnityEngine;
using UnityEngine.Video;

/// <summary>
/// ビデオの再生コントロールとステレオ判定を行います。
/// </summary>
public class PlaybackController : MonoBehaviour
{
	[SerializeField] VideoPlayer player;
	[SerializeField] Material material;
	[SerializeField] GameObject end;
	[SerializeField] BranchCreator branchCreator;
	[SerializeField] ControllerVisiblity controllerVisible;
	[SerializeField] Pointer[] pointers;

	[SerializeField, Header("最初に再生するビデオ")] Video firstVideo;
	Video currentVideo;

	[HideInInspector] public NextIs currentNextIs;

	void Start()
	{
		material.SetColor("_Tint", Color.black); //空を真っ暗にします

		player.prepareCompleted += source => //再生開始時にビデオの色を正しく。
		{
			Debug.Log("Color Set");
			material.SetColor("_Tint", new Color(0.5f, 0.5f, 0.5f, 0.5f));
		};

		end.SetActive(false); //終了表示を非表示に
		player.loopPointReached += OnVideoEnd;
	}

	/// <summary>
	/// １つのビデオが終わった時、選択肢を出すか、終了させるかを出しわけます。
	/// </summary>
	/// <param name="source"></param>
	void OnVideoEnd(VideoPlayer source)
	{
		switch (currentNextIs)
		{
			case NextIs.Video: //選択肢を作成
				branchCreator.Create(currentVideo.sentence, currentVideo.branches);
				break;

			case NextIs.End: //終了パネルを表示
				ShowEndPanel();
				break;
		}

		foreach (var item in pointers) item.SetRunningState(true);
		controllerVisible.ChangeAlpha(1f);
	}

	/// <summary>
	/// 最初のビデオを再生します
	/// </summary>
	public void PlayFirstVideo()
	{
		Play(firstVideo.clip, firstVideo.nextIs, firstVideo);
	}

	/// <summary>
	/// ビデオを再生します
	/// </summary>
	/// <param name="clip">再生するビデオクリップ</param>
	/// <param name="nextIs">このビデオが終了した場合のできごとタイプ</param>
	/// <param name="video">ビデオオブジェクト</param>
	public void Play(VideoClip clip, NextIs nextIs, Video video)
	{
		material.SetColor("_Tint", Color.black);

		//ステレオ判定
		var layout = clip.name.EndsWith("_TB", StringComparison.OrdinalIgnoreCase) ? 2 : 0;
		material.SetFloat("_Layout", layout);

		//再生
		player.clip = clip;
		player.Play();

		//ポインターやコントローラを非表示に
		foreach (var item in pointers) item.SetRunningState(false);
		controllerVisible.ChangeAlpha(0f);
		currentNextIs = nextIs;
		currentVideo = video;
	}

	/// <summary>
	/// 終了パネルを表示します。
	/// </summary>
	void ShowEndPanel()
	{
		end.SetActive(true);
		foreach (var item in end.GetComponentsInChildren<TextMeshPro>())
		{
			item.gameObject.SetActive(true);
			item.color = Color.clear;
			Tween.FadeText(this, item, Color.white, 1f); //終了テキストを白にフェードインします
		}
	}
}
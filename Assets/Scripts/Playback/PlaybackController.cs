using System;
using TMPro;
using UnityEngine;
using UnityEngine.Video;

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
		material.SetColor("_Tint", Color.black);

		player.prepareCompleted += source =>
		{
			Debug.Log("Color Set");
			material.SetColor("_Tint", new Color(0.5f, 0.5f, 0.5f, 0.5f));
		};

		end.SetActive(false);
		player.loopPointReached += OnVideoEnd;
	}

	void OnVideoEnd(VideoPlayer source)
	{
		switch (currentNextIs)
		{
			case NextIs.Video:
				branchCreator.Create(currentVideo.sentence, currentVideo.branches);
				break;

			case NextIs.End:
				ShowEndPanel();
				break;
		}

		foreach (var item in pointers) item.SetRunningState(true);
		controllerVisible.ChangeAlpha(1f);
	}

	public void PlayFirstVideo()
	{
		Play(firstVideo.clip, firstVideo.nextIs, firstVideo);
	}

	public void Play(VideoClip clip, NextIs nextIs, Video video)
	{
		material.SetColor("_Tint", Color.black);

		// ステレオ判定
		var layout = clip.name.EndsWith("_TB", StringComparison.OrdinalIgnoreCase) ? 2 : 0;
		material.SetFloat("_Layout", layout);

		player.clip = clip;
		player.Play();

		foreach (var item in pointers) item.SetRunningState(false);
		controllerVisible.ChangeAlpha(0f);
		currentNextIs = nextIs;
		currentVideo = video;
	}

	void ShowEndPanel()
	{
		end.SetActive(true);
		foreach (var item in end.GetComponentsInChildren<TextMeshPro>())
		{
			item.gameObject.SetActive(true);
			item.color = Color.clear;
			Tween.FadeText(this, item, Color.white, 1f);
		}
	}
}
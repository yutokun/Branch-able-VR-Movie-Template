using UnityEngine;
using UnityEngine.Video;

[System.Serializable]
public struct Branch
{
	public Video video;
	[Multiline] public string text;
}

public enum NextIs
{
	Video,
	End
}

/// <summary>
/// 再生するビデオと、その後の処理を指定します。
/// </summary>
public class Video : MonoBehaviour
{
	PlaybackController playback;
	public VideoClip clip;
	public NextIs nextIs;

	[Multiline] public string sentence;
	public Branch[] branches;

	void Awake()
	{
		playback = FindObjectOfType<PlaybackController>();
	}

	public void Play()
	{
		playback.Play(clip, nextIs, this);
	}

	void OnValidate()
	{
		name = "Video" + (clip ? ": " + clip.name : "");
	}
}
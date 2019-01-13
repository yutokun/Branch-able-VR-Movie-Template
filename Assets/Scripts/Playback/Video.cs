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
	Credits,
	End
}

/// <summary>
/// 再生するビデオと、その後の処理を指定します。
/// </summary>
public class Video : MonoBehaviour
{
	PlaybackController player;
	public VideoClip clip;
	public NextIs nextIs;
	[Range(-180f, 180f)] public float rotationOffset;

	[Multiline] public string sentence;
	public Branch[] branches;
	public int currentBranchSize;

	public AudioClip overrideSoundOnBranch;

	void Awake()
	{
		player = FindObjectOfType<PlaybackController>();
	}

	public void Play()
	{
		player.Play(clip, nextIs, this);
	}

	void OnValidate()
	{
		name = "Video" + (clip ? ": " + clip.name : "");
		if (player) player.SetRotation(rotationOffset);
	}
}
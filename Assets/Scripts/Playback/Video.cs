using UnityEngine;
using UnityEngine.Video;

[System.Serializable]
public struct Branch
{
	public Video video;
	[Multiline] public string text;
}

/// <summary>
/// ビデオが終わったら選択肢を出すか終了するかを定義します。
/// </summary>
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
		//起動時にビデオ再生プログラムを見つけておきます
		playback = FindObjectOfType<PlaybackController>();
	}

	public void Play()
	{
		//次のビデオを再生します。
		playback.Play(clip, nextIs, this);
	}

	void OnValidate()
	{
		//ゲームオブジェクトの名前を、このビデオの名前にします。（わかりやすさのため）
		name = "Video" + (clip ? ": " + clip.name : "");
	}
}
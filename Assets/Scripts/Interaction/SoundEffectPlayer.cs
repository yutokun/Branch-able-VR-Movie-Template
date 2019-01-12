using UnityEngine;

/// <summary>
/// 効果音の再生を管理します。
/// </summary>
public class SoundEffectPlayer : MonoBehaviour
{
	new static AudioSource audio;
	static AudioClip pointClip, clickClip;
	[SerializeField] AudioClip point, click;

	void Start()
	{
		audio = GetComponent<AudioSource>();
		pointClip = point;
		clickClip = click;
	}

	/// <summary>
	/// ボタンを指した時の効果音を再生します。
	/// </summary>
	public static void PlayPointedSound()
	{
		if (pointClip) audio.PlayOneShot(pointClip);
	}

	/// <summary>
	/// ボタンをクリックした時の効果音を再生します。
	/// </summary>
	public static void PlayClickSound()
	{
		if (clickClip) audio.PlayOneShot(clickClip);
	}
}
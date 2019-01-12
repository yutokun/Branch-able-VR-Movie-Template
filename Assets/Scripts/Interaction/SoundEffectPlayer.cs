using UnityEngine;

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

	public static void PlayPointedSound()
	{
		if (pointClip) audio.PlayOneShot(pointClip);
	}

	public static void PlayClickSound()
	{
		if (clickClip) audio.PlayOneShot(clickClip);
	}
}
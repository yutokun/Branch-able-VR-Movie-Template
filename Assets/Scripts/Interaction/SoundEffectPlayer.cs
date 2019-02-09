using UnityEngine;

public enum SEType
{
	Pointed,
	Click
}

public class SoundEffectPlayer : MonoBehaviour
{
	static AudioSource Audio;
	static AudioClip Point, Click;
	[SerializeField] AudioClip point, click;

	void Start()
	{
		Audio = GetComponent<AudioSource>();
		Point = point;
		Click = click;
	}

	public static void Play(SEType seType)
	{
		AudioClip clip = null;

		switch (seType)
		{
			case SEType.Pointed:
				clip = Point;
				break;

			case SEType.Click:
				clip = Click;
				break;
		}

		if (clip != null) Audio.PlayOneShot(clip);
	}
}
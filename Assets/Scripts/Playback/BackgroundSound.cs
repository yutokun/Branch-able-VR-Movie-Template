using UnityEngine;

public enum Situation
{
	Start,
	Intermission,
	End
}

public class BackgroundSound : MonoBehaviour
{
	static AudioSource Audio;
	[SerializeField] AudioClip start, intermission, end;

	static AudioClip startClip, intermissionClip, endClip;

	void Start()
	{
		startClip = start;
		intermissionClip = intermission;
		endClip = end;

		Audio = GetComponent<AudioSource>();
		Play(Situation.Start);
	}

	public static void Play(Situation situation)
	{
		switch (situation)
		{
			case Situation.Start:
				if (startClip) Play(startClip);
				break;

			case Situation.Intermission:
				if (intermissionClip) Play(intermissionClip);
				break;

			case Situation.End:
				if (endClip) Play(endClip);
				break;
		}
	}

	public static void Play(AudioClip clip)
	{
		Audio.clip = clip;
		Audio.volume = 1f;
		Audio.Play();
	}

	public static void Stop(float duration = 0.5f)
	{
		if (Audio.isPlaying)
		{
			Audio.FadeVolume(0f, duration, () => Audio.Stop());
		}
	}
}
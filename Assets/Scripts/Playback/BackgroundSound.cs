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

	// TODO : Start 以外の名前を募集
	static AudioClip Start, Intermission, End;

	void Awake()
	{
		Start = start;
		Intermission = intermission;
		End = end;

		Audio = GetComponent<AudioSource>();
		Play(Situation.Start);
	}

	public static void Play(Situation situation)
	{
		switch (situation)
		{
			case Situation.Start:
				if (Start) Play(Start);
				break;

			case Situation.Intermission:
				if (Intermission) Play(Intermission);
				break;

			case Situation.End:
				if (End) Play(End);
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
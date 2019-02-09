using UnityEngine;

public class NextButton : MonoBehaviour, IPointable
{
	[HideInInspector] public Video video;
	[HideInInspector] public bool isClickable;

	public void Pointed()
	{
		SoundEffectPlayer.Play(SEType.Pointed);
	}

	public void UnPointed() { }

	public void Click()
	{
		if (!isClickable) return;

		SoundEffectPlayer.Play(SEType.Click);
		video.Play();
	}
}
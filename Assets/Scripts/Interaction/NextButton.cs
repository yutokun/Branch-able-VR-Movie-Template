using UnityEngine;

public class NextButton : MonoBehaviour, IPointable
{
	[HideInInspector] public Video video;
	[HideInInspector] public bool isClickable;

	public void Pointed()
	{
		if (!isClickable) return;

		SoundEffectPlayer.Play(SEType.Pointed);
		transform.Scale(1.1f, 0.15f);
	}

	public void UnPointed()
	{
		if (!isClickable) return;

		transform.Scale(1f, 0.3f);
	}

	public void Click()
	{
		if (!isClickable) return;

		SoundEffectPlayer.Play(SEType.Click);
		video.Play();
	}
}
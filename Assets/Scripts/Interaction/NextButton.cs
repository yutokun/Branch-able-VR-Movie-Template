using UnityEngine;

public class NextButton : MonoBehaviour, IPointable
{
	[HideInInspector] public Video video;
	[HideInInspector] public bool isClickable;

	public void Pointed()
	{
		SoundEffectPlayer.PlayPointedSound();
	}

	public void UnPointed() { }

	public void Click()
	{
		if (!isClickable) return;
		
		SoundEffectPlayer.PlayClickSound();
		video.Play();
		GetComponentInParent<BranchCreator>().Destroy();
	}
}
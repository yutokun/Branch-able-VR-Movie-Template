using UnityEngine;

public class NextButton : MonoBehaviour, IPointable
{
	public Video video;

	public void Pointed()
	{
		SoundEffectPlayer.PlayPointedSound();
	}

	public void Unpointed() { }

	public void Click()
	{
		SoundEffectPlayer.PlayClickSound();
		video.Play();
		GetComponentInParent<BranchCreator>().Destroy();
	}
}
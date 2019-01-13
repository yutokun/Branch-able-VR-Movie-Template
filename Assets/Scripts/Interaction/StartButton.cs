using UnityEngine;

public class StartButton : MonoBehaviour, IPointable
{
	[SerializeField] PlaybackController player;
	[SerializeField] Transform startParent;

	public void Pointed()
	{
		SoundEffectPlayer.PlayPointedSound();
	}

	public void Unpointed() { }

	public void Click()
	{
		SoundEffectPlayer.PlayClickSound();
		startParent.Scale(0f, 0.5f, onComplete: Play);
	}

	void Play()
	{
		player.PlayFirstVideo();
		Destroy(transform.parent.gameObject);
	}

#if UNITY_EDITOR
	void Update()
	{
		if (Input.GetKeyDown(KeyCode.Return)) Click();
	}
#endif
}
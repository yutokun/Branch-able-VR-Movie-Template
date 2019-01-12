using UnityEngine;

public class StartButton : MonoBehaviour, IPointable
{
	[SerializeField] PlaybackController playback;

	public void Pointed()
	{
		SoundEffectPlayer.PlayPointedSound();
	}

	public void Unpointed() { }

	public void Click()
	{
		SoundEffectPlayer.PlayClickSound();
		Tween.Scale(this, transform.parent, 0f, 0.5f);
		Invoke(nameof(Play), 0.5f);
	}

	void Play()
	{
		playback.PlayFirstVideo();
		Destroy(transform.parent.gameObject);
	}

#if UNITY_EDITOR
	void Update()
	{
		if (Input.GetKeyDown(KeyCode.Return)) Click();
	}
#endif
}
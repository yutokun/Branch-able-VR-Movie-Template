using TMPro;
using UnityEngine;

public class StartButton : MonoBehaviour, IPointable
{
	static PlaybackController Player => PlaybackController.Instance;
	[SerializeField] TextMeshPro text;
	[SerializeField] Transform button;
	bool clicked;

	public void Pointed()
	{
		SoundEffectPlayer.Play(SEType.Pointed);
	}

	public void UnPointed() { }

	public void Click()
	{
		if (clicked) return;
		clicked = true;

		SoundEffectPlayer.Play(SEType.Click);
		text.FadeTextAlpha(0f, 0.5f);
		button.Scale(0f, 0.5f, onComplete: Play);
	}

	void Play()
	{
		Player.PlayFirstVideo();
		Destroy(gameObject);
	}

#if UNITY_EDITOR
	void Update()
	{
		if (Input.GetKeyDown(KeyCode.Return)) Click();
	}
#endif
}
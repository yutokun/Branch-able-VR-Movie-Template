using TMPro;
using UnityEngine;

public class NextButton : MonoBehaviour, IPointable
{
	[HideInInspector] public Video video;
	public TextMeshPro title;
	bool isClickable;

	void Start()
	{
		transform.localScale = Vector3.zero;
		transform.Scale(1f, 0.5f, () => isClickable = true);
	}

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

	public void Destroy()
	{
		isClickable = false;
		transform.Scale(0f, 0.5f, () => Destroy(gameObject));
	}
}
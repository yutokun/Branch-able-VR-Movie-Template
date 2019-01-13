using TMPro;
using UnityEngine;

public class End : MonoBehaviour
{
	void Start()
	{
		gameObject.SetActive(false);
	}

	public void Show()
	{
		gameObject.SetActive(true);
		foreach (var item in GetComponentsInChildren<TextMeshPro>())
		{
			item.gameObject.SetActive(true);
			item.color = Color.clear;
			item.FadeText(Color.white, 1f);
		}
	}
}
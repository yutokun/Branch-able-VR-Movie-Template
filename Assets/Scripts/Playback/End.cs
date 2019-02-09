using TMPro;
using UnityEngine;

public class End : MonoBehaviour
{
	public static End Instance { get; private set; }

	void Awake()
	{
		Instance = this;

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
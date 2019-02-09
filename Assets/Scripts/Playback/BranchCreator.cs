#if UNITY_EDITOR
using System;
using System.Text.RegularExpressions;
#endif
using System.Collections.Generic;
using TMPro;
using UnityEngine;

/// <summary>
/// 動画の選択肢を作成します。
/// </summary>
public class BranchCreator : MonoBehaviour
{
	public static BranchCreator Instance { get; private set; }

	[SerializeField] GameObject button;
	[SerializeField] TextMeshPro text;
	[SerializeField] float y = -1f, interval = 1.1f;
	readonly List<NextButton> nextButtons = new List<NextButton>();

	void Awake()
	{
		Instance = this;

		text.text = "";

		var color = text.color;
		color.a = 0f;
		text.color = color;
	}

	public void Create(string sentence, Branch[] branches, int branchSize)
	{
		text.text = sentence;
		text.FadeTextAlpha(1f, 0.5f);

		var startX = -((interval / 2) * (branchSize - 1));
		for (var i = 0; i < branchSize; i++)
		{
			var nextButton = Instantiate(button, transform).GetComponentInChildren<NextButton>();
			nextButton.transform.localPosition = new Vector3(startX + (interval * i), y, 0);
			nextButton.title.text = branches[i].text;
			nextButton.video = branches[i].video;
			nextButtons.Add(nextButton);
		}
	}

	public void Destroy()
	{
		text.FadeTextAlpha(0f, 0.5f);

		foreach (var nextButton in nextButtons) nextButton.Destroy();
		nextButtons.Clear();
	}

#if UNITY_EDITOR
	void Update()
	{
		if (Input.anyKeyDown)
		{
			foreach (KeyCode code in Enum.GetValues(typeof(KeyCode)))
			{
				if (Input.GetKeyDown(code))
				{
					var codeString = code.ToString();
					if (codeString.Contains("Alpha") == false) break;

					var key = int.Parse(Regex.Match(codeString, @"[1-9]").Value);
					if (key <= nextButtons.Count)
					{
						nextButtons[key - 1].Click();
						break;
					}
				}
			}
		}
	}
#endif
}
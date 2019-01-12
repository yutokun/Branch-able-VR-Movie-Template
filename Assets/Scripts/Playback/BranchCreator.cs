#if UNITY_EDITOR
using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
#endif
using TMPro;
using UnityEngine;

/// <summary>
/// 動画の選択肢を作成します。
/// </summary>
public class BranchCreator : MonoBehaviour
{
	[SerializeField] GameObject button;
	[SerializeField] TextMeshPro text;
	[SerializeField] float y = -1f, interval = 1.1f;
#if UNITY_EDITOR
	List<NextButton> nextButtons = new List<NextButton>();
#endif

	void Start()
	{
		text.text = "";
		transform.localScale = Vector3.zero;
	}

	public void Create(string sentence, Branch[] branches, int branchSize)
	{
		text.text = sentence;
		var startX = -((interval / 2) * (branchSize - 1));
		for (var i = 0; i < branchSize; i++)
		{
			var obj = Instantiate(button, transform);
			obj.transform.localPosition = new Vector3(startX + (interval * i), y, 0);
			obj.GetComponentInChildren<TextMeshPro>().text = branches[i].text;
			obj.GetComponentInChildren<NextButton>().video = branches[i].video;
#if UNITY_EDITOR
			nextButtons.Add(obj.GetComponentInChildren<NextButton>());
#endif
		}

		transform.Scale(1f, 0.5f);
	}

	public void Destroy()
	{
		transform.Scale(0f, 0.5f, RemoveChildren);
	}

	void RemoveChildren()
	{
		foreach (Transform item in transform) Destroy(item.gameObject);
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
						nextButtons.Clear();
						break;
					}
				}
			}
		}
	}
#endif
}
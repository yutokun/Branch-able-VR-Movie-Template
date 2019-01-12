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

	/// <summary>
	/// 選択肢を作成して並べます。
	/// </summary>
	/// <param name="sentence">文章</param>
	/// <param name="branches">選択肢</param>
	public void Create(string sentence, Branch[] branches)
	{
		text.text = sentence;
		var startX = -((interval / 2) * (branches.Length - 1));
		for (var i = 0; i < branches.Length; i++)
		{
			var obj = Instantiate(button, transform);
			obj.transform.localPosition = new Vector3(startX + (interval * i), y, 0);
			obj.GetComponentInChildren<TextMeshPro>().text = branches[i].text;
			obj.GetComponentInChildren<NextButton>().video = branches[i].video;
#if UNITY_EDITOR
			nextButtons.Add(obj.GetComponentInChildren<NextButton>());
#endif
		}

		//拡大エフェクト
		Tween.Scale(this, transform, 1f, 0.5f);
	}

	public void Destroy()
	{
		//選択肢をすべて小さく縮小して、削除します。
		Tween.Scale(this, transform, 0f, 0.5f);
		Invoke(nameof(RemoveChildren), 0.5f);
	}

	void RemoveChildren()
	{
		//上記の Destroy() から呼ばれ、全ての選択肢オブジェクトを削除します。
		foreach (Transform item in transform) Destroy(item.gameObject);
	}

#if UNITY_EDITOR
	/// <summary>
	/// Unity 上でのみ、キーボードで操作できるようにしています
	/// </summary>
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
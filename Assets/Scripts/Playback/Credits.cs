using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
#if UNITY_EDITOR
using UnityEditor;
#endif

enum CreditType
{
	Text,
	Image
}

public class Credits : MonoBehaviour
{
	[System.Serializable]
	struct Credit
	{
		public CreditType creditType;
		[Multiline] public string text;
		public Sprite sprite;
	}

	TextMeshProUGUI textMesh;
	Image image;

	[SerializeField] Credit[] credits;
	int current;

	void Awake()
	{
		textMesh = GetComponentInChildren<TextMeshProUGUI>();
		textMesh.CrossFadeAlpha(0f, 0f, true);
		image = GetComponentInChildren<Image>();
		image.CrossFadeAlpha(0f, 0f, true);
	}

	public IEnumerator Play()
	{
		while (current < credits.Length)
		{
			yield return StartCoroutine(Next());
			++current;
		}
	}

	IEnumerator Next()
	{
		textMesh.CrossFadeAlpha(0f, 1f, true);
		image.CrossFadeAlpha(0f, 1f, true);

		yield return new WaitForSeconds(1f);

		switch (credits[current].creditType)
		{
			case CreditType.Text:
				textMesh.text = credits[current].text;
				textMesh.CrossFadeAlpha(1f, 1f, true);
				break;

			case CreditType.Image:
				image.sprite = credits[current].sprite;
				image.CrossFadeAlpha(1f, 1f, true);
				break;
		}
	}
}

#if UNITY_EDITOR
[CustomEditor(typeof(Credits))]
public class CreditsInspector : Editor
{
	SerializedProperty credits;

	void OnEnable()
	{
		credits = serializedObject.FindProperty("credits");
	}

	public override void OnInspectorGUI()
	{
//		base.OnInspectorGUI();
//		EditorGUILayout.Separator();

		EditorGUILayout.LabelField("クレジット表記");

		EditorGUI.indentLevel = 1;

		for (var i = 0; i < credits.arraySize; i++)
		{
			var credit = credits.GetArrayElementAtIndex(i);
			var type = credit.FindPropertyRelative("creditType");
			EditorGUILayout.PropertyField(type, new GUIContent((i + 1).ToString()));

			switch ((CreditType) type.enumValueIndex)
			{
				case CreditType.Text:
					EditorGUILayout.PropertyField(credit.FindPropertyRelative("text"), new GUIContent(" "));
					break;

				case CreditType.Image:
					EditorGUILayout.PropertyField(credit.FindPropertyRelative("sprite"), new GUIContent(" "));
					break;
			}

			EditorGUILayout.Separator();
		}

		EditorGUILayout.BeginHorizontal();

		if (GUILayout.Button("-"))
		{
			credits.arraySize = Mathf.Max(0, credits.arraySize - 1);
		}

		if (GUILayout.Button("+"))
		{
			++credits.arraySize;
		}

		EditorGUILayout.EndHorizontal();

		serializedObject.ApplyModifiedProperties();
	}
}
#endif
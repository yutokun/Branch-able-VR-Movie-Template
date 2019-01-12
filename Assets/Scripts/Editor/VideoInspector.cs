using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(Video))]
public class VideoInspector : Editor
{
	SerializedProperty clip, nextIs, sentence, branches, arraySize;
	const int MaxArraySize = 5;

	void OnEnable()
	{
		clip = serializedObject.FindProperty("clip");
		nextIs = serializedObject.FindProperty("nextIs");
		sentence = serializedObject.FindProperty("sentence");
		branches = serializedObject.FindProperty("branches");
		arraySize = serializedObject.FindProperty("currentBranchSize");
	}

	public override void OnInspectorGUI()
	{
//		base.OnInspectorGUI();
		branches.arraySize = MaxArraySize;

		EditorGUILayout.PropertyField(clip, new GUIContent("再生するビデオ"));
		EditorGUILayout.PropertyField(nextIs, new GUIContent("次は"));

		if (nextIs.enumValueIndex == (int) NextIs.Video)
		{
			EditorGUILayout.Separator();

			EditorGUILayout.PrefixLabel("文章");
			EditorGUI.indentLevel = 1;
			sentence.stringValue = EditorGUILayout.TextArea(sentence.stringValue, GUILayout.Height(60f));
			EditorGUI.indentLevel = 0;

			EditorGUILayout.Separator();
			arraySize.intValue = EditorGUILayout.IntSlider("選択肢の数", arraySize.intValue, 1, MaxArraySize);

			for (var i = 0; i < arraySize.intValue; i++)
			{
				EditorGUI.indentLevel = 1;

				EditorGUILayout.Separator();
				EditorGUILayout.LabelField("選択肢 " + (i + 1), EditorStyles.boldLabel);

				EditorGUI.indentLevel = 2;

				var video = branches.GetArrayElementAtIndex(i).FindPropertyRelative("video");
				EditorGUILayout.PropertyField(video, new GUIContent("ビデオ"));

				var text = branches.GetArrayElementAtIndex(i).FindPropertyRelative("text");
				EditorGUILayout.PropertyField(text, new GUIContent("ボタンのテキスト"));

				EditorGUI.indentLevel = 1;
			}
		}
		else if (nextIs.enumValueIndex == (int) NextIs.End)
		{
			EditorGUILayout.HelpBox("動画「" + clip.objectReferenceValue.name + "」の再生が終わると、終了メッセージへ移行します。", MessageType.Info);
		}

		serializedObject.ApplyModifiedProperties();
	}
}
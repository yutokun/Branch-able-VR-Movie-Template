using UnityEngine;
using UnityEngine.Video;
#if UNITY_EDITOR
using UnityEditor;
#endif

[System.Serializable]
public struct Branch
{
	public Video video;
	[Multiline] public string text;
}

public enum NextIs
{
	Video,
	Credits,
	End
}

/// <summary>
/// 再生するビデオと、その後の処理を指定します。
/// </summary>
public class Video : MonoBehaviour
{
	PlaybackController player;
	public VideoClip clip;
	public NextIs nextIs;
	[Range(-180f, 180f)] public float rotationOffset;

	[Multiline] public string sentence;
	public Branch[] branches;
	public int currentBranchSize;

	public AudioClip overrideSoundOnBranch;

	void Awake()
	{
		player = FindObjectOfType<PlaybackController>();
	}

	public void Play()
	{
		player.Play(clip, nextIs, this);
	}

	void OnValidate()
	{
		name = "Video" + (clip ? ": " + clip.name : "");
		if (player) player.SetRotation(rotationOffset);
	}
}

#if UNITY_EDITOR
[CustomEditor(typeof(Video))]
public class VideoInspector : Editor
{
	SerializedProperty clip, nextIs, rotationOffset, sentence, branches, arraySize, overrideSoundOnBranch;
	const int MaxArraySize = 5;

	void OnEnable()
	{
		clip = serializedObject.FindProperty("clip");
		nextIs = serializedObject.FindProperty("nextIs");
		rotationOffset = serializedObject.FindProperty("rotationOffset");
		sentence = serializedObject.FindProperty("sentence");
		branches = serializedObject.FindProperty("branches");
		arraySize = serializedObject.FindProperty("currentBranchSize");
		overrideSoundOnBranch = serializedObject.FindProperty("overrideSoundOnBranch");
	}

	public override void OnInspectorGUI()
	{
//		base.OnInspectorGUI();
		branches.arraySize = MaxArraySize;

		EditorGUILayout.PropertyField(clip, new GUIContent("再生するビデオ"));
		EditorGUILayout.PropertyField(nextIs, new GUIContent("次は"));
		EditorGUILayout.PropertyField(rotationOffset, new GUIContent("回転のオフセット（度）"));

		switch ((NextIs) nextIs.enumValueIndex)
		{
			case NextIs.Video:
				EditorGUILayout.Separator();

				EditorGUILayout.LabelField("選択肢の設定");

				EditorGUILayout.Separator();

				EditorGUI.indentLevel = 1;
				EditorGUILayout.PrefixLabel("文章");
				EditorGUI.indentLevel = 2;
				sentence.stringValue = EditorGUILayout.TextArea(sentence.stringValue, GUILayout.Height(60f));
				EditorGUI.indentLevel = 1;

				EditorGUILayout.Separator();
				arraySize.intValue = EditorGUILayout.IntSlider("選択肢の数", arraySize.intValue, 1, MaxArraySize);

				for (var i = 0; i < arraySize.intValue; i++)
				{
					EditorGUI.indentLevel = 2;

					EditorGUILayout.Separator();
					EditorGUILayout.LabelField("選択肢 " + (i + 1), EditorStyles.boldLabel);

					EditorGUI.indentLevel = 3;

					var video = branches.GetArrayElementAtIndex(i).FindPropertyRelative("video");
					EditorGUILayout.PropertyField(video, new GUIContent("ビデオ"));

					var text = branches.GetArrayElementAtIndex(i).FindPropertyRelative("text");
					EditorGUILayout.PropertyField(text, new GUIContent("ボタンのテキスト"));
				}

				EditorGUI.indentLevel = 1;

				EditorGUILayout.Separator();
				EditorGUILayout.PropertyField(overrideSoundOnBranch, new GUIContent("この背景音で上書き"));
				break;

			case NextIs.Credits:
				ShowHelpBox("エンドクレジット");
				break;

			case NextIs.End:
				ShowHelpBox("終了メッセージ");
				break;
		}

		serializedObject.ApplyModifiedProperties();
	}

	void ShowHelpBox(string destination)
	{
		if (clip.objectReferenceValue == null) return;
		EditorGUILayout.HelpBox($"動画「{clip.objectReferenceValue.name}」の再生が終わると、{destination}へ移行します。", MessageType.Info);
	}
}
#endif
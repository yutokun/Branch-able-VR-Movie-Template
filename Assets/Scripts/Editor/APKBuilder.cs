using System.Linq;
using UnityEditor;

/// <summary>
/// ストアに弾かれない APK を作成します
/// </summary>
public class APKBuilder
{
	[MenuItem("Build/Build for Store")]
	public static void Build()
	{
		//ファイル名を取得
		var path = EditorUtility.SaveFolderPanel("ビルドの保存先を指定してください。", "", "");
		var levels = from scene in EditorBuildSettings.scenes
		             where scene.enabled
		             select scene.path;

		//ビルドナンバーをインクリメント
		++PlayerSettings.Android.bundleVersionCode;

		//ビルド
		BuildPipeline.BuildPlayer(levels.ToArray(), path + "/Branch-able Story.apk", BuildTarget.Android, BuildOptions.None);
	}
}
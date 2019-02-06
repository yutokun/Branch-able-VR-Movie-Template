using UnityEngine;

public class BatteryChecker : MonoBehaviour
{
	[SerializeField, Range(0f, 100f), Header("警告を表示するバッテリ残量（%）")] float threshold = 10f;

	void Start()
	{
		// バッテリ残量が取得できない場合
		if (SystemInfo.batteryLevel == -1f) Hide();

		if (SystemInfo.batteryLevel > threshold / 100f) Hide();

		PlaybackController.onStartPlaying += Hide;
	}

	public void Hide()
	{
		gameObject.SetActive(false);
	}
}
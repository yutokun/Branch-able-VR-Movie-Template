using UnityEngine;

public class BatteryChecker : MonoBehaviour
{
	[SerializeField, Range(0f, 100f), Header("警告を表示するバッテリ残量（%）")] float threshold = 10f;

	void OnEnable()
	{
		PlaybackController.onStartPlaying += Hide;
	}

	void OnDisable()
	{
		PlaybackController.onStartPlaying -= Hide;
	}

	void Start()
	{
		// バッテリ残量が取得できない場合
		if (SystemInfo.batteryLevel == -1f) Hide();

		if (SystemInfo.batteryLevel > threshold / 100f) Hide();
	}

	void Hide()
	{
		gameObject.SetActive(false);
	}
}
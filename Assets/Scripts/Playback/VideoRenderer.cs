using UnityEngine;
using UnityEngine.Video;

/// <summary>
/// RenderTexture を介することによる問題（解像度が変わる際の遅延とノイズ）を解決するため、ビデオのテクスチャを直接 Skybox にレンダーします。
/// </summary>
public class VideoRenderer : MonoBehaviour
{
	[SerializeField] VideoPlayer video;
	[SerializeField] Material sky;

	void Update()
	{
		// ビデオのフレームを Skybox にコピー
		sky.mainTexture = video.texture;
	}
}
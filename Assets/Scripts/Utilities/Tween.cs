using System.Collections;
using TMPro;
using UnityEngine;

public static class Tween
{
	public static Coroutine Scale(MonoBehaviour self, Transform target, float scale, float duration)
	{
		return self.StartCoroutine(ScaleCoroutine(target, scale, duration));
	}

	static IEnumerator ScaleCoroutine(Transform target, float scale, float duration)
	{
		var initial = target.localScale;
		var startTime = Time.time;
		var targetScale = Vector3.one * scale;
		while (target.localScale != targetScale)
		{
			var lerpTime = (Time.time - startTime) / duration;
			target.localScale = Vector3.Lerp(initial, targetScale, lerpTime);
			yield return null;
		}
	}

	public static Coroutine FadeText(MonoBehaviour self, TextMeshPro target, Color color, float duration)
	{
		return self.StartCoroutine(FadeText(target, color, duration));
	}

	static IEnumerator FadeText(TextMeshPro target, Color color, float duration)
	{
		var initial = target.color;
		var startTime = Time.time;
		while (target.color != color)
		{
			var lerpTime = (Time.time - startTime) / duration;
			target.color = Color.Lerp(initial, color, lerpTime);
			yield return null;
		}
	}

	public static Coroutine FadeMaterial(MonoBehaviour self, Material target, Color color, float duration)
	{
		return self.StartCoroutine(FadeMaterialCoroutine(target, color, duration));
	}

	static IEnumerator FadeMaterialCoroutine(Material target, Color color, float duration)
	{
		var initial = target.color;
		var startTime = Time.time;
		while (target.color != color)
		{
			var lerpTime = (Time.time - startTime) / duration;
			target.color = Color.Lerp(initial, color, lerpTime);
			yield return null;
		}
	}
}
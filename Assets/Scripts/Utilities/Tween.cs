using System;
using System.Collections;
using TMPro;
using UnityEngine;

public static class Tween
{
	public static Coroutine Scale(this Transform target, float scale, float duration, Action onComplete = null)
	{
		return TweenBase.Instance.StartCoroutine(ScaleCoroutine(target, scale, duration, onComplete));
	}

	static IEnumerator ScaleCoroutine(Transform target, float scale, float duration, Action onComplete = null)
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

		onComplete?.Invoke();
	}

	public static Coroutine FadeText(this TextMeshPro target, Color color, float duration, Action onComplete = null)
	{
		return TweenBase.Instance.StartCoroutine(FadeTextCoroutine(target, color, duration, onComplete));
	}

	public static Coroutine FadeTextAlpha(this TextMeshPro target, float alpha, float duration, Action onComplete = null)
	{
		var color = target.color;
		color.a = Mathf.Clamp01(alpha);
		return TweenBase.Instance.StartCoroutine(FadeTextCoroutine(target, color, duration, onComplete));
	}

	static IEnumerator FadeTextCoroutine(TextMeshPro target, Color color, float duration, Action onComplete = null)
	{
		var initial = target.color;
		var startTime = Time.time;
		while (target.color != color)
		{
			var lerpTime = (Time.time - startTime) / duration;
			target.color = Color.Lerp(initial, color, lerpTime);
			yield return null;
		}

		onComplete?.Invoke();
	}

	public static Coroutine FadeMaterial(Material target, Color color, float duration, Action onComplete = null)
	{
		return TweenBase.Instance.StartCoroutine(FadeMaterialCoroutine(target, color, duration, onComplete));
	}

	static IEnumerator FadeMaterialCoroutine(Material target, Color color, float duration, Action onComplete = null)
	{
		var initial = target.color;
		var startTime = Time.time;
		while (target.color != color)
		{
			var lerpTime = (Time.time - startTime) / duration;
			target.color = Color.Lerp(initial, color, lerpTime);
			yield return null;
		}

		onComplete?.Invoke();
	}

	public static Coroutine FadeVolume(this AudioSource audio, float volume, float duration, Action onComplete = null)
	{
		return TweenBase.Instance.StartCoroutine(FadeVolumeCoroutine(audio, volume, duration, onComplete));
	}

	static IEnumerator FadeVolumeCoroutine(AudioSource audio, float volume, float duration, Action onComplete = null)
	{
		var initial = audio.volume;
		var startTime = Time.time;
		while (audio.volume != volume)
		{
			var lerpTime = (Time.time - startTime) / duration;
			audio.volume = Mathf.Lerp(initial, volume, lerpTime);
			yield return null;
		}

		onComplete?.Invoke();
	}
}